# WheelSpeed Qt

A Qt Widgets port of the "WheelSpeed" wheel rotation visual effect simulator.

## Features

- Adjustable wheel rotation speed (RPM) with configurable increments.
- Customisable spoke count and optional colour marking for one spoke.
- Configurable sampling frequency that drives the animation timer.
- Start/stop controls via toolbar, menu, and panel button.

## Building

```bash
cmake -S . -B build
cmake --build build
```

The project requires Qt 5.15 (or newer) with the Widgets module available in your build environment.
