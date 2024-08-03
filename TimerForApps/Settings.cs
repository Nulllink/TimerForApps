using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Text;

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
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Settings.txt";
            if (!File.Exists(path))
            {
                update_settings();
            }
            string[] settings = File.ReadAllLines(path);
            if (settings.Length != 3) //checking on number of rows
            {
                update_settings();
                settings = File.ReadAllLines(path);
            }
            checkBox2.Checked = Convert.ToBoolean(settings[0].Split('=')[1]);
            checkBox3.Checked = Convert.ToBoolean(settings[1].Split('=')[1]);
            checkBox4.Checked = Convert.ToBoolean(settings[2].Split('=')[1]);
        }

        private void update_settings()
        {
            string[] new_settings = { "auto_new_day=True","continue_in_1_day=True","afk_after_5_minutes=False"};
            File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Settings.txt",new_settings);
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
            states.Add(checkBox2.Checked);//new day
            states.Add(checkBox3.Checked);//count continue
            states.Add(checkBox4.Checked);//AFK mode
            return states;
        }

        private void closeWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            string[] strings = File.ReadAllLines("Settings.txt");
            string[] line = strings[1].Split('=');
            strings[1] = $"{line[0]}={checkBox3.Checked}";
            File.WriteAllLines("Settings.txt",strings);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            string[] strings = File.ReadAllLines("Settings.txt");
            string[] line = strings[2].Split('=');
            strings[2] = $"{line[0]}={checkBox4.Checked}";
            File.WriteAllLines("Settings.txt",strings);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup";                                                                                                                     
            //string shortcutPath = Path.Combine(desktopPath, "TimerForApps.lnk");
            //string targetFile = AppDomain.CurrentDomain.BaseDirectory + "\\TimerForApps.exe";
            IShellLink link = (IShellLink)new ShellLink();

            // setup shortcut information
            link.SetDescription("TimerForApps");
            link.SetPath(AppDomain.CurrentDomain.BaseDirectory + "\\TimerForApps.exe");
            link.SetWorkingDirectory(AppDomain.CurrentDomain.BaseDirectory);

            // save it
            IPersistFile file = (IPersistFile)link;
            file.Save(Path.Combine(desktopPath, "TimerForApps.lnk"), false);
        }
    }
    [ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    internal class ShellLink
    {
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    internal interface IShellLink
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
        void GetIDList(out IntPtr ppidl);
        void SetIDList(IntPtr pidl);
        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
        void GetHotkey(out short pwHotkey);
        void SetHotkey(short wHotkey);
        void GetShowCmd(out int piShowCmd);
        void SetShowCmd(int iShowCmd);
        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
        void Resolve(IntPtr hwnd, int fFlags);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }
}
