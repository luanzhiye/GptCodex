import QtQuick 2.15
import QtQuick.Controls 2.15
import QtQuick.Layouts 1.15
import QtQuick.Window 2.15

ApplicationWindow {
    id: window
    visible: true
    width: 1100
    height: 680
    minimumWidth: 960
    minimumHeight: 600
    title: qsTr("车轮转速模拟器")
    color: "#ffffff"

    property bool running: false
    property int spokeCount: 6
    property double rpmSetting: 10
    property double degreeSetting: 10
    property int sampleRate: 25
    property string noteText: ""

    header: ToolBar {
        id: topBar
        padding: 12
        background: Rectangle {
            color: "#f5f5f7"
            border.color: "#d9d9d9"
            border.width: 1
        }

        RowLayout {
            anchors.fill: parent
            spacing: 16

            Label {
                text: qsTr("操作")
                font.pixelSize: 18
                font.bold: true
                color: "#333333"
                Layout.alignment: Qt.AlignVCenter
            }

            ToolSeparator {
                Layout.fillHeight: true
            }

            RoundButton {
                id: playButton
                text: window.running ? "⏸" : "▶"
                font.pixelSize: 20
                Accessible.name: window.running ? qsTr("暂停") : qsTr("开始")
                onClicked: window.running = !window.running
                implicitWidth: 40
                implicitHeight: 40
            }

            RoundButton {
                text: "■"
                font.pixelSize: 18
                Accessible.name: qsTr("停止")
                onClicked: {
                    window.running = false
                }
                implicitWidth: 40
                implicitHeight: 40
            }

            Item { Layout.fillWidth: true }
        }
    }

    background: Rectangle { color: "#ffffff" }

    RowLayout {
        anchors.fill: parent
        anchors.topMargin: 0
        spacing: 0

        Rectangle {
            id: sidebar
            Layout.preferredWidth: 300
            Layout.fillHeight: true
            color: "#f5f6fa"
            border.color: "#d7d8dd"
            border.width: 1

            ColumnLayout {
                anchors.fill: parent
                anchors.margins: 24
                spacing: 20

                ColumnLayout {
                    spacing: 6
                    Layout.fillWidth: true

                    Label {
                        text: qsTr("转速设置 (tpp)")
                        font.pixelSize: 16
                        font.bold: true
                        color: "#2c2c2c"
                    }

                    SpinBox {
                        id: rpmSpin
                        Layout.fillWidth: true
                        from: 0
                        to: 200
                        stepSize: 1
                        value: window.rpmSetting
                        onValueModified: window.rpmSetting = value
                    }
                }

                ColumnLayout {
                    spacing: 6
                    Layout.fillWidth: true

                    Label {
                        text: qsTr("转速（度）")
                        font.pixelSize: 16
                        font.bold: true
                        color: "#2c2c2c"
                    }

                    SpinBox {
                        id: degreeSpin
                        Layout.fillWidth: true
                        from: 0
                        to: 360
                        stepSize: 1
                        value: window.degreeSetting
                        enabled: !window.running
                        onValueModified: window.degreeSetting = value
                    }
                }

                ColumnLayout {
                    spacing: 6
                    Layout.fillWidth: true

                    Label {
                        text: qsTr("锯条数 (3-12)")
                        font.pixelSize: 16
                        font.bold: true
                        color: "#2c2c2c"
                    }

                    SpinBox {
                        id: spokeSpin
                        Layout.fillWidth: true
                        from: 3
                        to: 12
                        stepSize: 1
                        value: window.spokeCount
                        onValueModified: window.spokeCount = value
                    }
                }

                ColumnLayout {
                    spacing: 6
                    Layout.fillWidth: true

                    Label {
                        text: qsTr("采样频率 (Hz)")
                        font.pixelSize: 16
                        font.bold: true
                        color: "#2c2c2c"
                    }

                    SpinBox {
                        id: sampleSpin
                        Layout.fillWidth: true
                        from: 1
                        to: 120
                        stepSize: 1
                        value: window.sampleRate
                        onValueModified: window.sampleRate = value
                    }
                }

                Frame {
                    Layout.fillWidth: true
                    Layout.fillHeight: true
                    background: Rectangle {
                        radius: 6
                        color: "#ffffff"
                        border.color: "#d7d8dd"
                    }

                    ColumnLayout {
                        anchors.fill: parent
                        anchors.margins: 12
                        spacing: 8

                        Label {
                            text: qsTr("操作笔记")
                            font.pixelSize: 15
                            color: "#444444"
                        }

                        TextArea {
                            id: noteArea
                            Layout.fillWidth: true
                            Layout.fillHeight: true
                            wrapMode: Text.WordWrap
                            placeholderText: qsTr("输入测试记录或备注…")
                            text: window.noteText
                            onTextChanged: window.noteText = text
                        }
                    }
                }

                RowLayout {
                    Layout.fillWidth: true
                    spacing: 12

                    Button {
                        text: qsTr("保存笔记")
                        Layout.fillWidth: true
                        onClicked: window.noteText = noteArea.text
                    }

                    Button {
                        text: window.running ? qsTr("暂停") : qsTr("开始/旋转")
                        Layout.fillWidth: true
                        onClicked: window.running = !window.running
                    }
                }

                Rectangle {
                    Layout.fillWidth: true
                    height: 1
                    color: "#d7d8dd"
                }

                Label {
                    Layout.fillWidth: true
                    text: window.running ? qsTr("车轮旋转中") : qsTr("车轮待机")
                    horizontalAlignment: Text.AlignHCenter
                    font.pixelSize: 14
                    color: window.running ? "#2d8f3f" : "#555555"
                    padding: 8
                    background: Rectangle {
                        color: window.running ? "#e4f4e9" : "transparent"
                        radius: 6
                    }
                }
            }
        }

        Rectangle {
            id: contentArea
            Layout.fillWidth: true
            Layout.fillHeight: true
            color: "#ffffff"

            WheelDisplay {
                id: wheelDisplay
                anchors.centerIn: parent
                width: Math.min(parent.width, parent.height) * 0.85
                height: width
                rotationSpeed: window.rpmSetting
                spokeCount: window.spokeCount
                running: window.running
                manualAngle: window.degreeSetting
                hubColor: "#8828b4"
                rimColor: "#101010"
                spokeColor: "#101010"
                discColor: "#ffffff"
            }

            Rectangle {
                width: 240
                height: 90
                anchors.right: parent.right
                anchors.bottom: parent.bottom
                anchors.margins: 24
                radius: 12
                color: "#f5f5f7"
                border.color: "#d7d8dd"

                Column {
                    anchors.centerIn: parent
                    spacing: 6
                    Label {
                        text: qsTr("采样频率：%1 Hz").arg(window.sampleRate)
                        font.pixelSize: 14
                        color: "#4a4a4a"
                    }
                    Label {
                        text: qsTr("当前角度：%1°").arg(Math.round(wheelDisplay.currentAngle))
                        font.pixelSize: 14
                        color: "#4a4a4a"
                    }
                }
            }

            Connections {
                target: wheelDisplay
                function onCurrentAngleChanged() {
                    if (window.running) {
                        window.degreeSetting = wheelDisplay.currentAngle
                    }
                }
            }
        }
    }
}
