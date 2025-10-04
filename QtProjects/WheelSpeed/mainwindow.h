#pragma once

#include <QMainWindow>

class QAction;
class QCheckBox;
class QComboBox;
class QSpinBox;
class QPushButton;
class QTimer;
class WheelWidget;

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);

private slots:
    void toggleRunning();
    void updateAnimation();
    void updateRpm(int value);
    void updateSpokes(int value);
    void updateFrequency(int index);
    void updateDelta(int index);
    void updateColorMark(int state);
    void showAbout();

private:
    void createUi();
    void syncWheel();
    void updateStatus();

    WheelWidget *m_wheelWidget;
    QTimer *m_timer;
    QComboBox *m_freqCombo;
    QComboBox *m_deltaCombo;
    QSpinBox *m_rpmSpin;
    QSpinBox *m_spokesSpin;
    QCheckBox *m_markCheck;
    QPushButton *m_toggleButton;
    QAction *m_toggleAction;

    double m_angle;
    double m_rpm;
    double m_frequency;
    bool m_running;
};