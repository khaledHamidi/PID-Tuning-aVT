/*
================================================================================
                        🤖 ARDUINO SELF-BALANCING by KHALED HAMIDI
================================================================================

📋 PROJECT OVERVIEW:
   Advanced PID-controlled self-balancing system using MPU6050 IMU sensor
   and dual ESC-driven brushless motors for real-time balance control.

🎯 MAIN FEATURES:
   ✓ Real-time PID control loop (100Hz frequency)
   ✓ Live parameter tuning via serial interface
   ✓ Persistent EEPROM storage for all settings
   ✓ Emergency stop and safety features
   ✓ System diagnostics and telemetry
   ✓ Optimized performance with timed intervals

🔌 HARDWARE CONNECTIONS:
   MPU6050 IMU Sensor:
   ├── VCC    →  5V        (Power supply)
   ├── GND    →  GND       (Ground)
   ├── SDA    →  A4        (I²C Data line)
   └── SCL    →  A5        (I²C Clock line)
   
   ESC Controllers:
   ├── ESC1   →  Pin 9     (Left motor control)
   └── ESC2   →  Pin 10    (Right motor control)

⚙️ SYSTEM PARAMETERS:
   • PID Frequency: 100Hz (10ms intervals)
   • Serial Baud Rate: 115200
   • I²C Clock: 400kHz
   • Motor PWM Range: 1000-2000 μs
   • Base Speed: 1500 μs (neutral position)
   • Max Output Limit: ±400
📡 SERIAL COMMANDS:
   PID Tuning:
   ├── Kp:<value>      → Set proportional gain (integer part)
   ├── Kpd:<value>     → Set proportional gain (decimal part)
   ├── Ki:<value>      → Set integral gain (integer part)
   ├── Kid:<value>     → Set integral gain (decimal part)
   ├── Kd:<value>      → Set derivative gain (integer part)
   └── Kdd:<value>     → Set derivative gain (decimal part)
   
   System Control:
   ├── target:<angle>  → Set target angle (-30° to +30°)
   ├── limit:<value>   → Set PID output limit (0-500)
   ├── baseSpeed:<val> → Set neutral motor speed (1000-2000)
   ├── start           → Resume operation
   ├── stop            → Emergency stop motors
   ├── reset           → Recalibrate MPU6050
   ├── save            → Store settings to EEPROM
   ├── status          → Display system information
   └── test            → Run hardware diagnostics
   
   Monitoring:
   ├── monitor:on      → Enable telemetry output
   └── monitor:off     → Disable telemetry output

🚀 PERFORMANCE OPTIMIZATIONS:
   • Timed serial processing (every 5ms)
   • Reduced telemetry frequency (every 50ms)
   • Fixed PID computation intervals
   • Efficient I²C communication
   • Input validation and error handling

⚠️ SAFETY FEATURES:
   • Automatic emergency stop on sensor failure
   • Angle range validation (±60°)
   • Parameter bounds checking
   • Connection status monitoring
   • Graceful error recovery

================================================================================
*/

#include <Arduino.h>
#include <Wire.h>
#include <MPU6050_light.h>
#include <Servo.h>
#include <PID_v1.h>
#include <EEPROM.h>

#define log(msg) Serial.println(String(">> ") + msg)

// ⏱️ TIMING CONTROL SYSTEM
unsigned long lastPIDTime = 0;
unsigned long lastSerialTime = 0;
unsigned long lastTelemetryTime = 0;
const unsigned long PID_INTERVAL = 30;       // 10ms = 100Hz for optimal response | 0ms for max response.
const unsigned long SERIAL_INTERVAL = 1000;     // Check serial every 5ms to reduce load
const unsigned long TELEMETRY_INTERVAL = 300; // Send data every 50ms to prevent flooding
const int MAX_ANGLE = 40  ;// +-60.

/* Hardware objects */
MPU6050 mpu(Wire);
Servo ESC, ESC2;

/* PID variables */
double target = 0, angle, out;
double Kp = 10, Ki = 0, Kd = 0;
PID pid(&angle, &out, &target, Kp, Ki, Kd, DIRECT);

int baseSpeed = 1500;   // Center position (stopped)
int limit_range = 400;  // Full range ±400



/* State variables */
bool ERROR_ONLY = true;
bool emergency = false;
bool mpuConnected = false;
String serialBuffer = "";

/* EEPROM addresses */
constexpr int EE_ADDR_KP = 0;
constexpr int EE_ADDR_KI = EE_ADDR_KP + sizeof(double);
constexpr int EE_ADDR_KD = EE_ADDR_KI + sizeof(double);
constexpr int EE_ADDR_TARGET = EE_ADDR_KD + sizeof(double);
constexpr int EE_ADDR_LIMIT = EE_ADDR_TARGET + sizeof(double);
constexpr int EE_ADDR_BASESPEED = EE_ADDR_LIMIT + sizeof(int);

/* Helper functions */
static inline void setWhole(double& v, long w) {
  v = w + (v - floor(v));
}

