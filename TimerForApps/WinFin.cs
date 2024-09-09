using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Media;

namespace TimerForApps
{
    using HWND = IntPtr;
    public static class OpenWindowGetter
    {
        /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
        /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>

        public static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND lShellWindow = GetShellWindow();
            Dictionary<HWND, string> lWindows = new Dictionary<HWND, string>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == lShellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;
                if (IsIconic(hWnd)) return true;

                int lLength = GetWindowTextLength(hWnd);
                if (lLength == 0) return true;

                StringBuilder lBuilder = new StringBuilder(lLength);
                GetWindowText(hWnd, lBuilder, lLength + 1);

                lWindows[hWnd] = lBuilder.ToString();
                return true;

            }, 0);

            return lWindows;
        }

        delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        [DllImport("USER32.DLL")]
        static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.DLL")]
        static extern IntPtr GetShellWindow();

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);
    }
    /// <summary>
    /// Find process class
    /// </summary>
    public static class winproc
    {
        public static bool infopen = false;
        public static void infoclose()
        {
            infopen = false;
        }
        /// <summary>
        /// Make list of working process without black listed processes
        /// </summary>
        /// <param name="file">file exist or not</param>
        /// <returns>list of working process</returns>
        public static List<ProcessWindow> ProcessFind(bool file)
        {
            ProcessWindow[] applications = ProcessHelper.GetRunningApplications();//make list of running applications
            List<ProcessWindow> goodApps = new List<ProcessWindow>();//list of good apps
            //walk throw applications to find good apps
            foreach (var application in applications)
            {
                bool black = false; //process in black list or not
                int whichList = 0; //in which list lies current app
                //if file exist
                if (file)
                {
                    StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt");
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (whichList == 0)
                        {
                            if (line == "====BlackList====")
                            {
                                //white = false;
                                whichList = 1;
                            }
                            else
                            {
                                bool ind = application.WindowTitle.Contains(line);
                                if (ind)
                                {
                                    application.ProcessName = line.Replace(" ", "");
                                    application.WindowTitle = line;
                                }
                            }
                        }
                        else if (whichList == 1)
                        {
                            if (line == "====ControlList====")
                            {
                                //white = false;
                                whichList = 2;
                            }
                            if (line == application.WindowTitle)
                            {
                                black = true;
                                break;
                            }
                        }
                        else if (whichList == 2)
                        {
                            bool ind = application.WindowTitle.Contains(line);
                            if (ind)
                            {
                                //MessageBox.Show("Are you sure about this?");
                                if (!infopen)
                                {
                                    application.Process.Kill();
                                    Infobox info = new Infobox();
                                    infopen = true;
                                    info.write("Sorry, I closed it XD");
                                    info.Show();
                                }
                                break;
                            }
                        }
                    }

                    sr.Close();
                }
                if (black == false || file == false)
                {
                    goodApps.Add(application);
                }
            }
            return goodApps;
        }
    }

    public class ProcessWindow
    {
        public string WindowTitle;
        
        public Process Process { get; private set; }

        public string ProcessName;
        public ProcessWindow(string windowTitle, Process process)
        {
            WindowTitle = windowTitle;
            ProcessName = process.ProcessName;
            Process = process;
        }
    }

    public static class ProcessHelper
    {
        /// <summary>
        /// Возвращает массив приложений запущенных пользователем
        /// Из результата исклюлючаются текущий процесс и explorer
        /// </summary>
        public static ProcessWindow[] GetRunningApplications()
        {
            var allProccesses = Process.GetProcesses();
            var myPid = Process.GetCurrentProcess().Id;
            var explorerPids = allProccesses.Where(p => "explorer".Equals(p.ProcessName, StringComparison.OrdinalIgnoreCase)).Select(p => p.Id).ToArray();
            var windows = new List<ProcessWindow>();
            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                var sbTitle = new StringBuilder(255);
                GetWindowText(hWnd, sbTitle, sbTitle.Capacity + 1);
                string windowTitle = sbTitle.ToString();

                if (!string.IsNullOrEmpty(windowTitle) && IsWindowVisible(hWnd))
                {
                    int pid;
                    GetWindowThreadProcessId(hWnd, out pid);
                    if (pid != myPid && !explorerPids.Contains(pid) && !IsIconic(hWnd))
                    {
                        windows.Add(new ProcessWindow(windowTitle, allProccesses.FirstOrDefault(p => p.Id == pid)));
                    }
                }

                return true;
            };

            EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);
            return windows.ToArray();
        }

        delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);
        
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);
    }

    public static class PersonWork
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);


        public static TimeSpan? GetInactiveTime()
        {
            LASTINPUTINFO info = new LASTINPUTINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            if (GetLastInputInfo(ref info))
                return TimeSpan.FromMilliseconds(Environment.TickCount - info.dwTime);
            else
                return null;
        }
    }
}
