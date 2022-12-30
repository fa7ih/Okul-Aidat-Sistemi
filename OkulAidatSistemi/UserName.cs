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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OkulAidatSistemi
{
    public partial class UserName : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        public UserName()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void UserName_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select*from TBL_LOGIN where EMAIL=@usermail ", bgl.baglanti());
            komut.Parameters.AddWithValue("@usermail", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                panel1.Visible = false;
                panel2.Visible = true;
            }
            else
            {
                MessageBox.Show("Girdiğiniz kurtarma maili yanlıştır");
            }
            bgl.baglanti().Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox5.Text == "")
            {

                MessageBox.Show("Lütfen Boş değer Girmeyiniz");
            }
            else
            {
                if (textBox4.Text.Equals(textBox5.Text))
                {
                    SqlCommand komut = new SqlCommand("update TBL_LOGIN set KULLANICIADI='" + textBox4.Text + "'  ", bgl.baglanti());
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Kullanıcı Adı başarıyla oluşturulmuştur");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("yeni Kullanıcı Adı eşleşmiyor bu yüzden tekrar deneyin");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
