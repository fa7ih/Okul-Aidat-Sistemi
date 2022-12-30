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

namespace OkulAidatSistemi
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();


        private void FrmAyarlar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select*from TBL_LOGIN where KULLANICIADI=@username and SIFRE=@password ", bgl.baglanti());
            komut.Parameters.AddWithValue("@username", textBox1.Text);
            komut.Parameters.AddWithValue("@password", textBox2.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                panel2.Controls.Clear();
                Ayarlar ka = new Ayarlar();
                ka.TopLevel = false;
                panel2.Controls.Add(ka);
                ka.Show();
                ka.Dock = DockStyle.None;
                ka.BringToFront();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş Yaptınız");
            }
            bgl.baglanti().Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = true;
                var chekbox = (CheckBox)sender;
                chekbox.Text = "Şifreyi Göster";
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
                var chekbox = (CheckBox)sender;
                chekbox.Text = "Şifreyi Gizle";
            }
        }
    }
}
