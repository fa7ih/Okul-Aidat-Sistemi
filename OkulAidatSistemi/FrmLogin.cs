using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace OkulAidatSistemi
{
    public partial class FrmLogin : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        public FrmLogin()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        public static void minimize(Form form)
        {
            if (form.WindowState == FormWindowState.Minimized)
                form.WindowState = FormWindowState.Normal;
            else if (form.WindowState == FormWindowState.Normal)
                form.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select*from TBL_LOGIN where KULLANICIADI=@username and SIFRE=@password ", bgl.baglanti());
            komut.Parameters.AddWithValue("@username", textBox2.Text);
            komut.Parameters.AddWithValue("@password", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form1 anaSayfa = new Form1();
                anaSayfa.Show();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Hatalı Giriş Yaptınız");
            }
            bgl.baglanti().Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.Opacity = .90;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = true;
                var chekbox = (CheckBox)sender;
                chekbox.Text = "Şifreyi Göster";
            }
            else
            {
                textBox1.UseSystemPasswordChar = false;
                var chekbox = (CheckBox)sender;
                chekbox.Text = "Şifreyi Gizle";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            minimize(this);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            UserName k = new UserName();
            k.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Password k = new Password();
            k.ShowDialog();
        }
    }
}
