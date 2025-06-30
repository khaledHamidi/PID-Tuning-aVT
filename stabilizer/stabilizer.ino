/*
====================================================================
  Arduino Self-Balancing Control 
====================================================================

📌 PURPOSE:
- Implements a PID-controlled self-balancing system using an MPU6050 IMU and dual ESC-driven motors.
- Balances around Y-axis using real-time feedback and closed-loop PID adjustments.

📌 HARDWARE:
- MPU6050 (connected via I²C)
MPU6050 Pin     →     Arduino Pin       | Notes
------------------------------------------------------------
VCC             →     5V                | Power supply
GND             →     GND               | Ground
SDA             →     A4                | I²C data line
SCL             →     A5                | I²C clock line

- Two brushless motors driven by ESCs on pins 9 and 10
- Arduino board (any compatible with Servo, EEPROM, Wire libraries)

📌 FEATURES:
- PID control loop with real-time tuning via serial interface
- Persistent storage of PID parameters using EEPROM
- Live telemetry of system state (angle, PID output)
- Command-based interface for PID tuning and control

📌 SERIAL COMMANDS:
  - Kp:<int>       → Set integer part of Kp
  - Kpd:<int>    → Set decimal part of Kp
  - Ki:<int>       → Set integer part of Ki
  - Kid:<int>    → Set decimal part of Ki
  - Kd:<int>       → Set integer part of Kd
  - Kdd:<int>    → Set decimal part of Kd
  - target:<val>   → Set angle setpoint (default: 0°)
  - reset:         → Recalibrate MPU6050 offsets
  - save:          → Store PID values to EEPROM
  - status:        → Print current PID state to serial
  - stop:          → إيقاف المحرّكين فورًا (إلغاء الـ PID)

📌 NOTES:
- Motor PWM range: 1000–2000 µs (idle at 1500 µs)
- PID output is limited to [-400, 400], mapped to motor speed differential
====================================================================
*/

#include <Arduino.h>
#define log(msg) Serial.println(String(">> ") + msg)
/* ــــــــــــــــ أدوات مساعدة ــــــــــــــــ */
static inline void setWhole(double& v, long w) {        // يغيّر الجزء الصحيح فقط
  v = w + (v - floor(v));                                // احتفظ بالجزء العشري القديم
}

static inline void setDecimal(double& v, const String& s) { // يقبل أية خانات عشرية
  if (s.length() == 0) return;                           // بديل isEmpty()
  long  n   = s.toInt();                                 // مثال 453
  int   len = s.length();                                // عدد الخانات (3)
  double d  = (double)n / pow(10, len);                  // 0.453
  v = floor(v) + d;                                      // حدّد الجزء العشري الجديد
}
 
String serialBuffer = "";


/* EEPROM */

#include <EEPROM.h>
int Kp_int = 0, Ki_int = 0, Kd_int = 0;
int Kp_dec = 0, Ki_dec = 0, Kd_dec = 0;
/* ---------- EEPROM layout ---------- */
constexpr int EE_ADDR_KP = 0;
constexpr int EE_ADDR_KI = EE_ADDR_KP + sizeof(double);
constexpr int EE_ADDR_KD = EE_ADDR_KI + sizeof(double);
constexpr int EE_ADDR_TARGET = EE_ADDR_KD + sizeof(double);  // NEW
constexpr int EE_ADDR_LIMIT = EE_ADDR_TARGET + sizeof(int);  // NEW
constexpr int EE_ADDR_BASESPEED = EE_ADDR_LIMIT + sizeof(int);  // NEW


/* IMU */

#include <Wire.h>
#include <MPU6050_light.h>
MPU6050 mpu(Wire);
int power_high = A2, power_low = A3; // 

/***************************************/


/* MOTORS */

#include <Servo.h>
Servo ESC;
Servo ESC2;

/***************************************/


/* PID */

#include <PID_v1.h>
double target = 0;  // نريد زاوية Y = 0 (توازن)
double angle;       // mpu.getAngleY()
double out;         // الفرق في السرعة

double Kp = 10, Ki = 0, Kd = 0;
PID pid(&angle, &out, &target, Kp, Ki, Kd, DIRECT);

int baseSpeed = 1000;  // منتصف السرعة (متوقفة أو خفيفة)
int limit_range = 100; //  baseSpeed -400, +400 




