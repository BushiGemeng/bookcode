using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part03_09
{
    class Point
    {
        public string Id;
        public double X;
        public double Y;
        public double H;
        public double dis;

        public Point(string id, double x, double y, double h)
        {
            Id = id;
            X = x;
            Y = y;
            H = h;
        }

        public Point(string id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }

    }
}
