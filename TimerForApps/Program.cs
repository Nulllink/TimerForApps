using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TimerForApps
{
    static class Program
    {
        public static Dictionary<string,string> config = new Dictionary<string,string>();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                string[] conf = File.ReadAllLines("Config.txt");
                foreach (var item in conf)
                {
                    string[] vals = item.Split('=');
                    config.Add(vals[0], vals[1]);
                }
            }
            catch
            {
                MessageBox.Show("Please add Config.txt file");
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());//Main form
        }
    }
}
