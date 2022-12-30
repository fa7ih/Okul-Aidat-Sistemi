using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace OkulAidatSistemi
{
    public partial class FrmSehirBilgileri : Form
    {
        public FrmSehirBilgileri()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void ogrenciSehir()
        {
            SqlCommand komut2 = new SqlCommand("Select IL,Count(*) From TBL_OGRENCILER Group By IL", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl4.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        void ogretmenSehir()
        {
            SqlCommand komut2 = new SqlCommand("Select IL,Count(*) From TBL_OGRETMEN Group By IL", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl5.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        void kirtasiyeSehir()
        {
            SqlCommand komut2 = new SqlCommand("Select IL,Count(*) From TBL_KIRTASIYE Group By IL", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        void bankaSehir()
        {
            SqlCommand komut2 = new SqlCommand("Select IL,Count(*) From TBL_BANKALAR Group By IL", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        void personelSehir()
        {
            SqlCommand komut2 = new SqlCommand("Select IL,Count(*) From TBL_PERSONELLER Group By IL", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl3.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        private void FrmSehirBilgileri_Load(object sender, EventArgs e)
        {
            ogrenciSehir();
            ogretmenSehir();
            kirtasiyeSehir();
            bankaSehir();
            personelSehir();
        }
    }
}
