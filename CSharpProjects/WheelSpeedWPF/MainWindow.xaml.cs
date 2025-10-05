using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WheelSpeedWPF.Controls;

namespace WheelSpeedWPF;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer _timer;
    private double _angle;
    private double _rpm;
    private double _frequency = 25;
    private bool _isRunning;

    public MainWindow()
    {
        InitializeComponent();
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000 / _frequency)
        };
        _timer.Tick += TimerOnTick;

        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        RpmControl.Value = 0;
        SpokesControl.Value = 6;
        FrequencyCombo.SelectedIndex = 4; // 25 Hz
        DeltaCombo.SelectedIndex = 2; // 10 RPM per wheel increment
        MarkerCheckBox.IsChecked = false;
        UpdateWheel();
    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        _angle += _rpm * _timer.Interval.TotalSeconds * 360d / 60d;
        _angle %= 360d;
        UpdateWheel();
    }

    private void UpdateWheel()
    {
        WheelDisplay.Angle = _angle;
        WheelDisplay.Spokes = (int)SpokesControl.Value;
        WheelDisplay.IsMarkerEnabled = MarkerCheckBox.IsChecked == true;
    }

    private void RunStopButton_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleRunning();
    }

    private void ToggleRunning()
    {
        _isRunning = !_isRunning;
        if (_isRunning)
        {
            _timer.Start();
            RunStopIcon.Text = "❚❚";
            RunStopText.Text = "暂停";
            RunStopMenuItem.Header = "停止";
            StatusText.Text = "车轮转动中";
        }
        else
        {
            _timer.Stop();
            RunStopIcon.Text = "▶";
            RunStopText.Text = "启动";
            RunStopMenuItem.Header = "运行";
            StatusText.Text = "车轮停转中";
        }
    }

    private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void AboutMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        var about = new AboutWindow
        {
            Owner = this
        };
        about.ShowDialog();
    }

    private void RpmControl_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        _rpm = e.NewValue;
        if (!_isRunning)
        {
            _angle = _angle % 360d;
            UpdateWheel();
        }
    }

    private void SpokesControl_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdateWheel();
    }

    private void FrequencyCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!IsLoaded)
        {
            return;
        }

        if (FrequencyCombo.SelectedItem is ComboBoxItem item && double.TryParse(item.Content.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var freq))
        {
            _frequency = freq;
            _timer.Interval = TimeSpan.FromMilliseconds(1000 / _frequency);
        }
    }

    private void DeltaCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!IsLoaded)
        {
            return;
        }

        if (DeltaCombo.SelectedItem is ComboBoxItem item && double.TryParse(item.Content.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var delta))
        {
            RpmControl.WheelIncrement = delta;
            RpmControl.Increment = delta;
        }
    }

    private void MarkerCheckBox_OnChecked(object sender, RoutedEventArgs e)
    {
        UpdateWheel();
    }
}