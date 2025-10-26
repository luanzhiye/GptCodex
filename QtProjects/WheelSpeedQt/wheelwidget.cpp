#include "wheelwidget.h"

#include <QPainter>
#include <QBrush>
#include <QPen>
#include <QtMath>

WheelWidget::WheelWidget(QWidget *parent)
    : QWidget(parent)
    , m_spokes(6)
    , m_angle(0.0)
    , m_colorMarked(false)
{
    setMinimumSize(200, 200);
    setAutoFillBackground(true);
}

void WheelWidget::setSpokes(int spokes)
{
    if (spokes < 3) {
        spokes = 3;
    }
    if (m_spokes != spokes) {
        m_spokes = spokes;
        update();
    }
}

void WheelWidget::setAngle(double angleDegrees)
{
    if (!qFuzzyCompare(1.0 + m_angle, 1.0 + angleDegrees)) {
        m_angle = angleDegrees;
        update();
    }
}

void WheelWidget::setColorMarked(bool enabled)
{
    if (m_colorMarked != enabled) {
        m_colorMarked = enabled;
        update();
    }
}

void WheelWidget::paintEvent(QPaintEvent *event)
{
    Q_UNUSED(event);

    QPainter painter(this);
    painter.setRenderHint(QPainter::Antialiasing, true);

    painter.fillRect(rect(), Qt::white);

    const int width = this->width();
    const int height = this->height();
    const int centerX = width / 2;
    const int centerY = height / 2;
    const int maxR = (width > height) ? height * 8 / 20 : width * 7 / 20;
    const int littleR = maxR / 4;

    QPen rimPen(QColor(0, 0, 0), 24, Qt::SolidLine, Qt::RoundCap, Qt::RoundJoin);
    QBrush hubBrush(QColor(128, 0, 128));

    double angle = m_angle;
    for (int i = 0; i < m_spokes; ++i) {
        const QColor spokeColor = (m_colorMarked && i == 0) ? QColor(0, 0, 255) : QColor(0, 0, 0);
        painter.setBrush(spokeColor);
        painter.setPen(Qt::NoPen);
        const QPolygonF spoke = createSpoke(angle, centerX, centerY, maxR);
        painter.drawPolygon(spoke);
        angle += 360.0 / static_cast<double>(m_spokes);
    }

    painter.setBrush(Qt::NoBrush);
    painter.setPen(rimPen);
    painter.drawEllipse(QPointF(centerX, centerY), maxR, maxR);

    painter.setBrush(hubBrush);
    painter.setPen(Qt::NoPen);
    painter.drawEllipse(QPointF(centerX, centerY), littleR, littleR);
}

QPolygonF WheelWidget::createSpoke(double angleDegrees, int centerX, int centerY, double radius) const
{
    const double centerAngle = qDegreesToRadians(angleDegrees);
    const double widthAngle = qDegreesToRadians(20.0);
    const double widthPixel = 30.0;

    auto polarPoint = [&](double r, double angle) {
        const double x = centerX + r * std::cos(angle);
        const double y = centerY + r * std::sin(angle);
        return QPointF(x, y);
    };

    QPolygonF polygon(5);
    polygon[0] = polarPoint(widthPixel / 2.0, centerAngle + M_PI_4);
    polygon[1] = polarPoint(widthPixel / 2.0, centerAngle - M_PI_4);
    polygon[2] = polarPoint(radius, centerAngle - widthAngle / 2.0);
    polygon[3] = polarPoint(radius, centerAngle);
    polygon[4] = polarPoint(radius, centerAngle + widthAngle / 2.0);

    return polygon;
}
