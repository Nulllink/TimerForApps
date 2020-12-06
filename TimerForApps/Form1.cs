using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace TimerForApps
{
    //using HWND = IntPtr;
    public partial class Form1 : Form
    {
        bool file = false;
        public bool lgopen = false;
        bool savelogs = true;
        public bool lisopen = false;
        public bool setopen = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listView1.Items.Clear();
            try
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt");
                string line = sr.ReadToEnd();
                if (line.IndexOf("====WhiteList====") >= 0 && line.IndexOf("====BlackList====") >= 0)
                {
                    file = true;
                }
                else
                {
                    MessageBox.Show("Lists.txt file is corrapted");
                }
                sr.Close();
            }
            catch
            {
                //MessageBox.Show("Where is no Lists.txt file in base directory");
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt", false);
                sw.WriteLine("====WhiteList====");
                sw.WriteLine("====BlackList====");
                sw.WriteLine("====ControlList====");
                sw.Close();
            }
            timer1.Enabled = true;
            ListAdd2();
            toolStripStatusLabel2.Text = DateTime.Now.ToString();//.Replace('.', '_');//DateTime.Now.ToLongTimeString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ListAdd2();
            int hours, minutes, seconds;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                //open
                //if (listView1.Items[i].SubItems[2].Text == "true")
                if (listView1.Items[i].ForeColor == Color.Green)
                {
                    string[] time = listView1.Items[i].SubItems[1].Text.Split(':');
                    hours = Convert.ToInt32(time[0]);
                    minutes = Convert.ToInt32(time[1]);
                    seconds = Convert.ToInt32(time[2]);
                    seconds += 10;
                    if (seconds == 60)
                    {
                        seconds = 0;
                        minutes++;
                        if (minutes == 60)
                        {
                            minutes = 0;
                            hours++;
                        }
                    }
                    listView1.Items[i].SubItems[1].Text = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
                }
            }
        }

        private void ListAdd2()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                //open
                //listView1.Items[i].SubItems[2].Text = "false";
                listView1.Items[i].ForeColor = Color.Blue;
            }
            List<ProcessWindow> pw = winproc.Procfind(file);
            foreach (ProcessWindow text in pw)
            {
                bool win = false;
                for (int i = 0; i < listView1.Items.Count; i++)
                {

                    if (listView1.Items[i].SubItems[2].Text == text.ProcessName)
                    {
                        win = true;
                        //open
                        //listView1.Items[i].SubItems[2].Text = "true";
                        listView1.Items[i].ForeColor = Color.Green;
                        listView1.Items[i].SubItems[0].Text = text.WindowTitle;
                    }
                    //else if (listView1.Items[i].SubItems[0].Text == text.WindowTitle)
                    //{
                    //    win = true;
                    //    listView1.Items[i].SubItems[2].Text = "true";
                    //    listView1.Items[i].SubItems[3].Text = text.ProcessName;
                    //}
                }
                if (win == false)
                {
                    ListViewItem lvi = new ListViewItem(text.WindowTitle);
                    lvi.SubItems.Add("0:0:0");
                    //open
                    //lvi.SubItems.Add("true");
                    lvi.ForeColor = Color.Green;
                    lvi.SubItems.Add(text.ProcessName);
                    lvi.SubItems.Add(DateTime.Now.TimeOfDay.ToString().Substring(0, 8));
                    listView1.Items.Add(lvi);
                }

            }
            //WinSum();
        }

        //ListAdd
        #region
        //private void ListAdd()
        //{
        //    for (int i = 0; i < listView1.Items.Count; i++)
        //    {
        //        listView1.Items[i].SubItems[2].Text = "false";
        //    }
        //    IDictionary<HWND, string> dw;
        //    dw = OpenWindowGetter.GetOpenWindows();
        //    foreach (KeyValuePair<HWND, string> text in dw)
        //    {
        //        bool win = false;
        //        for (int i = 0; i < listView1.Items.Count; i++)
        //        {

        //            if (listView1.Items[i].SubItems[3].Text == text.Key.ToString())
        //            {
        //                win = true;
        //                listView1.Items[i].SubItems[2].Text = "true";
        //                listView1.Items[i].SubItems[0].Text = text.Value;
        //            }
        //            else if (listView1.Items[i].SubItems[0].Text == text.Value)
        //            {
        //                win = true;
        //                listView1.Items[i].SubItems[2].Text = "true";
        //                listView1.Items[i].SubItems[3].Text = text.Key.ToString();
        //            }
        //        }
        //        if (win == false)
        //        {
        //            bool black = false;
        //            if (file == true)
        //            {
        //                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt");
        //                string line;
        //                while ((line = sr.ReadLine()) != null)
        //                {
        //                    if (line == text.Value)
        //                    {
        //                        black = true;
        //                        break;
        //                    }
        //                }
        //                sr.Close();
        //            }
        //            if (black == false || file == false)
        //            {
        //                ListViewItem lvi = new ListViewItem(text.Value);
        //                lvi.SubItems.Add("0:0:0");
        //                lvi.SubItems.Add("true");
        //                lvi.SubItems.Add(text.Key.ToString());
        //                lvi.SubItems.Add(DateTime.Now.TimeOfDay.ToString().Substring(0, 8));
        //                listView1.Items.Add(lvi);
        //                //listView1.Items.a

        //            }
        //        }

        //    }
        //    //WinSum();
        //}
        #endregion
        //WinSum
        #region
        //private void WinSum()
        //{
        //    int hours, minutes, seconds;
        //    for (int i = 0; i < listView1.Items.Count; i++)
        //    {
        //        for (int j = i + 1; j < listView1.Items.Count; j++)
        //        {
        //            if (listView1.Items[i].SubItems[0].Text == listView1.Items[j].SubItems[0].Text)
        //            {
        //                string[] time = listView1.Items[i].SubItems[1].Text.Split(':');
        //                hours = Convert.ToInt32(time[0]);
        //                minutes = Convert.ToInt32(time[1]);
        //                seconds = Convert.ToInt32(time[2]);
        //                time = listView1.Items[j].SubItems[1].Text.Split(':');
        //                hours += Convert.ToInt32(time[0]);
        //                minutes += Convert.ToInt32(time[1]);
        //                seconds += Convert.ToInt32(time[2]);
        //                if (seconds >= 60)
        //                {
        //                    minutes++;
        //                    seconds -= 60;
        //                }
        //                if (minutes >= 60)
        //                {
        //                    minutes -= 60;
        //                    hours++;
        //                }
        //                listView1.Items[j].SubItems[1].Text = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
        //                DeleteItem(i);
        //                i = listView1.Items.Count;
        //                break;
        //            }
        //        }
        //    }
        //}
        #endregion
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savelogs)
            {
                try
                {
                    string date = toolStripStatusLabel2.Text; //DateTime.Now.ToString().Replace('.', '_');
                    //date = date.Replace(':', '#');
                    string path = AppDomain.CurrentDomain.BaseDirectory + "TimerLogs\\" + date.Substring(3, 7).Replace('.', '_') + ".txt";
                     
                    StreamWriter sw = new StreamWriter(path, true);
                    sw.WriteLine($"{date}=={toolStripStatusLabel1.Text}==Windows total count {listView1.Items.Count} {this.Text}");
                    int ww = 0;
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        DateTime time;
                        time = DateTime.Parse(listView1.Items[i].SubItems[1].Text);
                        DateTime time1 = new DateTime(1, 1, 1, 0, 5, 0);
                        //string[] time = listView1.Items[i].SubItems[1].Text.Split(':');
                        if (time1.TimeOfDay <= time.TimeOfDay)
                        {
                            for (int j = 0; j < listView1.Items[0].SubItems.Count; j++)
                            {
                                sw.Write(listView1.Items[i].SubItems[j].Text + "< >");
                            }
                            sw.WriteLine();
                            ww++;
                        }
                    }
                    sw.WriteLine("Windows write count: " + ww.ToString());
                    sw.Close();
                }
                catch
                {
                    MessageBox.Show("Fail to write in file");
                }
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void LogsFinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lgopen)
            {
                LogsFinder lg = new LogsFinder(this);
                lgopen = true;
                lg.Show();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int i = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                DeleteItem(i);
            }
        }
        void DeleteItem(int i)
        {
            for (int c = i + 1; c < listView1.Items.Count; c++)
            {
                listView1.Items[c - 1].SubItems[0].Text = listView1.Items[c].SubItems[0].Text;
                listView1.Items[c - 1].SubItems[1].Text = listView1.Items[c].SubItems[1].Text;
                //open
                //listView1.Items[c - 1].SubItems[2].Text = listView1.Items[c].SubItems[2].Text;
                listView1.Items[c - 1].SubItems[2].Text = listView1.Items[c].SubItems[2].Text;
                listView1.Items[c - 1].SubItems[3].Text = listView1.Items[c].SubItems[3].Text;
            }
            listView1.Items.RemoveAt(listView1.Items.Count - 1);
        }

        //private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (timer1.Enabled)
        //    {
        //        timer1.Enabled = false;
        //        toolStripStatusLabel3.Text = "Pause: True";
        //        //pauseToolStripMenuItem.Text = "PauseT";
        //    }
        //    else
        //    {
        //        timer1.Enabled = true;
        //        toolStripStatusLabel3.Text = "Pause: False";
        //    }
        //}

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                List<ProcessWindow> pw = winproc.Procfind(file);
                for (int i = 0; i < pw.Count; i++)
                {
                    if (pw[i].ProcessName == listView1.Items[index].SubItems[2].Text)
                    {
                        pw[i].Process.Kill();
                        break;
                    }
                }
                toolStripStatusLabel6.Text = "App killed";
            }
        }

        private void saveLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (savelogs)
            {
                savelogs = false;
                toolStripStatusLabel4.Text = "SaveLogs: False";
            }
            else
            {
                savelogs = true;
                toolStripStatusLabel4.Text = "SaveLogs: True";
            }
        }

        int h=0, m=0, s=0;

        private void listsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lisopen)
            {
                Lists lis = new Lists(file,this);
                lisopen = true;
                lis.Show();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!setopen)
            {
                Settings s = new Settings();
                setopen = true;
                s.Show();
            }
            
        }

        private void inBlackListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file)
            {
                string name = listView1.FocusedItem.SubItems[0].Text;

                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt");
                string oldfile = sr.ReadToEnd();
                sr.Close();
                int indexC = oldfile.LastIndexOf("====ControlList====");
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt", false);
                string oldfile1 = oldfile.Substring(0, indexC);
                string oldfile2 = oldfile.Substring(indexC, oldfile.Length - indexC);
                sw.Write(oldfile1);
                sw.WriteLine(name);
                sw.Write(oldfile2);

                sw.Close();
                toolStripStatusLabel6.Text = "App added to black list";
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            s ++;
            if (s == 60)
            {
                s = 0;
                m++;
                if (m == 60)
                {
                    m = 0;
                    h++;
                }
            }
            toolStripStatusLabel1.Text = h.ToString() + ":" + m.ToString() + ":" + s.ToString();
            toolStripStatusLabel5.Text = "Count: " + listView1.Items.Count.ToString();

        }
    } 
}

