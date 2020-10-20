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
        public static List<ProcessWindow> Procfind(bool file)
        {
            //Process[] lis =  Process.GetProcesses();
            ProcessWindow[] applications = ProcessHelper.GetRunningApplications();
            List<ProcessWindow> whiteapp = new List<ProcessWindow>();

            for (int i = 0; i < applications.Length; i++)
            {
                bool black = false;
                //bool white = true;
                int listc = 0;
                if (file == true)
                {
                    StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Lists.txt");
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (listc == 0)
                        {
                            if (line == "====BlackList====")
                            {
                                //white = false;
                                listc = 1;
                            }
                            else
                            {
                                int ind = applications[i].WindowTitle.IndexOf(line);
                                if (ind > -1)
                                {
                                    applications[i].ProcessName = line.Replace(" ", "");
                                    applications[i].WindowTitle = line;
                                }
                            }
                        }
                        else if (listc == 1)
                        {
                            if (line == "====ControlList====")
                            {
                                //white = false;
                                listc = 2;
                            }
                            if (line == applications[i].WindowTitle)
                            {
                                black = true;
                                break;
                            }
                        }
                        else if (listc == 2)
                        {
                            if (line == applications[i].WindowTitle)
                            {
                                //MessageBox.Show("Are you shure about this?");
                                if (!infopen)
                                {
                                    Infobox info = new Infobox();
                                    infopen = true;
                                    info.write("Are you shure about this?");
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
                    whiteapp.Add(applications[i]);
                }
            }
            return whiteapp;
        }
    }

    public class ProcessWindow
    {
        public string WindowTitle { get; set; }
        public Process Process { get; private set; }

        public string ProcessName { get; set; }
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

}
