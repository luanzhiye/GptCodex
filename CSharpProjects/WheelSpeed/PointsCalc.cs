using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace WheelSpeed
{
    public class PointsCalc
    {
        public Complex Center { get; set; }
        public double R { get; set; }

        public PointsCalc(int x, int y, int r)
        {
            Center = new Complex(x, y);
            R = r;
        }

        /// <summary>
        /// draw pentagon
        /// </summary>
        /// <param name="centerAngle"></param>
        /// <param name="widthAngle"></param>
        /// <param name="widthPixel"></param>
        /// <returns></returns>
        public Point[] CalcPentagon(double centerAngle, double widthAngle, int widthPixel)
        {
            if (widthPixel < 2)
            {
                widthPixel = 2;
            }
            Point[] result = new Point[5];
            Complex p1 = Center + Complex.FromPolarCoordinates(widthPixel/2d, centerAngle + Math.PI/4);
            Complex p2 = Center + Complex.FromPolarCoordinates(widthPixel/2d, centerAngle - Math.PI/4);
            Complex p3 = Center + Complex.FromPolarCoordinates(R, centerAngle - widthAngle/2);
            Complex p4 = Center + Complex.FromPolarCoordinates(R, centerAngle);
            Complex p5 = Center + Complex.FromPolarCoordinates(R, centerAngle + widthAngle/2);


            result[0] = new Point((int) p1.Real, (int) p1.Imaginary);
            result[1] = new Point((int) p2.Real, (int) p2.Imaginary);
            result[2] = new Point((int) p3.Real, (int) p3.Imaginary);
            result[3] = new Point((int) p4.Real, (int) p4.Imaginary);
            result[4] = new Point((int) p5.Real, (int) p5.Imaginary);


            return result;
        }
    }
}

