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
    public partial class Password : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        public Password()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();    


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select*from TBL_LOGIN where EMAIL=@usermail ", bgl.baglanti());
            komut.Parameters.AddWithValue("@usermail", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                panel1.Visible = false;
                panel3.Visible = true;
            }
            else
            {
                MessageBox.Show("Girdiğiniz kurtarma maili yanlıştır");
            }
            bgl.baglanti().Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
                var chekbox = (CheckBox)sender;
                chekbox.Text = "Şifreyi Göster";
            }
            else
            {
                textBox3.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
                var chekbox = (CheckBox)sender;
                chekbox.Text = "Şifreyi Gizle";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == "" || textBox2.Text == "")
                {

                    MessageBox.Show("Lütfen Boş değer Girmeyiniz");
                }
                else
                {
                    if (textBox2.Text == textBox3.Text)
                    {
                        SqlCommand komut = new SqlCommand("update TBL_LOGIN set SIFRE='" + textBox2.Text + "'  ", bgl.baglanti());
                        komut.ExecuteNonQuery();
                        bgl.baglanti().Close();
                        MessageBox.Show("Şifre başarıyla oluşturulmuştur");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("yeni şifre eşleşmiyor bu yüzden tekrar deneyin");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void Password_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }
    }
}
