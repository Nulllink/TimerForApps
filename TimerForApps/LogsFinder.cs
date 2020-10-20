using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Drawing;

namespace TimerForApps
{
    public partial class LogsFinder : Form
    {
        Form1 f1p;
        int rowname;
        public bool graphop = false;
        Graph g;
        public LogsFinder(Form1 f1)
        {
            InitializeComponent();
            f1p = f1;
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\History.txt", true);
            sw.Close();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rowname = 0;
            Find();
        }

        private void LogsFinder_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1p.lgopen = false;
        }

        bool branch(string name,string[] spline)
        {
            if (rowname == 0)
            {
                if (-1 < spline[0].Replace(" ", "").ToLower().IndexOf(name))
                    return true;
            }
            else if (rowname==2)
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
            string trname;
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
                int dateci = datec.Year * 100 + datec.Month;
                int dateps = dateTimePicker1.Value.Year * 100 + dateTimePicker1.Value.Month;
                int datepf = dateTimePicker2.Value.Year * 100 + dateTimePicker2.Value.Month;
                if (dateci >= dateps && dateci <= datepf)
                {
                    files.Add(item.FullName);
                }

            }
            listView1.Items.Clear();
            for (int i = 0; i < files.Count; i++)
            {
                string date="";
                //string dateo = "";
                //bool blu = false;
                StreamReader sr = new StreamReader(files[i]);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (name != "self")
                    {
                        string[] spline = line.Split(new string[] { "< >" }, StringSplitOptions.RemoveEmptyEntries);
                        if (spline.Length > 2)
                        {

                            if (branch(name, spline))
                            {
                                int hours;
                                int minutes;
                                trname = spline[0];
                                string[] sptime = spline[1].Split(':');
                                hours = Convert.ToInt32(sptime[0]);
                                minutes = Convert.ToInt32(sptime[1]);
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
                        string[] spline = line.Split(new string[] { "==" }, StringSplitOptions.RemoveEmptyEntries);
                        if (spline.Length > 1)
                        {
                            date = spline[0];
                            int hours;
                            int minutes;
                            string[] sptime = spline[1].Split(':');
                            hours = Convert.ToInt32(sptime[0]);
                            minutes = Convert.ToInt32(sptime[1]);
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
                toolStripStatusLabel1.Text = $"Open Time: {allhours}h {allminutes}m";
                historywrite();
                //ListViewItem lvi = new ListViewItem("OpenTime");
                //lvi.SubItems.Add(allhours.ToString());
                //lvi.SubItems.Add(allminutes.ToString());
                //listView1.Items.Add(lvi);
            }
        }

        private void findProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rowname = 2;
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
        private void historywrite()
        {
            if (comboBox1.Text != "")
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\History.txt");
                string fil = sr.ReadToEnd();
                sr.Close();
                int i;
                if ((i = fil.IndexOf(comboBox1.Text.ToLower())) >= 0)
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
                bool add = false;
                if (sender.ToString() == "Add as series to Graph")
                {
                    add = true;
                }
                if (!graphop)
                {
                    g = new Graph(this);
                }
                g.Show();
                g.addseries(listView1.Items[0].SubItems[0].Text, add);
                int lastday = Convert.ToInt32(listView1.Items[0].SubItems[3].Text.Substring(0, 2));
                int y = Convert.ToInt32(listView1.Items[0].SubItems[1].Text) * 60 + Convert.ToInt32(listView1.Items[0].SubItems[2].Text);
                
                for (int i = 1; i <= listView1.Items.Count; i++)
                {
                    if (i == listView1.Items.Count)
                    {
                        g.draw(y, listView1.Items[i - 1].SubItems[3].Text);
                    }
                    else
                    {
                        int nowday = Convert.ToInt32(listView1.Items[i].SubItems[3].Text.Substring(0, 2));
                        if (lastday != nowday)
                        {
                            g.draw(y, listView1.Items[i - 1].SubItems[3].Text);
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
