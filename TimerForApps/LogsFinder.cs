using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace TimerForApps
{
    public partial class LogsFinder : Form
    {
        private readonly Form1 _f1P;
        private int _rowname;
        public bool Graphop = false;
        private Graph _g;
        public LogsFinder(Form1 f1)
        {
            InitializeComponent();
            _f1P = f1;
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\History.txt", true);
            sw.Close();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _rowname = 0;
            Find();
        }

        private void LogsFinder_FormClosed(object sender, FormClosedEventArgs e)
        {
            _f1P.Lgopen = false;
        }

        private bool Branch(string name,string[] spline)
        {
            if (_rowname == 0)
            {
                if (-1 < spline[0].Replace(" ", "").ToLower().IndexOf(name, StringComparison.Ordinal))
                    return true;
            }
            else if (_rowname==2)
            {
                //open
                if (name == spline[2].ToLower().Replace(" ", ""))
                    return true;
            }
            
            return false;
        }

        void Find()
        {
            
            List<string> files = new List<string>();
            string path = AppDomain.CurrentDomain.BaseDirectory + "TimerLogs";
            string name = comboBox1.Text.Replace(" ", "").ToLower();
            bool iffind = false;
            int allhours = 0;
            int allminutes = 0;
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (var item in dir.GetFiles())
            {
                string date = item.Name.Substring(0, 7);
                //DateTime dateps = dateTimePicker1.Value;
                //DateTime datepf = dateTimePicker2.Value;
                
                DateTime datec = DateTime.ParseExact(date, "MM_yyyy", null);
                var dateci = datec.Year * 100 + datec.Month;
                var dateps = dateTimePicker1.Value.Year * 100 + dateTimePicker1.Value.Month;
                var datepf = dateTimePicker2.Value.Year * 100 + dateTimePicker2.Value.Month;
                if (dateci >= dateps && dateci <= datepf)
                {
                    files.Add(item.FullName);
                }

            }
            listView1.Items.Clear();
            foreach (var t in files)
            {
                string date="";
                //string dateo = "";
                //bool blu = false;
                StreamReader sr = new StreamReader(t);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (name != "self")
                    {
                        var spline = line.Split(new[] { "< >" }, StringSplitOptions.RemoveEmptyEntries);
                        if (spline.Length > 2)
                        {

                            if (Branch(name, spline))
                            {
                                var trname = spline[0];
                                string[] splittime = spline[1].Split(':');
                                var hours = Convert.ToInt32(splittime[0]);
                                var minutes = Convert.ToInt32(splittime[1]);
                                allhours += hours;
                                allminutes += minutes;
                                if (allminutes >= 60)
                                {
                                    allminutes -= 60;
                                    allhours++;
                                }
                                ListViewItem lvi = new ListViewItem(trname);
                                lvi.SubItems.Add(hours.ToString());
                                lvi.SubItems.Add(minutes.ToString());
                                lvi.SubItems.Add(date);
                                lvi.SubItems.Add(spline[2]);
                                listView1.Items.Add(lvi);
                                //if (date != dateo)
                                //{
                                //    dateo = date;
                                //    if (blu)
                                //    {
                                //        listView1.Items[listView1.Items.Count - 1].ForeColor = Color.Blue;
                                //    }
                                //}
                                iffind = true;
                            }

                        }
                        else
                        {
                            date = spline[0].Substring(0, 10);
                        }
                    }
                    else
                    {
                        string[] spline = line.Split(new[] { "==" }, StringSplitOptions.RemoveEmptyEntries);
                        if (spline.Length > 1)
                        {
                            date = spline[0];
                            string[] sptime = spline[1].Split(':');
                            var hours = Convert.ToInt32(sptime[0]);
                            var minutes = Convert.ToInt32(sptime[1]);
                            allhours += hours;
                            allminutes += minutes;
                            if (allminutes >= 60)
                            {
                                allminutes -= 60;
                                allhours++;
                            }
                            ListViewItem lvi = new ListViewItem("Self");
                            lvi.SubItems.Add(hours.ToString());
                            lvi.SubItems.Add(minutes.ToString());
                            lvi.SubItems.Add(date);
                            listView1.Items.Add(lvi);
                            iffind = true;
                        }
                    }
                }
                sr.Close();
            }
            if (iffind)
            {
                toolStripStatusLabel1.Text = $@"Open Time: {allhours}h {allminutes}m";
                Historywrite();
                //ListViewItem lvi = new ListViewItem("OpenTime");
                //lvi.SubItems.Add(allhours.ToString());
                //lvi.SubItems.Add(allminutes.ToString());
                //listView1.Items.Add(lvi);
            }
        }

        private void findProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _rowname = 2;
            Find();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\History.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                comboBox1.Items.Add(line);
                if(comboBox1.Items.Count > 30)
                {
                    break;
                }
            }
            sr.Close();
        }
        private void Historywrite()
        {
            if (comboBox1.Text != "")
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\History.txt");
                string fil = sr.ReadToEnd();
                sr.Close();
                int i;
                if ((i = fil.IndexOf(comboBox1.Text.ToLower(), StringComparison.Ordinal)) >= 0)
                {
                    fil = fil.Remove(i, comboBox1.Text.Length + 2);
                }
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\History.txt", false);
                sw.Write(comboBox1.Text.ToLower() + "\r\n" + fil);
                sw.Close();
            }
            //if (fil.IndexOf(comboBox1.Text.ToLower()) < 0)
            //{
            //    StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\History.txt", false);
            //    sw.Write(comboBox1.Text.ToLower() + "\r\n" + fil);
            //    sw.Close();
            //}
        }

        private void createNewGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 1)
            {
                bool add = sender.ToString() == "Add as series to Graph";
                if (!Graphop)
                {
                    _g = new Graph(this);
                }
                _g.Show();
                _g.addseries(listView1.Items[0].SubItems[0].Text, add);
                int lastday = Convert.ToInt32(listView1.Items[0].SubItems[3].Text.Substring(0, 2));
                int y = Convert.ToInt32(listView1.Items[0].SubItems[1].Text) * 60 + Convert.ToInt32(listView1.Items[0].SubItems[2].Text);
                
                for (int i = 1; i <= listView1.Items.Count; i++)
                {
                    if (i == listView1.Items.Count)
                    {
                        _g.draw(y, listView1.Items[i - 1].SubItems[3].Text);
                    }
                    else
                    {
                        int nowday = Convert.ToInt32(listView1.Items[i].SubItems[3].Text.Substring(0, 2));
                        if (lastday != nowday)
                        {
                            _g.draw(y, listView1.Items[i - 1].SubItems[3].Text);
                            lastday = nowday;
                            y = Convert.ToInt32(listView1.Items[i].SubItems[1].Text) * 60 + Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                        }
                        else
                        {
                            y += Convert.ToInt32(listView1.Items[i].SubItems[1].Text) * 60 + Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                        }
                    }
                }
            }
        }
    }
}
