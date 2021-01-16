using System;
using System.Windows.Forms;
using System.IO;
using System.Timers;

namespace TimerForApps
{
    public partial class Lists : Form
    {
        bool file;
        Form1 f;
        public Lists(bool file1, Form1 f1)
        {
            InitializeComponent();
            file = file1;
            f = f1;
        }

        private void whiteListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            //bool white = true;
            int listc = 0;
            if (sender.ToString() == "Black List")
            {
                //white = false;
                listc = 1;
            }
            else if (sender.ToString() == "Control List")
            {
                listc = 2;
            }
            if (file)
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt");
                string line;
                bool black = false;
                bool controll = false;
                while ((line = sr.ReadLine()) != null)
                {
                    
                    if (listc == 0)
                    {
                        if (line == "====BlackList====")
                        {
                            break;
                        }
                        ListViewItem lvi = new ListViewItem(line);
                        listView1.Items.Add(lvi);
                    }
                    else if (listc == 1)
                    {
                        if (line == "====ControlList====")
                        {
                            break;
                        }
                        if (line == "====BlackList====")
                        {
                            black = true;
                        }
                        if (black)
                        {
                            ListViewItem lvi = new ListViewItem(line);
                            listView1.Items.Add(lvi);
                        }
                    }
                    else if (listc == 2)
                    {
                        if (line == "====ControlList====")
                        {
                            controll = true;
                        }
                        if (controll)
                        {
                            ListViewItem lvi = new ListViewItem(line);
                            listView1.Items.Add(lvi);
                        }
                    }
                }
                sr.Close();
            }
        }

        private void Lists_FormClosed(object sender, FormClosedEventArgs e)
        {
            f.Lisopen = false;
        }

        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file)
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt");
                string oldfile = sr.ReadToEnd();
                sr.Close();
                int indexB = oldfile.LastIndexOf("====BlackList====");
                int indexC = oldfile.LastIndexOf("====ControlList====");
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt", false);
                if (listView1.Items[0].SubItems[0].Text == "====BlackList====")
                {
                    string oldfile1 = oldfile.Substring(0, indexB);
                    string oldfile2 = oldfile.Substring(indexC, oldfile.Length - indexC);
                    sw.Write(oldfile1);
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        sw.WriteLine(listView1.Items[i].SubItems[0].Text);
                    }
                    sw.Write(oldfile2);
                }
                else if (listView1.Items[0].SubItems[0].Text == "====ControlList====")
                {
                    oldfile = oldfile.Substring(0, indexC);
                    sw.Write(oldfile);
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        sw.WriteLine(listView1.Items[i].SubItems[0].Text);
                    }
                }
                else
                {
                    oldfile = oldfile.Substring(indexB, oldfile.Length - indexB);
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        sw.WriteLine(listView1.Items[i].SubItems[0].Text);
                    }
                    sw.Write(oldfile);
                }
                sw.Close();
                toolStripStatusLabel1.Text = @"List updated";
                timer1.Start();
            }
        }

        private void addAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = new ListViewItem("Enter your app here!");
            listView1.Items.Add(lvi);
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
            }
            listView1.Items.RemoveAt(listView1.Items.Count - 1);
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            toolStripStatusLabel1.Text = @"";
            timer1.Stop();
        }
    }
}