//  يستبدل الجزء الكسري لـ v بالأرقام الموجودة في السلسلة s.
//  يتجاهل أي محارف غير رقمية (مثل "s= 0001") ويحافظ على إشارة العدد.
static inline void setDecimal(double& v, const String& s)
{
    // استخراج الأرقام فقط
    String digits;
    for (char c : s) {
        if (isDigit(c)) digits += c;
    }
    if (digits.length() == 0) return;          // لا أرقام

    unsigned long n = digits.toInt();          // القيمة الرقمية
    uint8_t len    = digits.length();          // عدد المنازل

    double frac = static_cast<double>(n) / pow(10.0, len);  // الجزء الكسري الجديد

    // الجزء الصحيح مع مراعاة الإشارة
    double intPart = (v >= 0.0) ? floor(v) : ceil(v);

    // إعادة تركيب العدد
    v = (v >= 0.0) ? intPart + frac
                   : intPart - frac;
}


void setup() {
  Serial.begin(2500000);
  log("System initializing...");
  
  /* Initialize I2C with error checking */
  Wire.begin();
  Wire.setClock(400000);
  
  /* Initialize MPU with error checking */
  log("Connecting to MPU6050...");
  byte status = mpu.begin();
  if (status != 0) {
    log("ERROR: MPU6050 connection failed! Code: " + String(status));
    emergency = true;
    return;
  }
  
  log("MPU6050 connected successfully");
  log("Calibrating MPU6050...");
  mpu.calcOffsets();
  mpuConnected = true;
  
  /* Initialize motors with proper sequence */
  log("Initializing ESCs...");
  ESC.attach(9);
  ESC2.attach(10);
  
  // ARM sequence
  ESC.writeMicroseconds(1000);
  ESC2.writeMicroseconds(1000);
  delay(3000);
  
  log("ESC initialization complete");
  
  /* Load settings from EEPROM */
  loadFromEEPROM();
  
  /* Initialize PID */
  pid.SetMode(AUTOMATIC);
  pid.SetSampleTime(PID_INTERVAL);
  updatePID();
  
  log("System ready for operation");
  parseCommand("status");
}

void loop() {
  unsigned long currentTime = millis();
  
  /* 🔄 Update IMU data continuously for best response */
  if (mpuConnected && !emergency) {
    mpu.update();
    angle = mpu.getAngleY();
    
    // Safety check for valid angle
    if (isnan(angle) || abs(angle) > MAX_ANGLE) {
      //log("ERROR: Invalid angle detected!");
      //emergencyStop();
      return;
    }
  }
  
  /* ⚡ PID computation at fixed (1/PID_INTERVAL)Hz intervals */
    if (pid.Compute() && !emergency) {
      // Apply motor commands
      int motor1 = constrain(baseSpeed - out, 1000, 2000);
      int motor2 = constrain(baseSpeed + out, 1000, 2000);
      
      ESC.writeMicroseconds(motor1);
    //  ESC2.writeMicroseconds(motor2);
      
      // Send telemetry at lower frequency to reduce overhead
      if (currentTime - lastTelemetryTime >= TELEMETRY_INTERVAL && !ERROR_ONLY) {
        lastTelemetryTime = currentTime;
        Serial.print(currentTime);
        Serial.print(":");
        Serial.print(angle, 2);
        Serial.print(":");
        Serial.println(motor2);
      }
    }
  
  /* 📡 Handle serial commands at optimized intervals (every 5ms instead of every loop) */
  if (currentTime - lastSerialTime >= SERIAL_INTERVAL) {
    lastSerialTime = currentTime;
    handleSerialCommands();
  }
}

void handleSerialCommands() {
  while (Serial.available()) {
    char c = Serial.read();
    if (c == '\n') {
      parseCommand(serialBuffer);
      serialBuffer = "";
    } else if (c != '\r') {  // Ignore carriage return
      serialBuffer += c;
    }
  }
}

