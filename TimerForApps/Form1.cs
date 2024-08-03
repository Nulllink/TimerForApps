using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace TimerForApps
{
    public partial class Form1 : Form
    {
        #region Global params
        private bool _file;
        public bool Lgopen;
        private bool _savelogs = true;
        public bool Lisopen;
        private LogsFinder lg;
        private bool _search = false;
        Settings fSettings = new Settings();
        List<bool> sBools;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            sBools = fSettings.Check_boxes_states();//get settings
            Starting();
        }

        private void Starting()
        {
            listView1.Items.Clear();
            _s = 0;//seconds
            _m = 0;//minutes
            _h = 0;//hours
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + $"TimerLogs/{Program.config["pc_name"]}");
            //Checking file Lists.txt
            try
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt");
                string line = sr.ReadToEnd();
                //if specific lines exist
                if (line.IndexOf("====WhiteList====", StringComparison.Ordinal) >= 0 && line.IndexOf("====BlackList====", StringComparison.Ordinal) >= 0)
                {
                    _file = true;
                }
                else
                {
                    MessageBox.Show(@"Lists.txt file is corrupted");
                }
                sr.Close();
            }
            //creating file List.txt
            catch
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt", false);
                sw.WriteLine("====WhiteList====");
                sw.WriteLine("====BlackList====");
                sw.WriteLine("====ControlList====");
                sw.Close();
            }
            bool found =false;
            if (sBools[1])
            {
                found = continue_day();
            }
            if (!found)
            {
                ListAdd2();
                toolStripStatusLabel2.Text = DateTime.Now.ToString();//.Replace('.', '_');//DateTime.Now.ToLongTimeString();
            }
            timer1.Enabled = true;
        }

        private bool continue_day()
        {
            string month = $"0{DateTime.Today.Month}";
            month = month.Substring(month.Length - 2, 2);
            bool found = false;
            string path = $"TimerLogs/{Program.config["pc_name"]}/{DateTime.Today.Year}_{month}.txt"; //fix
            if(File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                int found_index = -1;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (!found)
                    {
                        string[] current = lines[i].Split(new[] {"=="}, StringSplitOptions.None);
                        if (current.Length == 3)
                        {
                            if (DateTime.Parse(current[0]) >= DateTime.Today)
                            {
                                toolStripStatusLabel2.Text = current[0];
                                string[] work_time = current[1].Split(':');
                                _h = Int32.Parse(work_time[0]);
                                _m = Int32.Parse(work_time[1]);
                                _s = Int32.Parse(work_time[2]);
                                found = true;
                                found_index = i;
                            }
                        }
                    }
                    else
                    {
                        string[] current = lines[i].Split(new[] {"< >"}, StringSplitOptions.None);
                        if (current.Length > 1)
                        {
                            ListViewItem lvi = new ListViewItem(current[0]); //window name
                            lvi.SubItems.Add(current[1]); //working time
                            lvi.ForeColor = Color.Blue; //color
                            lvi.SubItems.Add(current[2]); //process name
                            lvi.SubItems.Add(current[3]); //open time
                            listView1.Items.Add(lvi); //adding item
                        }
                    }
                }

                if (found)
                {
                    Array.Resize(ref lines, found_index);
                    File.WriteAllLines(path, lines);
                }
            }
            return found;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            TimeSpan? timeSpan = TimeSpan.Parse("00:05:00");
            if (PersonWork.GetInactiveTime() < timeSpan || !sBools[2])
            {
                if (toolStripStatusLabel6.Text == @"You are AFK now")
                {
                    toolStripStatusLabel6.Text = @"You are here";
                    timer3.Start();
                }
                ListAdd2();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].ForeColor == Color.Green)
                    {
                        string[] time = listView1.Items[i].SubItems[1].Text.Split(':');
                        var hours = Convert.ToInt32(time[0]);
                        var minutes = Convert.ToInt32(time[1]);
                        var seconds = Convert.ToInt32(time[2]);
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

                        listView1.Items[i].SubItems[1].Text = $@"{hours}:{minutes}:{seconds}";
                    }
                }
            }
            else
            {
                toolStripStatusLabel6.Text = @"You are AFK now";
            }
        }

        /// <summary>
        /// Making green items in list or add new process
        /// </summary>
        private void ListAdd2()
        {
            //make all items blue
            for (int i = 0; i < listView1.Items.Count; i++) 
            {
                listView1.Items[i].ForeColor = Color.Blue;
            }
            List<ProcessWindow> pw = winproc.ProcessFind(_file);

            //processing list of processes
            foreach (ProcessWindow text in pw)
            {
                bool win = false;//exist or not exist process
                //walk throw list view to find already existing process
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    //if process name equivalent
                    if (listView1.Items[i].SubItems[2].Text == text.ProcessName)
                    {
                        win = true;//process exist
                        listView1.Items[i].ForeColor = Color.Green;//making item green
                        listView1.Items[i].SubItems[0].Text = text.WindowTitle;//update window name
                    }
                    
                }
                //if process don't exist in list
                if (win == false)
                {
                    ListViewItem lvi = new ListViewItem(text.WindowTitle); //window name
                    lvi.SubItems.Add("0:0:0"); //working time
                    lvi.ForeColor = Color.Green; //color
                    lvi.SubItems.Add(text.ProcessName); //process name
                    lvi.SubItems.Add(DateTime.Now.TimeOfDay.ToString().Substring(0, 8)); //open time
                    listView1.Items.Add(lvi); //adding item
                }
            }
        }

        
        #region ListAdd
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
            Saving();
        }

        private void Saving()
        {
            if (_savelogs)
            {
                Infobox ibform = new Infobox();
                ibform.bar_settings(listView1.Items.Count);
                ibform.write("Saving");
                ibform.Show();
                try
                {
                    //listView1.Items[0].SubItems[1].Text = "40:40:40";
                    string date = toolStripStatusLabel2.Text; //DateTime.Now.ToString().Replace('.', '_');

                    //date = date.Replace(':', '#');
                    string path = AppDomain.CurrentDomain.BaseDirectory + $"TimerLogs\\{Program.config["pc_name"]}\\{date.Substring(6, 4)}_{date.Substring(3, 2)}.txt"; //fix
                     
                    StreamWriter sw = new StreamWriter(path, true);
                    sw.WriteLine($"{date}=={toolStripStatusLabel1.Text}==Windows total count {listView1.Items.Count} {this.Text}");
                    int ww = 0;
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        var time = listView1.Items[i].SubItems[1].Text.Split(':');
                        if (int.Parse(time[0])>0 || int.Parse(time[1])>5)
                        {
                            for (int j = 0; j < listView1.Items[0].SubItems.Count; j++)
                            {
                                sw.Write(listView1.Items[i].SubItems[j].Text + "< >");
                            }
                            sw.WriteLine();
                            ww++;
                        }
                        ibform.plus_bar(1);
                    }
                    sw.WriteLine("Windows write count: " + ww.ToString());
                    sw.Close();
                }
                catch
                {
                    MessageBox.Show(@"Fail to write in file");
                }
                ibform.Close();
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
            if (!Lgopen)
            {
                lg = new LogsFinder(this);
                Lgopen = true;
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
            if (listView1.SelectedItems.Count <= 0) return;
            var index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
            var pw = winproc.ProcessFind(_file);
            foreach (var t in pw)
            {
                if (t.ProcessName == listView1.Items[index].SubItems[2].Text)
                {
                    t.Process.Kill();
                    break;
                }
            }
            toolStripStatusLabel6.Text = @"App killed";
            timer3.Start();
        }

        private void saveLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_savelogs)
            {
                _savelogs = false;
                toolStripStatusLabel4.Text = @"SaveLogs: False";
            }
            else
            {
                _savelogs = true;
                toolStripStatusLabel4.Text = @"SaveLogs: True";
            }
        }

        private int _h, _m, _s;

        private void listsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Lisopen)
            {
                Lists lis = new Lists(_file,this);
                Lisopen = true;
                lis.Show();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSettings.Show();
        }

        private void inBlackListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_file)
            {
                string name = listView1.FocusedItem.SubItems[0].Text;

                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt");
                string oldfile = sr.ReadToEnd();
                sr.Close();
                int indexC = oldfile.LastIndexOf("====ControlList====", StringComparison.Ordinal);
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt", false);
                string oldfile1 = oldfile.Substring(0, indexC);
                string oldfile2 = oldfile.Substring(indexC, oldfile.Length - indexC);
                sw.Write(oldfile1);
                sw.WriteLine(name);
                sw.Write(oldfile2);

                sw.Close();
                toolStripStatusLabel6.Text = @"App added to black list";
                timer3.Start();
            }
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_search)
            {
                if (!Lgopen)
                {
                    lg = new LogsFinder(this);
                    Lgopen = true;
                    lg.Show();
                }

                lg.Controls["comboBox1"].Text = listView1.FocusedItem.SubItems[2].Text;
                lg.findProcess();
            }
        }

        

        private void notifyIcon1_BalloonTipShown(object sender, MouseEventArgs e)
        {
            notifyIcon1.Text = toolStripStatusLabel1.Text;
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_search)
            {
                _search = false;
                toolStripStatusLabel3.Text = @"Search: False";
            }
            else
            {
                _search = true;
                toolStripStatusLabel3.Text = @"Search: True";
            }
        }

        private bool started = false;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!started)
            {
                WindowState = FormWindowState.Minimized;
                started = true;
            }
            if (DateTime.Today > DateTime.Parse(toolStripStatusLabel2.Text) && sBools[0])
            {
                Saving();
                Starting();
                toolStripStatusLabel6.Text = @"New day";
                timer3.Start();
            }
            _s ++;
            if (_s == 60)
            {
                _s = 0;
                _m++;
                if (_m == 60)
                {
                    _m = 0;
                    _h++;
                }
            }
            toolStripStatusLabel1.Text = $@"{_h}:{_m}:{_s}";
            
            //toolStripStatusLabel5.Text = $@"Count: {listView1.Items.Count}";

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel6.Text = "";
            timer3.Stop();
        }
    } 
}

