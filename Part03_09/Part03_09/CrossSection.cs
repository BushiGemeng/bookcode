using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part03_09
{
    class CrossSection
    {
        public List<List<Point>> InterMP(Point[] points)
        {
            // 存储关键点
            List<Point> KeyPoints = new List<Point>();
            foreach (Point point in points)
                if (point.Id.Contains('K'))
                    KeyPoints.Add(point);

            InterPolation_H iph = new InterPolation_H();

            List<List<Point>> Point2d = new List<List<Point>>(); // 每个横断面的点为一行List<Point>数据
            for (int i = 0; i < KeyPoints.Count - 1; i++) 
            {
                List<Point> InterPoints = new List<Point>();
                double Az = Azimuth_Angle.Count_Az(KeyPoints[i], KeyPoints[i + 1]) + Math.PI / 2;
                double mx = (KeyPoints[i].X + KeyPoints[i + 1].X) / 2, my = (KeyPoints[i].Y + KeyPoints[i + 1].Y) / 2; // 计算中点坐标
                // 对横断面的各个点插值
                for (int j = -5; j <= 5; j++) //  需要插值11个点，j = 0 时为中点
                {
                    double ip_x = mx + j * Math.Cos(Az) * 5, ip_y = my + j * Math.Sin(Az) * 5;
                    char c = j == 0 ? 'M' : 'P';
                    Point point = new Point((6 - j).ToString() + '_' + c, ip_x, ip_y);
                    point.H = iph.Count_H(points, point);
                    InterPoints.Add(point);
                }

                InterPoints.Sort((A, B) =>
                {
                    if (A.X - B.X < 0) return -1;
                    else return 1;
                });

                Point2d.Add(InterPoints);
            }

            return Point2d;
        }

        public double Count_Area(List<Point> points)
        {
            double Area = 0;

            for (int i = 0; i < points.Count - 1; i++) Area += SectionArea.CountArea(points[i], points[i + 1]);

            return Area;
        }
    }
}
