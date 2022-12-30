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
using DevExpress.XtraGrid;

namespace OkulAidatSistemi
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void chartKır()
        {
            try
            {
                //Charta Stok Miktarı Listeleme
                SqlCommand komut = new SqlCommand("Select URUNAD,Sum(URUNADET) As 'Miktar' from TBL_KIRTASIYEURUNLERI group by URUNAD", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl3.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        
        void Giderler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_GIDERLER order by ID asc", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            Giderler();
            chartKır();
            //Son ayın faturaları
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
            //Son ayın personel maaşları
            SqlCommand komut3 = new SqlCommand("Select MAASLAR From TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaasları.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
            //Toplam öğrenci Sayısı
            SqlCommand komut5 = new SqlCommand("Select Count(*) From TBL_OGRENCILER", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblOgrenciSayilari.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();
            //Toplam kırtasiye Sayısı
            SqlCommand komut6 = new SqlCommand("Select Count(*) From TBL_KIRTASIYE", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblKirtasiyeSayilari.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();
            //Toplam kırtasiye Şehir Sayısı
            SqlCommand komut7 = new SqlCommand("Select Count(Distinct(IL)) From TBL_KIRTASIYE", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblSehirSayısı.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();
            //Toplam Personel Sayısı
            SqlCommand komut8 = new SqlCommand("Select Count(*) From TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblPersonelSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();
            //Toplam Öğretmen Sayısı
            SqlCommand komut9 = new SqlCommand("Select Count(*) From TBL_OGRETMEN", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                LblOgretmenSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();
            //Toplam banka Sayısı
            SqlCommand komut10 = new SqlCommand("Select Count(*) From TBL_BANKALAR", bgl.baglanti());
            SqlDataReader dr10 = komut10.ExecuteReader();
            while (dr10.Read())
            {
                Lblbankasayisi.Text = dr10[0].ToString();
            }
            bgl.baglanti().Close();

        }


        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;

            //Elektrik
            if (sayac > 0 && sayac <= 5)
            {
                groupControl10.Text = "Elektrik";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTRIK from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Su
            if (sayac > 5 && sayac <= 10)
            {
                groupControl10.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,SU from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Doğalgaz
            if (sayac > 10 && sayac <= 15)
            {
                groupControl10.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //İnternet
            if (sayac > 15 && sayac <= 20)
            {
                groupControl10.Text = "İnternet";
                chartControl1.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,INTERNET from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra
            if (sayac > 20 && sayac <= 25)
            {
                groupControl10.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,EKSTRA from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac == 26)
            {
                sayac = 0;
            }
        }

        int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;

            //Elektrik
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl11.Text = "Elektrik";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTRIK from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Su
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl11.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,SU from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Doğalgaz
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl11.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //İnternet
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl11.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,INTERNET from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl11.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 AY,EKSTRA from TBL_GIDERLER order by ID Desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
