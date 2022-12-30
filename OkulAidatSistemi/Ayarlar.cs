using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulAidatSistemi
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            newusername ka = new newusername();
            ka.TopLevel = false;
            panel2.Controls.Add(ka);
            ka.Show();
            ka.Dock = DockStyle.None;
            ka.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            newpassword ka = new newpassword();
            ka.TopLevel = false;
            panel2.Controls.Add(ka);
            ka.Show();
            ka.Dock = DockStyle.None;
            ka.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            newemail ka = new newemail();
            ka.TopLevel = false;
            panel2.Controls.Add(ka);
            ka.Show();
            ka.Dock = DockStyle.None;
            ka.BringToFront();
        }

        private void Ayarlar_Load(object sender, EventArgs e)
        {
            button6.PerformClick();
        }
    }
}
