#pragma once

#include <QMainWindow>
#include <memory>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);
    ~MainWindow() override;
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

    void syncWheel();
    void updateStatus();

    std::unique_ptr<Ui::MainWindow> ui;
    QTimer *m_timer;


    double m_angle;
    double m_rpm;
    double m_frequency;
    bool m_running;
};
