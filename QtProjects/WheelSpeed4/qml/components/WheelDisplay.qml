import QtQuick 2.15

Item {
    id: wheelRoot
    property real rotationSpeed: 0            // rotations per minute
    property bool running: false
    property real manualAngle: 0
    property int spokeCount: 6
    property color rimColor: "#101010"
    property color spokeColor: "#101010"
    property color discColor: "#ffffff"
    property color hubColor: "#8a2be2"

    readonly property real currentAngle: (wheelFace.rotation % 360 + 360) % 360

    implicitWidth: 420
    implicitHeight: 420

    readonly property real _degreesPerMillisecond: rotationSpeed * 360 / 60000

    Timer {
        id: rotationTimer
        interval: 16
        repeat: true
        running: wheelRoot.running && Math.abs(wheelRoot.rotationSpeed) > 0.01
        onTriggered: wheelFace.rotation = (wheelFace.rotation + _degreesPerMillisecond * interval) % 360
    }

    onRunningChanged: {
        if (!running) {
            wheelFace.rotation = manualAngle % 360;
        } else {
            rotationTimer.restart();
        }
    }

    onManualAngleChanged: {
        if (!running) {
            wheelFace.rotation = manualAngle % 360;
        }
    }

    onSpokeCountChanged: wheelCanvas.requestPaint();

    Component.onCompleted: wheelFace.rotation = manualAngle % 360;

    Rectangle {
        anchors.fill: parent
        color: "transparent"
    }

    Item {
        id: wheelFace
        anchors.centerIn: parent
        width: Math.min(parent.width, parent.height)
        height: width

        Canvas {
            id: wheelCanvas
            anchors.fill: parent
            antialiasing: true

            onPaint: {
                var ctx = getContext("2d");
                ctx.reset();

                var w = width;
                var h = height;
                var cx = w / 2;
                var cy = h / 2;
                var radius = Math.min(w, h) / 2;

                // outer rim
                ctx.beginPath();
                ctx.arc(cx, cy, radius, 0, Math.PI * 2, false);
                ctx.fillStyle = rimColor;
                ctx.fill();

                // inner disc
                ctx.beginPath();
                ctx.arc(cx, cy, radius * 0.9, 0, Math.PI * 2, false);
                ctx.fillStyle = discColor;
                ctx.fill();

                // spokes
                var spokeWidth = Math.PI / spokeCount * 0.6;
                ctx.fillStyle = spokeColor;
                for (var i = 0; i < spokeCount; ++i) {
                    var baseAngle = i * 2 * Math.PI / spokeCount;
                    ctx.beginPath();
                    ctx.moveTo(cx, cy);
                    var outerAngle = baseAngle - spokeWidth / 2;
                    ctx.lineTo(cx + Math.cos(outerAngle) * radius * 0.92,
                               cy + Math.sin(outerAngle) * radius * 0.92);
                    outerAngle = baseAngle + spokeWidth / 2;
                    ctx.lineTo(cx + Math.cos(outerAngle) * radius * 0.92,
                               cy + Math.sin(outerAngle) * radius * 0.92);
                    ctx.closePath();
                    ctx.fill();
                }

                // hub
                ctx.beginPath();
                ctx.arc(cx, cy, radius * 0.22, 0, Math.PI * 2, false);
                ctx.fillStyle = hubColor;
                ctx.fill();

                // hub highlight
                ctx.beginPath();
                ctx.arc(cx, cy, radius * 0.11, 0, Math.PI * 2, false);
                ctx.fillStyle = "#ffffff";
                ctx.fill();
            }

            onWidthChanged: requestPaint();
            onHeightChanged: requestPaint();
            onRimColorChanged: requestPaint();
            onDiscColorChanged: requestPaint();
            onSpokeColorChanged: requestPaint();
            onHubColorChanged: requestPaint();            
        }
    }
}
