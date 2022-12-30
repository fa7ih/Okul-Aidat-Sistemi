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
    public partial class newpassword : Form
    {
        public newpassword()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen Boş Değer Girmeyiniz");
            }
            else
            {
                if (textBox1.Text == textBox2.Text)
                {
                    
                    SqlCommand komut = new SqlCommand("update TBL_LOGIN set SIFRE='" + textBox1.Text + "'  ", bgl.baglanti());
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Şifre başarıyla yenilenmiştir");
                }
                else
                {
                    MessageBox.Show("yeni şifre eşleşmiyor bu yüzden tekrar deneyin");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
                var chekbox = (CheckBox)sender;
                chekbox.Text = "Şifreyi Göster";
            }
            else
            {
                textBox1.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
                var chekbox = (CheckBox)sender;
                chekbox.Text = "Şifreyi Gizle";
            }
        }
    }
}
