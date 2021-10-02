using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

namespace TimerForApps
{
    public partial class Graph : Form
    {
        LogsFinder lgf;
        //private int count = 0;//for count days
        public Graph(LogsFinder lgf1)
        {
            InitializeComponent();
            lgf = lgf1;
            lgf.Graphop = true;
        }
        public void addseries(string series, bool add)
        {
            if (!add)
            {
                chart1.Series.Clear();
                chart1.Legends.Clear();
            }
            try
            {
                chart1.Series.Add(series);
                chart1.Legends.Add(series);
            }
            catch
            {
                MessageBox.Show(@"This graph already exist");
            }
            
        }

        private int last_week = 0;
        public void draw(int y, string date)
        {
            int li = chart1.Series.Count - 1; // index of series
            date = date.Replace('_', '.');//replace _ with . in date
            DateTime dt = DateTime.Parse(date);
            int x = dt.DayOfYear;
            chart1.Series[li].Points.AddXY(x,y/60.0);//fixed problem with int value
            //chart1.Series[li].Points[chart1.Series[li].Points.Count - 1].AxisLabel = "date";
            //chart1.Series[li].AxisLabel.la
            //int week = (x-Delta_monday_of_new_year(dt.Year)) / 7;
            Calendar cal = new CultureInfo("en-US").Calendar;
            int week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            if (last_week < week)
            {
                last_week = week;
                chart1.Series[li].Points[chart1.Series[li].Points.Count - 1].Label = date;
            }
            
        }

        private int Delta_monday_of_new_year(int year)
        {
            int delta=4;
            DateTime dt = new DateTime(year,1,1);
            if (dt.DayOfWeek == DayOfWeek.Tuesday)
            {
                delta = 5;
            }
            else if(dt.DayOfWeek == DayOfWeek.Wednesday)
            {
                delta = 6;
            }
            else if(dt.DayOfWeek == DayOfWeek.Thursday)
            {
                delta = 7;
            }
            else if(dt.DayOfWeek == DayOfWeek.Friday)
            {
                delta = 8;
            }
            else if(dt.DayOfWeek == DayOfWeek.Saturday)
            {
                delta = 9;
            }
            else if(dt.DayOfWeek == DayOfWeek.Sunday)
            {
                delta = 10;
            }
            return delta;
        }

        private void Graph_FormClosed(object sender, FormClosedEventArgs e)
        {
            lgf.Graphop = false;
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int i =0;i < chart1.Series.Count; i++)
            {
                chart1.Series[i].ChartType = SeriesChartType.Line;
            }
        }

        private void columnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].ChartType = SeriesChartType.Column;
            }
        }
    }
}
