using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;

namespace TimerForApps
{
    public partial class Settings : Form
    {
        // The path to the key where Windows looks for startup applications
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public Settings()
        {
            InitializeComponent();

            Point p = new Point(0, 25);
            panel1.Location = p;
            panel2.Location = p;
            panel3.Location = p;

            if (rkApp.GetValue("TimerApps") == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                checkBox1.Checked = false;
            }
            else
            {
                // The value exists, the application is set to run at startup
                checkBox1.Checked = true;
            }

            if (!File.Exists("Settings.txt"))
            {
                string[] new_settings = { "auto_new_day=True"};
                File.WriteAllLines("Settings.txt",new_settings);
            }
            string[] settings = File.ReadAllLines("Settings.txt");
            checkBox2.Checked = Convert.ToBoolean(settings[0].Split('=')[1]);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue("TimerApps", Application.ExecutablePath);
            }
            else
            {
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue("TimerApps", false);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string[] strings = File.ReadAllLines("Settings.txt");
            string[] line = strings[0].Split('=');
            strings[0] = $"{line[0]}={checkBox2.Checked}";
            File.WriteAllLines("Settings.txt",strings);
        }

        public List<bool> Check_boxes_states()
        {
            List<bool> states = new List<bool>();
            states.Add(checkBox2.Checked);
            return states;
        }

        private void closeWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
