﻿using System;
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
    public partial class newemail : Form
    {
        public newemail()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {

                MessageBox.Show("Lütfen Boş değer Girmeyiniz");
            }
            else
            {
                if (textBox1.Text == textBox2.Text)
                {
                    SqlCommand komut = new SqlCommand("update TBL_LOGIN set EMAIL='" + textBox1.Text + "'  ", bgl.baglanti());
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("E-mail başarıyla yenilenmiştir");
                }
                else
                {
                    MessageBox.Show("yeni e-mail eşleşmiyor bu yüzden tekrar deneyin");
                }
            }
        }
    }
}
