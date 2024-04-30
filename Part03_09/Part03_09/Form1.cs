using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Part03_09
{
    /// <summary>
    /// VerticalSection类，实现纵断面的各种计算
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        Point[] points;

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文件(*.txt) |*.txt";
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName; // 获取文件路径
            if (path != "") LoadData(path);
        }

        private void LoadData(string path) // 将数据存储并可视化
        {
            string[] Lines = File.ReadAllLines(path);
            DataTable dataTable = new DataTable();
            points = new Point[Lines.Length - 5];

            string[] headers = new string[] { "点号", "X(m)", "Y(m)", "H(m)" };
            foreach (string header in headers) { dataTable.Columns.Add(header); }

            for (int i = 5; i < Lines.Length; i++)
            {
                string[] elements = Lines[i].Split(',');
                points[i - 5] = new Point(elements[0], double.Parse(elements[1]), double.Parse(elements[2]), double.Parse(elements[3]));
                dataTable.Rows.Add(elements);
            }

            ViewData.DataSource = dataTable;
            ViewData.ReadOnly = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (points != null)
            {
                Count_verticalSection();
                tabControl.SelectTab(1);
            }
            else MessageBox.Show("未加载数据");
        }

        private void Count_verticalSection()
        {
            VerticalSection verticalSection = new VerticalSection();
            List<Point> CenterLinePoints = verticalSection.InterPoints(points);

            DataTable res = new DataTable();
            string[] headers = new string[] { "点号", "X(m)", "Y(m)", "H(m)" };
            foreach (string header in headers) { res.Columns.Add(header); }
            foreach (Point point in CenterLinePoints) { res.Rows.Add(point.Id, point.X.ToString("0.000"), point.Y.ToString("0.000"), point.H.ToString("0.000")); }

            Double Len = verticalSection.Count_Len(points), Area = verticalSection.Count_Area(CenterLinePoints);

            Vertical_Len.Text = Len.ToString("0.000");
            Vertical_Area.Text = Area.ToString("0.000");

            Vertical_Res.DataSource = res;
            Vertical_Res.ReadOnly = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (points != null) Count_crossSection();
            else MessageBox.Show("未加载数据");
        }

        private void Count_crossSection()
        {
            tabControl.SelectTab(2);
            TabPage targetTabPage = tabControl.SelectedTab;

            CrossSection crossSection = new CrossSection();
            List<List<Point>> point2d = crossSection.InterMP(points);
            string[] headers = new string[] { "点号", "X(m)", "Y(m)", "H(m)" };
            int count = 0;
            foreach(List<Point> points in point2d)
            {
                richTextBox2.Text += "*********************中心点M" + (++count).ToString() + "*********************\n";
                richTextBox2.Text += "横断面积： " + crossSection.Count_Area(points).ToString("0.000") + '\n';
                for (int i = 0; i < points.Count; i++) richTextBox2.Text += points[i].Id + " " + points[i].X.ToString("0.000") + " " + points[i].Y.ToString("0.000") + ' ' + points[i].H.ToString("0.000") + '\n';
            }
            richTextBox2.ReadOnly = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "1.文件读取点位数据从文本第六行开始读取，关键点位请使用'K'开头命名\n";
            richTextBox1.Text += "2.点位插值选取最近的5个点为的高程进行插值，断面面积计算以10m为基准面\n";
            richTextBox1.Text += "3.纵断面以中心点为起点，向两端插值五个点，点距选取 Delta = 5m\n";
            richTextBox1.Text += "4.对该程序有任何疑问或改进意见，请联系开发者***";
            richTextBox1.ReadOnly = true;
        }
    }
}
