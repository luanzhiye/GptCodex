using System;
using System.Windows;
using System.Windows.Media;
using WheelSpeedWPF.Rendering;

namespace WheelSpeedWPF.Controls;

public class WheelVisualizer : FrameworkElement
{
    public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
        nameof(Angle), typeof(double), typeof(WheelVisualizer), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty SpokesProperty = DependencyProperty.Register(
        nameof(Spokes), typeof(int), typeof(WheelVisualizer), new FrameworkPropertyMetadata(6, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty IsMarkerEnabledProperty = DependencyProperty.Register(
        nameof(IsMarkerEnabled), typeof(bool), typeof(WheelVisualizer), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

    protected override Size MeasureOverride(Size availableSize)
    {
        return availableSize;
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        double width = ActualWidth;
        double height = ActualHeight;
        if (width <= 0 || height <= 0)
        {
            return;
        }

        double centerX = width / 2d;
        double centerY = height / 2d;
        double maxR = width > height ? height * 8d / 20d : width * 7d / 20d;
        double hubR = maxR / 4d;

        var penWheel = new Pen(Brushes.Black, 24);
        penWheel.Freeze();

        var hubBrush = Brushes.Purple;
        var calc = new PointsCalc(centerX, centerY, maxR);
        double angleRad = Angle * Math.PI / 180d;
        int spokes = Math.Max(1, Spokes);

        for (int i = 0; i < spokes; i++)
        {
            Brush brush = Brushes.Black;
            if (IsMarkerEnabled && i == 0)
            {
                brush = Brushes.Blue;
            }

            var points = calc.CalcPentagon(angleRad, 20d * Math.PI / 180d, 30);
            var geometry = new StreamGeometry();
            using (var context = geometry.Open())
            {
                context.BeginFigure(points[0], true, true);
                context.PolyLineTo(points[1..], true, true);
            }
            geometry.Freeze();
            drawingContext.DrawGeometry(brush, null, geometry);
            angleRad += 2d * Math.PI / spokes;
        }

        drawingContext.DrawEllipse(null, penWheel, new Point(centerX, centerY), maxR, maxR);
        drawingContext.DrawEllipse(hubBrush, null, new Point(centerX, centerY), hubR, hubR);
    }

    public double Angle
    {
        get => (double)GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }

    public int Spokes
    {
        get => (int)GetValue(SpokesProperty);
        set => SetValue(SpokesProperty, value);
    }

    public bool IsMarkerEnabled
    {
        get => (bool)GetValue(IsMarkerEnabledProperty);
        set => SetValue(IsMarkerEnabledProperty, value);
    }
}