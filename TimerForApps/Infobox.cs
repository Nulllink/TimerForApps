using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerForApps
{
    public partial class Infobox : Form
    {
        public Infobox()
        {
            InitializeComponent();
        }

        public void write(string text)
        {
            label1.Text = text;
        }

        public void bar_settings(int max_points)
        {
            progressBar1.Show();
            progressBar1.Maximum = max_points;
        }
        
        public void plus_bar(int points)
        {
            progressBar1.Value += points;
        }

        private void Infobox_FormClosed(object sender, FormClosedEventArgs e)
        {
            winproc.infoclose();
        }
    }
}
