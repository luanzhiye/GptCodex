using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WheelSpeedWPF.Controls;

public partial class NumericUpDown : UserControl
{
    public event RoutedPropertyChangedEventHandler<double>? ValueChanged;

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(double), typeof(NumericUpDown),
        new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged, CoerceValue));

    public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
        nameof(Minimum), typeof(double), typeof(NumericUpDown), new PropertyMetadata(0d, OnLimitChanged));

    public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
        nameof(Maximum), typeof(double), typeof(NumericUpDown), new PropertyMetadata(100d, OnLimitChanged));

    public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register(
        nameof(Increment), typeof(double), typeof(NumericUpDown), new PropertyMetadata(1d));

    public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(
        nameof(DecimalPlaces), typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, OnDecimalPlacesChanged));

    public static readonly DependencyProperty WheelIncrementProperty = DependencyProperty.Register(
        nameof(WheelIncrement), typeof(double), typeof(NumericUpDown), new PropertyMetadata(1d));

    public NumericUpDown()
    {
        InitializeComponent();
        ValueBox.Text = FormatValue(Value);
        UpButton.Click += (_, _) => ChangeValue(Increment);
        DownButton.Click += (_, _) => ChangeValue(-Increment);
        ValueBox.PreviewTextInput += ValueBoxOnPreviewTextInput;
        ValueBox.LostFocus += (_, _) => ValidateText();
        ValueBox.PreviewKeyDown += ValueBoxOnPreviewKeyDown;
        ValueBox.MouseWheel += ValueBoxOnMouseWheel;
        DataObject.AddPastingHandler(ValueBox, ValueBoxOnPasting);
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public double Increment
    {
        get => (double)GetValue(IncrementProperty);
        set => SetValue(IncrementProperty, value);
    }

    public int DecimalPlaces
    {
        get => (int)GetValue(DecimalPlacesProperty);
        set => SetValue(DecimalPlacesProperty, value);
    }

    public double WheelIncrement
    {
        get => (double)GetValue(WheelIncrementProperty);
        set => SetValue(WheelIncrementProperty, value);
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is NumericUpDown control)
        {
            control.ValueBox.Text = control.FormatValue(control.Value);
            control.ValueChanged?.Invoke(control, new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue));
        }
    }

    private static object CoerceValue(DependencyObject d, object basevalue)
    {
        if (d is NumericUpDown control)
        {
            double value = (double)basevalue;
            if (value < control.Minimum)
            {
                value = control.Minimum;
            }
            if (value > control.Maximum)
            {
                value = control.Maximum;
            }
            return Math.Round(value, control.DecimalPlaces);
        }
        return basevalue;
    }

    private static void OnLimitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is NumericUpDown control)
        {
            control.Value = Math.Min(Math.Max(control.Value, control.Minimum), control.Maximum);
        }
    }

    private static void OnDecimalPlacesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is NumericUpDown control)
        {
            control.Value = Math.Round(control.Value, control.DecimalPlaces);
            control.ValueBox.Text = control.FormatValue(control.Value);
        }
    }

    private void ChangeValue(double delta)
    {
        Value = Math.Min(Math.Max(Value + delta, Minimum), Maximum);
    }

    private void ValueBoxOnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var fullText = ValueBox.Text.Remove(ValueBox.SelectionStart, ValueBox.SelectionLength).Insert(ValueBox.SelectionStart, e.Text);
        e.Handled = !IsValidInput(fullText);
    }

    private void ValueBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Up)
        {
            ChangeValue(Increment);
            e.Handled = true;
        }
        else if (e.Key == Key.Down)
        {
            ChangeValue(-Increment);
            e.Handled = true;
        }
        else if (e.Key == Key.Enter)
        {
            ValidateText();
        }
    }

    private void ValueBoxOnMouseWheel(object sender, MouseWheelEventArgs e)
    {
        ChangeValue(e.Delta > 0 ? WheelIncrement : -WheelIncrement);
        e.Handled = true;
    }

    private void ValueBoxOnPasting(object sender, DataObjectPastingEventArgs e)
    {
        if (!e.DataObject.GetDataPresent(typeof(string)))
        {
            e.CancelCommand();
            return;
        }

        var pastedText = (string)e.DataObject.GetData(typeof(string))!;
        var fullText = ValueBox.Text.Remove(ValueBox.SelectionStart, ValueBox.SelectionLength)
            .Insert(ValueBox.SelectionStart, pastedText);
        if (!IsValidInput(fullText))
        {
            e.CancelCommand();
        }
    }

    private void ValidateText()
    {
        if (double.TryParse(ValueBox.Text, NumberStyles.Float, CultureInfo.CurrentCulture, out var value))
        {
            Value = value;
        }
        else
        {
            ValueBox.Text = FormatValue(Value);
        }
    }

    private bool IsValidInput(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return true;
        }

        if (text == "-" && Minimum < 0)
        {
            return true;
        }

        return double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out _);
    }

    private string FormatValue(double value)
    {
        string format = "F" + Math.Max(0, DecimalPlaces);
        return value.ToString(format, CultureInfo.CurrentCulture);
    }
}