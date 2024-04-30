using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part03_09
{
    class SectionArea
    {
        public static double CountArea(Point A, Point B)
        {
            double L = Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
            return (A.H + B.H - 2 * 10) * L / 2;
        }
    }
}
