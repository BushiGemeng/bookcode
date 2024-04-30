using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part03_09
{
    class InterPolation_H
    {
        private double Count_Dis(Point A, Point B)
        {
            return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
        }

        public double Count_H(Point[] points, Point Center_Point)
        {
            // 创建一个新的数组，与原始数组具有相同的长度
            Point[] temp = new Point[points.Length];

            // 将原始数组的内容复制到新数组中
            Array.Copy(points, temp, points.Length);

            double H = 0, P = 0;
            int i;

            for (i = 0; i < temp.Length; i++)
            {
                temp[i].dis = Count_Dis(temp[i], Center_Point);
            }

            Array.Sort(temp, (a, b) => a.dis.CompareTo(b.dis));

            int startIndex = temp[0].dis == 0 ? 1 : 0; // 如果第一个点为自身，从第二个开始插值
            for (i = startIndex; i < startIndex + 5; i++)
            {
                H += temp[i].H / temp[i].dis;
                P += 1 / temp[i].dis;
            }

            return H / P;
        }
    }
}
