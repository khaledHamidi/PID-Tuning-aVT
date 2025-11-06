# PID Tuning and Visualization Tool

This tool enables **online PID parameter tuning** (Kp, Ki, Kd) with **instant visualization** of system response and telemetry feedback.  
It’s optimized for **dual-BLDC balancing systems**, providing synchronized control, data streaming, and precise feedback from the IMU.

![Version](https://img.shields.io/badge/version-1.0-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)
![.NET](https://img.shields.io/badge/.NET-Framework-purple.svg)
![Arduino](https://img.shields.io/badge/Arduino-Compatible-00979D.svg)
![License](https://img.shields.io/badge/license-Open%20Source-green.svg)


### Key Features

- 🔁 **Real-time / Online PID tuning** — adjust Kp, Ki, Kd while system runs.  
- ⚙️ **Low-latency multi-threaded communication** for smooth plotting and stable link.  
- 📊 **Data logging to CSV** — recorded telemetry easily importable into MATLAB or Python.  
- 🎛 **Live graph visualization** — shows target, feedback, and control signals.  
- 🧭 **Sensor calibration & diagnostics** — real-time MPU6050 monitoring.  
- 💾 **EEPROM save/load support** — retain PID settings across reboots.



## What It Does

This tool lets you adjust PID parameters in real-time while watching your system respond. You get live graphs, performance metrics, and data logging. No guesswork, just tune and see results immediately.


## Quick Start

### What You Need

- Download the released version and run it.
- for balancing platforms you need: 
   - Arduino Uno or similar
   - MPU6050 sensor
   - Two ESC motor controllers
   - USB cable

### Installation

download [PID-Tuning-aVT Release](https://github.com/khaledHamidi/PID-Tuning-aVT/releases/tag/release) and run. That's it.

If you want to build from source, open the solution in Visual Studio and hit build.


### Experiment Setup

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

## License

Open source. Do whatever you want with it.

## Author

Khaled HAMIDI

GitHub: khaledHamidi/PID-Tuning-aVT