bool ERROR_ONLY = false;
void setup() {

  Wire.begin();
  Wire.setClock(400000);  // 400 kHz I²C for speed
  Serial.begin(115200);
  log(">> System initializing...");

  /* MPU */
  //inMode(power_high, OUTPUT);
  //digitalWrite(power_high, HIGH);
  //pinMode(power_low, OUTPUT);
  log(">> MPU connecting Begin");
  mpu.begin();
  log(">> MPU connected successfully.");
  log(">> Calibrating MPU...");
  mpu.calcOffsets();  // fast calibration


  /* MOTORS */
  Serial.println(">> Motor calibration Begin.");
  ESC.attach(9);                 //
  ESC2.attach(10);               //
  ESC.writeMicroseconds(1000);   // تسليح (ARM)
  ESC2.writeMicroseconds(1000);  // تسليح (ARM)
  delay(3000);                   // انتظر حتى يصدر الـ ESC نغمة الجاهزية
  log(">> Motor calibration complete.");
  log(">> System ready.");

  /* EEPROM */
  EEPROM.get(EE_ADDR_KP, Kp);
  EEPROM.get(EE_ADDR_KI, Ki);
  EEPROM.get(EE_ADDR_KD, Kd);
  EEPROM.get(EE_ADDR_TARGET, target);  // NEW
  EEPROM.get(EE_ADDR_LIMIT, limit_range);  // NEW
  EEPROM.get(EE_ADDR_BASESPEED, baseSpeed);  // NEW

  parseCommand("status");
  /* PID */
  pid.SetMode(AUTOMATIC);
  pid.SetSampleTime(20);  // الآن حلقة PID تنفّذ كل 20ms بدلاً من 100ms

  updatePID();
}
int motor2,motor1;
void loop() {
  mpu.update();             // refresh sensor state
  angle = mpu.getAngleY();  // زاوية الميل
  pid.Compute();

  motor2 = constrain(baseSpeed + out, 1000, 2000);
  motor1 = constrain(baseSpeed - out, 1000, 2000);

  if (!ERROR_ONLY) {
    Serial.print(angle);  // X-axis tilt angle in degrees
    Serial.print(":");
    Serial.println(baseSpeed+out);
  }

  ESC.writeMicroseconds(motor1);
  ESC2.writeMicroseconds(motor2);

  handleSerialCommands();
}

void handleSerialCommands() {
  while (Serial.available()) {
    char c = Serial.read();
    if (c == '\n') {
      parseCommand(serialBuffer);
      serialBuffer = "";
    } else {
      serialBuffer += c;
    }
  }
}

void parseCommand(String cmd) {
  cmd.trim();

  /* ---- Kp ---- */
  if (cmd.startsWith("stop")) { emergencyStop(); } 
  else if (cmd.startsWith("Kp:"))  { setWhole (Kp, cmd.substring(3).toInt());          updatePID(); Serial.println(">> Kp = " + String(Kp, 4)); }
  else if (cmd.startsWith("Kpd:")) { setDecimal(Kp, cmd.substring(4));                 updatePID(); Serial.println(">> Kp = " + String(Kp, 4)); }

  /* ---- Ki ---- */
  else if (cmd.startsWith("Ki:"))  { setWhole (Ki, cmd.substring(3).toInt());          updatePID(); Serial.println(">> Ki = " + String(Ki, 4)); }
  else if (cmd.startsWith("Kid:")) { setDecimal(Ki, cmd.substring(4));                 updatePID(); Serial.println(">> Ki = " + String(Ki, 4)); }

  /* ---- Kd ---- */
  else if (cmd.startsWith("Kd:"))  { setWhole (Kd, cmd.substring(3).toInt());          updatePID(); Serial.println(">> Kd = " + String(Kd, 4)); }
  else if (cmd.startsWith("Kdd:")) { setDecimal(Kd, cmd.substring(4));                 updatePID(); Serial.println(">> Kd = " + String(Kd, 4)); }

  else if (cmd.startsWith("limit:")) { limit_range = cmd.substring(6).toInt(); updatePID(); Serial.println(">> limit:" + String(limit_range)); }
  else if (cmd.startsWith("baseSpeed:")) { baseSpeed = cmd.substring(10).toInt(); updatePID(); Serial.println(">> baseSpeed:" + String(baseSpeed)); }

  /* ---- بقيّة الأوامر ---- */
  else if (cmd == "monitor:on") {  ERROR_ONLY = false;}
  else if (cmd == "monitor:off") {  ERROR_ONLY = true;}
  else if (cmd.startsWith("target:")) { target = cmd.substring(7).toFloat(); Serial.println(">> Target = " + String(target)); }
  else if (cmd == "reset")           { mpu.calcOffsets(); Serial.println(">> MPU recalibrated."); }
  else if (cmd == "status") {
    Serial.println("STATUS_START");
    Serial.println("Kp:" + String(Kp, 4));
    Serial.println("Ki:" + String(Ki, 4));
    Serial.println("Kd:" + String(Kd, 4));
    Serial.println("setpoint:" + String(target, 2));
    Serial.println("limit:" + String(limit_range));
    Serial.println("baseSpeed:" + String(baseSpeed));
    Serial.println("STATUS_END");
  }
  else if (cmd.startsWith("save"))    { savePIDToEEPROM(); Serial.println(">> memory updated."); }
  else                                { Serial.println(">> Unknown command."); }
}

void savePIDToEEPROM() {
  EEPROM.put(EE_ADDR_KP,     Kp);
  EEPROM.put(EE_ADDR_KI,     Ki);
  EEPROM.put(EE_ADDR_KD,     Kd);
  EEPROM.put(EE_ADDR_TARGET, target);
  EEPROM.put(EE_ADDR_LIMIT, limit_range);
  EEPROM.put(EE_ADDR_BASESPEED, baseSpeed);
}

void updatePID() {
  if (isnan(Kp) || isnan(Ki) || isnan(Kd)|| isnan(limit_range)) {
    Serial.println(">> PID update failed: NaN detected.");
    return;
  }
  pid.SetOutputLimits(-limit_range, limit_range);
  pid.SetTunings(Kp, Ki, Kd);
}

void emergencyStop() {
  pid.SetMode(MANUAL); 
  out = 0;
  ESC.writeMicroseconds(1000); 
  ESC2.writeMicroseconds(1000);
  Serial.println(">> EMERGENCY STOP: Motors OFF");
}