void parseCommand(String cmd) {
  cmd.trim();
  
  if (cmd.startsWith("stop")) {
    emergencyStop();
    emergency = true;
  } else if (cmd.startsWith("start")) {
    if (mpuConnected) {
      emergency = false;
      log("System resumed");
    } else {
      log("ERROR: Cannot start - MPU6050 not connected");
    }
  } else if (cmd.startsWith("Kp:")) {
    setWhole(Kp, cmd.substring(3).toInt());
    updatePID();
    Serial.println(">> Kp = " + String(Kp, 4));
  } else if (cmd.startsWith("Kpd:")) {
    setDecimal(Kp, cmd.substring(4));
    updatePID();
    Serial.println(">> Kp = " + String(Kp, 4));
  } else if (cmd.startsWith("Ki:")) {
    setWhole(Ki, cmd.substring(3).toInt());
    updatePID();
    Serial.println(">> Ki = " + String(Ki, 4));
  } else if (cmd.startsWith("Kid:")) {
    setDecimal(Ki, cmd.substring(4));
    updatePID();
    Serial.println(">> Ki = " + String(Ki, 4));
  } else if (cmd.startsWith("Kd:")) {
    setWhole(Kd, cmd.substring(3).toInt());
    updatePID();
    Serial.println(">> Kd = " + String(Kd, 4));
  } else if (cmd.startsWith("Kdd:")) {
    setDecimal(Kd, cmd.substring(4));
    updatePID();
    Serial.println(">> Kd = " + String(Kd, 4));
  } else if (cmd.startsWith("limit:")) {
    limit_range = constrain(cmd.substring(6).toInt(), 0, 500);
    updatePID();
    Serial.println(">> limit: " + String(limit_range));
  } else if (cmd.startsWith("baseSpeed:")) {
    baseSpeed = constrain(cmd.substring(10).toInt(), 1000, 2000);
    Serial.println(">> baseSpeed: " + String(baseSpeed));
  } else if (cmd == "monitor:on") {
    ERROR_ONLY = false;
  } else if (cmd == "monitor:off") {
    ERROR_ONLY = true;
  } else if (cmd.startsWith("target:")) {
    target = constrain(cmd.substring(7).toFloat(), -30, 30);
    Serial.println(">> Target = " + String(target, 2));
  } else if (cmd == "reset") {
    if (mpuConnected) {
      mpu.calcOffsets();
      Serial.println(">> MPU recalibrated");
    } else {
      Serial.println(">> ERROR: MPU not connected");
    }
  } else if (cmd == "status") {
    printStatus();
  } else if (cmd == "save") {
    saveToEEPROM();
    Serial.println(">> Settings saved to EEPROM");
  } else if (cmd == "test") {
    systemTest();
  } else {
    Serial.println(">> Unknown command: " + cmd);
  }
}

void updatePID() {
  // Validate parameters
  if (isnan(Kp) || isnan(Ki) || isnan(Kd)) {
    log("ERROR: Invalid PID parameters detected");
    return;
  }
  
  pid.SetOutputLimits(-limit_range, limit_range);
  pid.SetTunings(Kp, Ki, Kd);
}

void emergencyStop() {
  ESC.writeMicroseconds(1000);
  ESC2.writeMicroseconds(1000);
  emergency = true;
  log("EMERGENCY STOP: Motors OFF");
}

void printStatus() {
  Serial.println("STATUS_START");
  Serial.println("Connected: " + String(mpuConnected ? "YES" : "NO"));
  Serial.println("Emergency: " + String(emergency ? "YES" : "NO"));
  Serial.println("Angle: " + String(angle, 2));
  Serial.println("PID_Output: " + String(out, 2));
  Serial.println("Kp: " + String(Kp, 4));
  Serial.println("Ki: " + String(Ki, 4));
  Serial.println("Kd: " + String(Kd, 4));
  Serial.println("Target: " + String(target, 2));
  Serial.println("Limit: " + String(limit_range));
  Serial.println("BaseSpeed: " + String(baseSpeed));
  Serial.println("STATUS_END");
}

void loadFromEEPROM() {
  // Load with fallback defaults
  EEPROM.get(EE_ADDR_KP, Kp);
  if (isnan(Kp)) Kp = 10;
  
  EEPROM.get(EE_ADDR_KI, Ki);
  if (isnan(Ki)) Ki = 0;
  
  EEPROM.get(EE_ADDR_KD, Kd);
  if (isnan(Kd)) Kd = 0;
  
  EEPROM.get(EE_ADDR_TARGET, target);
  if (isnan(target)) target = 0;
  
  EEPROM.get(EE_ADDR_LIMIT, limit_range);
  if (limit_range < 0 || limit_range > 500) limit_range = 400;
  
  EEPROM.get(EE_ADDR_BASESPEED, baseSpeed);
  if (baseSpeed < 1000 || baseSpeed > 2000) baseSpeed = 1500;
}

void saveToEEPROM() {
  EEPROM.put(EE_ADDR_KP, Kp);
  EEPROM.put(EE_ADDR_KI, Ki);
  EEPROM.put(EE_ADDR_KD, Kd);
  EEPROM.put(EE_ADDR_TARGET, target);
  EEPROM.put(EE_ADDR_LIMIT, limit_range);
  EEPROM.put(EE_ADDR_BASESPEED, baseSpeed);
}

void systemTest() {
  log("Running system test...");
  
  // Test MPU6050
  if (mpuConnected) {
    log("✓ MPU6050: Connected");
    log("  Current angle: " + String(mpu.getAngleY(), 2));
  } else {
    log("✗ MPU6050: FAILED");
  }
  
  // Test motors
  log("Testing motors (2 seconds)...");
  ESC.writeMicroseconds(1200);
  ESC2.writeMicroseconds(1200);
  delay(1000);
  ESC.writeMicroseconds(1300);
  ESC2.writeMicroseconds(1300);
  delay(1000);
  ESC.writeMicroseconds(1000);
  ESC2.writeMicroseconds(1000);
  
  log("System test complete");
}