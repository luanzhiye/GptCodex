import QtQuick 2.15
import QtQuick.Controls 2.15
import QtQuick.Layouts 1.15
import "."

ApplicationWindow {
    id: window
    width: 800
    height: 600
    visible: true
    title: qsTr("Wheel Speed Visualizer")

    color: "#0f1a26"

    Rectangle {
        anchors.fill: parent
        gradient: Gradient {
            GradientStop { position: 0.0; color: "#1b2d3f" }
            GradientStop { position: 1.0; color: "#02070b" }
        }
    }

    Wheel {
        id: wheel
        anchors.centerIn: parent
        width: Math.min(parent.width, parent.height) * 0.6
        height: width
        speedRpm: speedSlider.value
        strobeFrequency: strobeToggle.checked ? strobeSlider.value : 0
        spokeCount: Math.round(spokeCountSlider.value)
    }

    ColumnLayout {
        id: controlPanel
        anchors {
            left: parent.left
            right: parent.right
            bottom: parent.bottom
            margins: 28
        }
        spacing: 12

        Rectangle {
            Layout.fillWidth: true
            height: 2
            color: "#24435f"
            opacity: 0.6
        }

        Text {
            Layout.fillWidth: true
            text: {
                if (speedSlider.value === 0)
                    return qsTr("Wheel stopped");
                const direction = speedSlider.value > 0 ? qsTr("forward") : qsTr("reverse");
                return qsTr("Speed: %1 rpm (%2)")
                        .arg(speedSlider.value.toFixed(1))
                        .arg(direction);
            }
            color: "#e0f6ff"
            font.pixelSize: 22
            horizontalAlignment: Text.AlignHCenter
        }

        Slider {
            id: speedSlider
            Layout.fillWidth: true
            from: -120
            to: 120
            value: 30
            stepSize: 1
            snapMode: Slider.SnapAlways
        }

        RowLayout {
            Layout.fillWidth: true
            spacing: 12

            Button {
                Layout.fillWidth: true
                text: qsTr("Stop")
                onClicked: speedSlider.value = 0
            }

            Button {
                Layout.fillWidth: true
                text: qsTr("Reverse")
                onClicked: speedSlider.value = -Math.abs(speedSlider.value)
            }

            Button {
                Layout.fillWidth: true
                text: qsTr("Forward")
                onClicked: speedSlider.value = Math.abs(speedSlider.value)
            }
        }

        RowLayout {
            Layout.fillWidth: true
            spacing: 12

            Label {
                text: qsTr("Spokes: %1").arg(Math.round(spokeCountSlider.value))
                color: "#c7e2ff"
                Layout.alignment: Qt.AlignVCenter
            }

            Slider {
                id: spokeCountSlider
                Layout.fillWidth: true
                from: 6
                to: 24
                stepSize: 1
                value: 12
                snapMode: Slider.SnapAlways
            }
        }

        ColumnLayout {
            Layout.fillWidth: true
            spacing: 6

            RowLayout {
                Layout.fillWidth: true
                spacing: 12

                Label {
                    text: qsTr("Strobe")
                    color: "#c7e2ff"
                    Layout.alignment: Qt.AlignVCenter
                }

                Switch {
                    id: strobeToggle
                    Layout.alignment: Qt.AlignVCenter
                }

                Item { Layout.fillWidth: true }

                Label {
                    text: strobeToggle.checked ? qsTr("%1 Hz").arg(strobeSlider.value.toFixed(1)) : qsTr("off")
                    color: "#c7e2ff"
                }
            }

            Slider {
                id: strobeSlider
                Layout.fillWidth: true
                from: 1
                to: 30
                stepSize: 0.5
                value: 8
                enabled: strobeToggle.checked
                snapMode: Slider.SnapOnRelease
            }
        }
    }
}