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
    public partial class FrmOgrenciler : Form
    {
        public FrmOgrenciler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void verileriGoster(String veriler)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(veriler, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public void ogretmenListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from TBL_OGRETMEN", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        void egıtımYili()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_EGITIMYILI", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit2.Properties.ValueMember = "ID";
            lookUpEdit2.Properties.DisplayMember = "EGITIMYILI";
            lookUpEdit2.Properties.DataSource = dt;
        }

        void egıtımYili1()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_EGITIMYILI", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit3.Properties.ValueMember = "ID";
            lookUpEdit3.Properties.DisplayMember = "EGITIMYILI";
            lookUpEdit3.Properties.DataSource = dt;
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void temizle1()
        {
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtMail.Text = "";
            Txtid.Text = "";
            MskDogumTarihi.Text = "";
            MskKayıtTarihi.Text = "";
            MskNumara.Text = "";
            MskTC.Text = "";
            MskTelefon1.Text = "";
            CmbSinif.Text = "";
            CmbSube.Text = "";
            RchAdres.Text = "";
        }

        void temizle2()
        {
            TxtAdBul.Text = "";
            TxtSoyadBul.Text = "";
            MskNoBul.Text = "";
            MskTcBul.Text = "";
        }
        

        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            verileriGoster("Execute OgrenciBilgileri");
            sehirlistesi();
            ogretmenListesi();
            temizle1();
            temizle2();
            egıtımYili();
            egıtımYili1();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            string sqlString = "select * from TBL_OGRENCILER WHERE";
            int control = 0;

            if (TxtAdBul.Text != "")
            {
                control++;
                sqlString += " AD LIKE '" + TxtAdBul.Text + "%' AND";
            }
            if (TxtSoyadBul.Text != "")
            {
                control++;
                sqlString += " SOYAD LIKE '" + TxtSoyadBul.Text + "%' AND";
            }
            if (MskTcBul.Text != "")
            {
                control++;
                sqlString += " TC LIKE '" + MskTcBul.Text + "%' AND";
            }
            if (MskNoBul.Text != "")
            {
                control++;
                sqlString += " OKULNO LIKE '" + MskNoBul.Text + "%' AND";
            }
            if (lookUpEdit3.Text != "")
            {
                control++;
                sqlString += " EGITIMYILIID LIKE '" + lookUpEdit3.EditValue + "%' AND";
            }
            if (control == 0)
            {
                MessageBox.Show("Lütfen en az bir değer giriniz.");
            }
            else
            {
                sqlString = sqlString.Remove(sqlString.Length - 3, 3);
                verileriGoster(sqlString);
            }
            bgl.baglanti().Close();
            temizle2();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();

            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnTemizle3_Click(object sender, EventArgs e)
        {
            temizle2();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_OGRENCILER where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            verileriGoster("Execute OgrenciBilgileri");
            MessageBox.Show("Öğrenci listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            temizle1();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle1();
        }

        private void BtnTemizle2_Click(object sender, EventArgs e)
        {
            temizle1();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_OGRENCILER (AD,SOYAD,TC,OKULNO,SINIF,SUBE,DOGUMTARIHI,KAYITTARIHI,TELEFON,MAIL,IL,ILCE,ADRES,OGRETMENID,EGITIMYILIID,CINSIYET) " +
                "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTC.Text);
            komut.Parameters.AddWithValue("@p4", MskNumara.Text);
            komut.Parameters.AddWithValue("@p5", CmbSinif.Text);
            komut.Parameters.AddWithValue("@p6", CmbSube.Text);
            komut.Parameters.AddWithValue("@p7", MskDogumTarihi.Text);
            komut.Parameters.AddWithValue("@p8", MskKayıtTarihi.Text);
            komut.Parameters.AddWithValue("@p9", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p10", TxtMail.Text);
            komut.Parameters.AddWithValue("@p11", Cmbil.Text);
            komut.Parameters.AddWithValue("@p12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", RchAdres.Text);
            komut.Parameters.AddWithValue("@p14", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p15", lookUpEdit2.EditValue);
            komut.Parameters.AddWithValue("@p16", cmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            verileriGoster("Execute OgrenciBilgileri");
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle1();
        }

        private void BtnListeyiGoruntule_Click(object sender, EventArgs e)
        {
            verileriGoster("Execute OgrenciBilgileri");
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTC.Text = dr["TC"].ToString();
                MskNumara.Text = dr["OKULNO"].ToString();
                CmbSinif.Text = dr["SINIF"].ToString();
                CmbSube.Text = dr["SUBE"].ToString();
                MskDogumTarihi.Text = dr["DOGUMTARIHI"].ToString();
                MskKayıtTarihi.Text = dr["KAYITTARIHI"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                cmbCinsiyet.Text = dr["CINSIYET"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_OGRENCILER set AD=@p1,SOYAD=@p2,TC=@p3,OKULNO=@p4,SINIF=@p5,SUBE=@p6,DOGUMTARIHI=@p7" +
                ",KAYITTARIHI=@p8,TELEFON=@p9,MAIL=@p10,IL=@p11,ILCE=@p12,ADRES=@p13,OGRETMENID=@p14,EGITIMYILIID=@p15,CINSIYET=@p17 WHERE ID=@P16 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTC.Text);
            komut.Parameters.AddWithValue("@p4", MskNumara.Text);
            komut.Parameters.AddWithValue("@p5", CmbSinif.Text);
            komut.Parameters.AddWithValue("@p6", CmbSube.Text);
            komut.Parameters.AddWithValue("@p7", MskDogumTarihi.Text);
            komut.Parameters.AddWithValue("@p8", MskKayıtTarihi.Text);
            komut.Parameters.AddWithValue("@p9", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p10", TxtMail.Text);
            komut.Parameters.AddWithValue("@p11", Cmbil.Text);
            komut.Parameters.AddWithValue("@p12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", RchAdres.Text);
            komut.Parameters.AddWithValue("@p14", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p15", lookUpEdit2.EditValue);
            komut.Parameters.AddWithValue("@p16", Txtid.Text);
            komut.Parameters.AddWithValue("@p17", cmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            verileriGoster("Execute OgrenciBilgileri");
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            temizle1();
        }
    }
}
