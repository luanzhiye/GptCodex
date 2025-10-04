import QtQuick 2.15

Item {
    id: wheel
    property real speedRpm: 0
    property int spokeCount: 12
    property color rimColor: "#3d3d3d"
    property color spokeColor: "#f5f5f5"
    property color hubColor: "#c0c0c0"
    property real strobeFrequency: 0
    property real rotationAngle: 0

    implicitWidth: 360
    implicitHeight: 360

    onSpeedRpmChanged: {
        if (Math.abs(speedRpm) > 0.01) {
            rotationTimer.restart();
        }
    }

    readonly property real _degreesPerMillisecond: speedRpm * 360 / 60000

    Timer {
        id: rotationTimer
        interval: 16
        repeat: true
        running: wheel.visible && wheel.enabled && Math.abs(wheel.speedRpm) > 0.01
        onTriggered: {
            wheel.rotationAngle = (wheel.rotationAngle + _degreesPerMillisecond * interval) % 360
        }
    }

    Timer {
        id: strobeTimer
        property bool flash: false
        interval: strobeFrequency > 0 ? Math.max(10, 1000 / strobeFrequency) : 1000
        repeat: true
        running: wheel.visible && wheel.enabled && strobeFrequency > 0
        onTriggered: flash = !flash
        onRunningChanged: flash = false
    }

    Rotation {
        id: rotationTransform
        origin.x: wheel.width / 2
        origin.y: wheel.height / 2
        angle: wheel.rotationAngle
    }

    transform: [ rotationTransform ]

    Rectangle {
        anchors.fill: parent
        color: "transparent"
    }

    Rectangle {
        id: rim
        anchors.centerIn: parent
        width: parent.width
        height: parent.width
        radius: width / 2
        color: "#101010"
        border.color: rimColor
        border.width: width * 0.08
        antialiasing: true
    }

    Rectangle {
        anchors.centerIn: parent
        width: rim.width * 0.92
        height: width
        radius: width / 2
        gradient: Gradient {
            GradientStop { position: 0.0; color: "#2c2c2c" }
            GradientStop { position: 1.0; color: "#050505" }
        }
        border.color: "transparent"
        antialiasing: true
        opacity: 0.85
    }

    Rectangle {
        id: spokeMask
        anchors.centerIn: parent
        width: rim.width * 0.92
        height: width
        radius: width / 2
        color: "transparent"
        border.color: "transparent"
        clip: true

        Repeater {
            model: spokeCount
            Rectangle {
                width: spokeMask.width * 0.08
                height: spokeMask.height
                radius: width / 2
                color: spokeColor
                antialiasing: true
                anchors.centerIn: parent
                rotation: index * 360 / spokeCount
            }
        }
    }

    Rectangle {
        id: hub
        anchors.centerIn: parent
        width: rim.width * 0.25
        height: width
        radius: width / 2
        color: hubColor
        border.color: "#7d7d7d"
        border.width: width * 0.08
        antialiasing: true
    }

    Rectangle {
        anchors.centerIn: hub
        width: hub.width * 0.6
        height: width
        radius: width / 2
        gradient: Gradient {
            GradientStop { position: 0.0; color: "#ffffff" }
            GradientStop { position: 1.0; color: "#7d7d7d" }
        }
        border.color: "#4f4f4f"
        border.width: width * 0.05
        antialiasing: true
    }

    Rectangle {
        id: strobeOverlay
        anchors.fill: parent
        color: "#000000"
        opacity: strobeTimer.flash ? 0.4 : 0.0
        visible: strobeFrequency > 0
        Behavior on opacity {
            NumberAnimation { duration: 120 }
        }
    }
}