#include "mainwindow.h"

#include "ui_mainwindow.h"
#include "wheelwidget.h"

#include <QAction>
#include <QCheckBox>
#include <QComboBox>
#include <QMessageBox>
#include <QPushButton>
#include <QSpinBox>
#include <QStatusBar>
#include <QTimer>
#include <QStringList>
#include <cmath>

namespace {
const QStringList kFrequencyOptions{
    QStringLiteral("1"),   QStringLiteral("2"),   QStringLiteral("2.5"), QStringLiteral("4"),
    QStringLiteral("5"),   QStringLiteral("8"),   QStringLiteral("10"),  QStringLiteral("12.5"),
    QStringLiteral("20"),  QStringLiteral("25"),  QStringLiteral("40"),  QStringLiteral("50"),
    QStringLiteral("100")};

const QStringList kDeltaOptions{QStringLiteral("1"),   QStringLiteral("2"),   QStringLiteral("3"),
                                QStringLiteral("4"),   QStringLiteral("5"),   QStringLiteral("6"),
                                QStringLiteral("7"),   QStringLiteral("8"),   QStringLiteral("9"),
                                QStringLiteral("10"),  QStringLiteral("12"),  QStringLiteral("15"),
                                QStringLiteral("20"),  QStringLiteral("25"),  QStringLiteral("30"),
                                QStringLiteral("40"),  QStringLiteral("50"),  QStringLiteral("60"),
                                QStringLiteral("80"),  QStringLiteral("100")};
}

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
    , m_timer(new QTimer(this))
    , m_angle(0.0)
    , m_rpm(0.0)
    , m_frequency(25.0)
    , m_running(false)
{
    ui->setupUi(this);

    statusBar()->showMessage(tr("车轮停转中"));

    ui->comboFrequency->clear();
    ui->comboFrequency->addItems(kFrequencyOptions);
    ui->comboDeltaRpm->clear();
    ui->comboDeltaRpm->addItems(kDeltaOptions);

    ui->spinRpm->setRange(-4500, 4500);
    ui->spinSpokes->setRange(3, 12);

    connect(ui->actionToggle, &QAction::triggered, this, &MainWindow::toggleRunning);
    connect(ui->actionAbout, &QAction::triggered, this, &MainWindow::showAbout);
    connect(ui->buttonToggle, &QPushButton::clicked, this, &MainWindow::toggleRunning);
    connect(ui->comboFrequency, &QComboBox::currentIndexChanged, this, &MainWindow::updateFrequency);
    connect(ui->comboDeltaRpm, &QComboBox::currentIndexChanged, this, &MainWindow::updateDelta);
    connect(ui->spinRpm, qOverload<int>(&QSpinBox::valueChanged), this, &MainWindow::updateRpm);
    connect(ui->spinSpokes, qOverload<int>(&QSpinBox::valueChanged), this, &MainWindow::updateSpokes);
    connect(ui->checkColorMark, &QCheckBox::stateChanged, this, &MainWindow::updateColorMark);
    connect(m_timer, &QTimer::timeout, this, &MainWindow::updateAnimation);

    ui->comboDeltaRpm->setCurrentIndex(9);
    ui->comboFrequency->setCurrentIndex(9);
    ui->spinRpm->setValue(0);
    ui->spinSpokes->setValue(6);
    ui->checkColorMark->setChecked(false);

    updateDelta(ui->comboDeltaRpm->currentIndex());
    updateFrequency(ui->comboFrequency->currentIndex());
    syncWheel();
    updateStatus();
}

MainWindow::~MainWindow() = default;

void MainWindow::toggleRunning()
{
    m_running = !m_running;
    if (m_running) {
        m_timer->start();
    } else {
        m_timer->stop();
    }
    updateStatus();
}

void MainWindow::updateAnimation()
{
    const double intervalSeconds = static_cast<double>(m_timer->interval()) / 1000.0;
    const double deltaAngle = m_rpm * intervalSeconds * 360.0 / 60.0;
    m_angle = std::fmod(m_angle + deltaAngle, 360.0);
    if (m_angle < 0.0) {
        m_angle += 360.0;
    }
    ui->wheelWidget->setAngle(m_angle);
}

void MainWindow::updateRpm(int value)
{
    m_rpm = static_cast<double>(value);
    m_angle = std::fmod(m_angle, 360.0);
    ui->wheelWidget->setAngle(m_angle);
    updateStatus();
}

void MainWindow::updateSpokes(int value)
{
    ui->wheelWidget->setSpokes(value);
    updateStatus();
}

void MainWindow::updateFrequency(int index)
{
    if (index < 0 || index >= kFrequencyOptions.size()) {
        return;
    }

    bool ok = false;
    const double freq = kFrequencyOptions.at(index).toDouble(&ok);
    if (!ok || freq <= 0.0) {
        return;
    }

    m_frequency = freq;
    const int intervalMs = static_cast<int>(std::round(1000.0 / m_frequency));
    m_timer->setInterval(intervalMs);
    if (m_running) {
        m_timer->start();
    }
    updateStatus();
}

void MainWindow::updateDelta(int index)
{
    if (index < 0 || index >= kDeltaOptions.size()) {
        return;
    }

    bool ok = false;
    const int delta = kDeltaOptions.at(index).toInt(&ok);
    if (!ok) {
        return;
    }

    ui->spinRpm->setSingleStep(delta);
}

void MainWindow::updateColorMark(int state)
{
    const bool enabled = (state == Qt::Checked);
    ui->wheelWidget->setColorMarked(enabled);
}

void MainWindow::showAbout()
{
    QMessageBox::about(this, tr("关于"),
                       tr("车轮旋转视觉效果模拟程序\n"));
}

void MainWindow::syncWheel()
{
    ui->wheelWidget->setSpokes(ui->spinSpokes->value());
    ui->wheelWidget->setAngle(m_angle);
    ui->wheelWidget->setColorMarked(ui->checkColorMark->isChecked());
}

void MainWindow::updateStatus()
{
    if (m_running) {
        statusBar()->showMessage(tr("车轮转动中"));
        ui->actionToggle->setText(tr("停转车轮"));
        ui->buttonToggle->setText(tr("停转车轮"));
    } else {
        statusBar()->showMessage(tr("车轮停转中"));
        ui->actionToggle->setText(tr("转动车轮"));
        ui->buttonToggle->setText(tr("转动车轮"));
    }
}