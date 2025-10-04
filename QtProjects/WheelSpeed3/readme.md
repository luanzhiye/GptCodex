# WheelSpeedQML

A Qt Quick (QML) port of the wheel speed visualisation demo. The application renders a stylised wheel with configurable spoke count and supports speed and stroboscope controls to study optical illusions while the wheel spins.

## Features

- QML-based UI with Qt Quick Controls 2 for sliders, switches and buttons.
- Adjustable rotation speed with forward and reverse direction control.
- Strobe light simulation to explore wagon-wheel aliasing effects.
- Dynamic spoke count selection to vary the perceived motion blur.

## Building

```
mkdir build && cd build
cmake ..
cmake --build .
```

The project automatically locates Qt 5.15+ or Qt 6. Use `-DCMAKE_PREFIX_PATH` if your Qt installation is not in the default search path.
