using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part03_09
{
    class Azimuth_Angle
    {
        public static double Count_Az(Point A, Point B)
        {
            double Delta_X = B.X - A.X, Delta_Y = B.Y - A.Y;
            double Az = Math.Atan(Delta_Y / Delta_X);

            if (Delta_X < 0) Az += Math.PI;
            else if (Delta_X > 0 && Delta_Y < 0) Az += Math.PI * 2;

            return Az;
        }
    }
}
