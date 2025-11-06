# PID Tuning and Visualization Tool

A desktop application for tuning PID controllers. With Arduino code for balancing platforms Works with MPU6050 sensors and BLDC motors.

![Version](https://img.shields.io/badge/version-1.0-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)
![.NET](https://img.shields.io/badge/.NET-Framework-purple.svg)
![Arduino](https://img.shields.io/badge/Arduino-Compatible-00979D.svg)
![License](https://img.shields.io/badge/license-Open%20Source-green.svg)


## What It Does

This tool lets you adjust PID parameters in real-time while watching your system respond. You get live graphs, performance metrics, and data logging. No guesswork, just tune and see results immediately.


## Quick Start

### What You Need

- Windows PC
- Arduino Uno or similar
- MPU6050 sensor
- Two ESC motor controllers
- USB cable


### Installation

Go to `bin/PID Tuning aVT 1.0/` and run the application file. That's it.

If you want to build from source, open the solution in Visual Studio and hit build.


### Arduino Setup

1. Upload `Arduino/Arduino.ino` to your board
2. Wire up the MPU6050:
   - VCC to 5V
   - GND to GND  
   - SDA to A4
   - SCL to A5
3. Connect ESCs to pins 9 and 10
4. Plug in USB


## How to Use

1. Open the app and enter your COM port
2. Set baud rate to 115200 and click Connect
3. The app will read current PID values from Arduino
4. Adjust Kp, Ki, Kd values and watch the response
5. Use the slider to change target angle
6. Click Save to EEPROM when you find good values


## Commands

You can send these directly to Arduino:

```
Kp:15          - Set proportional gain
Ki:5           - Set integral gain  
Kd:2           - Set derivative gain
target:10      - Set target angle
limit:300      - Set output limit
baseSpeed:1500 - Set motor neutral point
save           - Save to EEPROM
status         - Get system info
reset          - Recalibrate sensor
stop           - Emergency stop
```


## Troubleshooting

Can't connect? Check your COM port and make sure nothing else is using it.

Motors not moving? Calibrate your ESCs first.

Sensor not found? Double check your wiring, especially SDA and SCL.

Laggy plots? Disable raw data logging and close the performance window.


## Technical Details

The system runs at 100Hz on the Arduino side. The PC application processes data in real-time with sub-20ms latency. Everything is threaded properly so the UI stays responsive even under heavy load.

PID calculations happen on Arduino. The PC just sends parameters and plots results. This keeps loop timing consistent regardless of what's happening on your computer.


## License

Open source. Do whatever you want with it.


## Author

Khaled HAMIDI

GitHub: khaledHamidi/PID-Tuning-aVT
