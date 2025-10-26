#include "mainwindow.h"

#include "wheelwidget.h"

#include <QAction>
#include <QCheckBox>
#include <QComboBox>
#include <QHBoxLayout>
#include <QLabel>
#include <QMenuBar>
#include <QMessageBox>
#include <QPushButton>
#include <QStringList>
#include <QSpinBox>
#include <QSplitter>
#include <QStatusBar>
#include <QTimer>
#include <QToolBar>
#include <QVBoxLayout>
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
    , m_wheelWidget(new WheelWidget(this))
    , m_timer(new QTimer(this))
    , m_freqCombo(nullptr)
    , m_deltaCombo(nullptr)
    , m_rpmSpin(nullptr)
    , m_spokesSpin(nullptr)
    , m_markCheck(nullptr)
    , m_toggleButton(nullptr)
    , m_toggleAction(nullptr)
    , m_angle(0.0)
    , m_rpm(0.0)
    , m_frequency(25.0)
    , m_running(false)
{
    createUi();

    connect(m_timer, &QTimer::timeout, this, &MainWindow::updateAnimation);

    m_timer->setInterval(static_cast<int>(std::round(1000.0 / m_frequency)));
}

void MainWindow::createUi()
{
    setWindowTitle(tr("车轮旋转视觉效果"));

    m_toggleAction = new QAction(tr("转动车轮"), this);
    connect(m_toggleAction, &QAction::triggered, this, &MainWindow::toggleRunning);

    QMenu *operateMenu = menuBar()->addMenu(tr("操作"));
    operateMenu->addAction(m_toggleAction);

    QMenu *helpMenu = menuBar()->addMenu(tr("帮助"));
    helpMenu->addAction(tr("关于"), this, &MainWindow::showAbout);

    QToolBar *toolbar = addToolBar(tr("工具"));
    toolbar->addAction(m_toggleAction);

    QWidget *central = new QWidget(this);
    auto *layout = new QHBoxLayout(central);
    layout->setContentsMargins(0, 0, 0, 0);

    QSplitter *splitter = new QSplitter(Qt::Horizontal, central);
    layout->addWidget(splitter);

    QWidget *controls = new QWidget(splitter);
    auto *controlsLayout = new QVBoxLayout(controls);

    auto *deltaLabel = new QLabel(tr("转速增量(RPM)"), controls);
    m_deltaCombo = new QComboBox(controls);
    m_deltaCombo->addItems(kDeltaOptions);
    controlsLayout->addWidget(deltaLabel);
    controlsLayout->addWidget(m_deltaCombo);

    auto *rpmLabel = new QLabel(tr("转速 (RPM)："), controls);
    m_rpmSpin = new QSpinBox(controls);
    m_rpmSpin->setRange(-4500, 4500);
    controlsLayout->addWidget(rpmLabel);
    controlsLayout->addWidget(m_rpmSpin);

    auto *spokesLabel = new QLabel(tr("辐条数 (3-12)："), controls);
    m_spokesSpin = new QSpinBox(controls);
    m_spokesSpin->setRange(3, 12);
    controlsLayout->addWidget(spokesLabel);
    controlsLayout->addWidget(m_spokesSpin);

    auto *freqLabel = new QLabel(tr("采样频率 (Hz)"), controls);
    m_freqCombo = new QComboBox(controls);
    m_freqCombo->addItems(kFrequencyOptions);
    controlsLayout->addWidget(freqLabel);
    controlsLayout->addWidget(m_freqCombo);

    m_markCheck = new QCheckBox(tr("颜色标记"), controls);
    controlsLayout->addWidget(m_markCheck);

    m_toggleButton = new QPushButton(tr("转动车轮"), controls);
    controlsLayout->addWidget(m_toggleButton);

    controlsLayout->addStretch(1);

    splitter->addWidget(controls);
    splitter->addWidget(m_wheelWidget);
    splitter->setStretchFactor(1, 1);

    setCentralWidget(central);

    statusBar()->showMessage(tr("车轮停转中"));

    connect(m_deltaCombo, &QComboBox::currentIndexChanged, this, &MainWindow::updateDelta);
    connect(m_rpmSpin, qOverload<int>(&QSpinBox::valueChanged), this, &MainWindow::updateRpm);
    connect(m_spokesSpin, qOverload<int>(&QSpinBox::valueChanged), this, &MainWindow::updateSpokes);
    connect(m_freqCombo, &QComboBox::currentIndexChanged, this, &MainWindow::updateFrequency);
    connect(m_markCheck, &QCheckBox::stateChanged, this, &MainWindow::updateColorMark);
    connect(m_toggleButton, &QPushButton::clicked, this, &MainWindow::toggleRunning);

    m_deltaCombo->setCurrentIndex(9);
    m_freqCombo->setCurrentIndex(9);
    m_rpmSpin->setValue(0);
    m_spokesSpin->setValue(6);
    m_markCheck->setChecked(false);

    syncWheel();
}

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
    m_wheelWidget->setAngle(m_angle);
}

void MainWindow::updateRpm(int value)
{
    m_rpm = static_cast<double>(value);
    m_angle = std::fmod(m_angle, 360.0);
    m_wheelWidget->setAngle(m_angle);
    updateStatus();
}

void MainWindow::updateSpokes(int value)
{
    m_wheelWidget->setSpokes(value);
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

    m_rpmSpin->setSingleStep(delta);
}

void MainWindow::updateColorMark(int state)
{
    const bool enabled = (state == Qt::Checked);
    m_wheelWidget->setColorMarked(enabled);
}

void MainWindow::showAbout()
{
    QMessageBox::about(this, tr("关于"),
                       tr("车轮旋转视觉效果模拟程序\n"));
}

void MainWindow::syncWheel()
{
    m_wheelWidget->setSpokes(m_spokesSpin->value());
    m_wheelWidget->setAngle(m_angle);
    m_wheelWidget->setColorMarked(m_markCheck->isChecked());
}

void MainWindow::updateStatus()
{
    if (m_running) {
        statusBar()->showMessage(tr("车轮转动中"));
        m_toggleAction->setText(tr("停转车轮"));
        m_toggleButton->setText(tr("停转车轮"));
    } else {
        statusBar()->showMessage(tr("车轮停转中"));
        m_toggleAction->setText(tr("转动车轮"));
        m_toggleButton->setText(tr("转动车轮"));
    }
}
