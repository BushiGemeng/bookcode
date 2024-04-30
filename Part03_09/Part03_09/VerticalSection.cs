using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part03_09
{
    /// <summary>
    /// Count_Len 函数，计算纵断面长度
    /// InterPoints函数，进行内插，返回一个Point的列表
    /// </summary>
    class VerticalSection
    {
        private double Count_Dis(Point A, Point B)
        {
            return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
        }

        public double Count_Len(Point[] points)
        {
            double Len = 0;

            List<Point> KeyPoints = new List<Point>();
            foreach (Point point in points)
                if (point.Id.Contains('K'))
                    KeyPoints.Add(point);

            for (int i = 0; i < KeyPoints.Count - 1; i++) { Len += Count_Dis(KeyPoints[i], KeyPoints[i + 1]); }

            return Len;
        }

        // 返回内插后中心线上的点列表
        public List<Point> InterPoints(Point[] points)
        {
            InterPolation_H interPolation_H = new InterPolation_H();

            List<Point> KeyPoints = new List<Point>();
            foreach (Point point in points)
                if (point.Id.Contains('K'))
                    KeyPoints.Add(point);

            List<Point> CenterPoints = new List<Point>();

            int Count = 0; // 记录一共内插多少个点

            for (int i = 0; i < KeyPoints.Count - 1; i++)
            {
                CenterPoints.Add(KeyPoints[i]);

                double dis = Count_Dis(KeyPoints[i], KeyPoints[0]);
                double Az = Azimuth_Angle.Count_Az(KeyPoints[i], KeyPoints[i + 1]);
                int n = (int)Math.Floor(Count_Dis(KeyPoints[i], KeyPoints[i + 1]) / 10); // 这一段上可以可以内插n个点

                for (int j = 0; j < n; j++)
                {
                    double current_dis = 10 * (++Count) - dis;
                    Point vp = new Point("V" + Count.ToString(), KeyPoints[i].X + Math.Cos(Az) * current_dis, KeyPoints[i].Y + Math.Sin(Az) * current_dis);
                    vp.H = interPolation_H.Count_H(points, vp);

                    CenterPoints.Add(vp);
                }
            }
            CenterPoints.Add(KeyPoints.Last());

            return CenterPoints;
        }

        public double Count_Area(List<Point> points)
        {
            double Area = 0;

            for (int i = 0; i < points.Count - 1; i++) { Area += SectionArea.CountArea(points[i], points[i + 1]); }

            return Area;
        }
    }
}
