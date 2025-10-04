#pragma once

#include <QWidget>
#include <QPolygonF>

class WheelWidget : public QWidget
{
    Q_OBJECT

public:
    explicit WheelWidget(QWidget *parent = nullptr);

    void setSpokes(int spokes);
    void setAngle(double angleDegrees);
    void setColorMarked(bool enabled);

protected:
    void paintEvent(QPaintEvent *event) override;

private:
    QPolygonF createSpoke(double angleDegrees, int centerX, int centerY, double radius) const;

    int m_spokes;
    double m_angle;
    bool m_colorMarked;
};
