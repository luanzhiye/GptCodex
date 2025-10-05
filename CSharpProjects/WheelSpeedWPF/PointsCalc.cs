using System;
using System.Linq;
using System.Numerics;
using System.Windows;

namespace WheelSpeedWPF.Rendering;

public class PointsCalc
{
    public PointsCalc(double x, double y, double radius)
    {
        Center = new Complex(x, y);
        Radius = radius;
    }

    public Complex Center { get; set; }

    public double Radius { get; set; }

    public Point[] CalcPentagon(double centerAngle, double widthAngle, double hubWidth)
    {
        if (hubWidth < 2)
        {
            hubWidth = 2;
        }

        var result = new Complex[5];
        result[0] = Center + Complex.FromPolarCoordinates(hubWidth / 2d, centerAngle + Math.PI / 4);
        result[1] = Center + Complex.FromPolarCoordinates(hubWidth / 2d, centerAngle - Math.PI / 4);
        result[2] = Center + Complex.FromPolarCoordinates(Radius, centerAngle - widthAngle / 2);
        result[3] = Center + Complex.FromPolarCoordinates(Radius, centerAngle);
        result[4] = Center + Complex.FromPolarCoordinates(Radius, centerAngle + widthAngle / 2);
        return result.Select(p => new Point(p.Real, p.Imaginary)).ToArray();
    }
}