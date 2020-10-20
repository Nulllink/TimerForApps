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

namespace TimerForApps
{
    public partial class Graph : Form
    {
        LogsFinder lgf;
        public Graph(LogsFinder lgf1)
        {
            InitializeComponent();
            lgf = lgf1;
            lgf.graphop = true;
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
                MessageBox.Show("This graph already exist");
            }
            
        }
        public void draw(int y, string date)
        {
            int li = chart1.Series.Count - 1;
            date = date.Replace('_', '.');
            DateTime dt = DateTime.Parse(date);
           
            int x = dt.DayOfYear;
            chart1.Series[li].Points.AddXY(x,y);
            //chart1.Series[li].Points[chart1.Series[li].Points.Count - 1].AxisLabel = "date";
            //chart1.Series[li].AxisLabel.la
            chart1.Series[li].Points[chart1.Series[li].Points.Count - 1].Label = date;
        }

        private void Graph_FormClosed(object sender, FormClosedEventArgs e)
        {
            lgf.graphop = false;
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
