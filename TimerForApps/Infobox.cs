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

        private void Infobox_FormClosed(object sender, FormClosedEventArgs e)
        {
            winproc.infoclose();
        }
    }
}
