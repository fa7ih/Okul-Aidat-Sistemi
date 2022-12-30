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
using DevExpress.XtraEditors;

namespace OkulAidatSistemi
{
    public partial class FrmOgretmen : Form
    {
        public FrmOgretmen()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        public void verileriGoster(string veri)
        {
            SqlDataAdapter da = new SqlDataAdapter(veri, bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle1()
        {
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtMail.Text = "";
            Txtid.Text = "";
            MskDogumTarihi.Text = "";
            txtbrans.Text = "";
            txtmaas.Text = "";
            MskTC.Text = "";
            MskTelefon1.Text = "";
            MskBaslamaTarihi.Text = "";
            RchDetay.Text = "";
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

        void egıtımYili()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,EGITIMYILI from TBL_EGITIMYILI", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit2.Properties.ValueMember = "ID";
            lookUpEdit2.Properties.DisplayMember = "EGITIMYILI";
            lookUpEdit2.Properties.DataSource = dt;
        }

        void egıtımYili1()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,EGITIMYILI from TBL_EGITImYILI", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit3.Properties.ValueMember = "ID";
            lookUpEdit3.Properties.DisplayMember = "EGITIMYILI";
            lookUpEdit3.Properties.DataSource = dt;
        }

        void temizle2()
        {
            TxtAdBul.Text = "";
            TxtSoyadBul.Text = "";
            txtbransbul.Text = "";
            MskTcBul.Text = "";
        }

        private void FrmOgretmen_Load(object sender, EventArgs e)
        {
            verileriGoster("Execute OgretmenBilgileri");
            sehirlistesi();
            temizle1();
            temizle2();
            egıtımYili();
            egıtımYili1();
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

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle1();
        }

        private void BtnTemizle2_Click(object sender, EventArgs e)
        {
            temizle1();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            string sqlString = "select * from TBL_OGRETMEN WHERE";
            int control = 0;

            if (TxtAdBul.Text != "")
            {
                control++;
                sqlString += " AD LIKE '" + TxtAdBul.Text + "%' AND";
            }
            if (TxtSoyadBul.Text != "")
            {
                control++;
                sqlString += " soyad LIKE '" + TxtSoyadBul.Text + "%' AND";
            }
            if (MskTcBul.Text != "")
            {
                control++;
                sqlString += " TC LIKE '" + MskTcBul.Text + "%' AND";
            }
            if (txtbransbul.Text != "")
            {
                control++;
                sqlString += " BRANS LIKE '" + txtbransbul.Text + "%' AND";
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

        private void BtnListeyiGoruntule_Click(object sender, EventArgs e)
        {
            verileriGoster("exec ogretmenbilgileri");
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_OGRETMEN (AD,SOYAD,TC,BRANS,MAAS,BASLANGICTARIHI,DOGUMTARIHI,DETAY,TELEFON,MAIL,IL,ILCE,EGITIMYILIID) " +
                "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTC.Text);
            komut.Parameters.AddWithValue("@p4", txtbrans.Text);
            komut.Parameters.AddWithValue("@p5", txtmaas.Text);
            komut.Parameters.AddWithValue("@p6", MskBaslamaTarihi.Text);
            komut.Parameters.AddWithValue("@p7", MskDogumTarihi.Text);
            komut.Parameters.AddWithValue("@p8", RchDetay.Text);
            komut.Parameters.AddWithValue("@p9", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p10", TxtMail.Text);
            komut.Parameters.AddWithValue("@p11", Cmbil.Text);
            komut.Parameters.AddWithValue("@p12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", lookUpEdit2.EditValue);
            komut.ExecuteNonQuery();
            verileriGoster("Execute OgretmenBilgileri");
            bgl.baglanti().Close();
            MessageBox.Show("Öğretmen Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle1();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_OGRETMEN where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            verileriGoster("select * from TBL_OGREtmen");
            MessageBox.Show("Öğretmen listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            temizle1();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_OGRETMEN set AD=@p1,SOYAD=@p2,TC=@p3,BRANS=@p4,MAAS=@p5,BASLANGICTARIHI=@p6,DOGUMTARIHI=@p7" +
                         ",DETAY=@p8,TELEFON=@p9,MAIL=@p10,IL=@p11,ILCE=@p12,EGITIMYILIID=@p13 WHERE ID=@P14 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTC.Text);
            komut.Parameters.AddWithValue("@p4", txtbrans.Text);
            komut.Parameters.AddWithValue("@p5", txtmaas.Text);
            komut.Parameters.AddWithValue("@p6", MskBaslamaTarihi.Text);
            komut.Parameters.AddWithValue("@p7", MskDogumTarihi.Text);
            komut.Parameters.AddWithValue("@p8", RchDetay.Text);
            komut.Parameters.AddWithValue("@p9", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p10", TxtMail.Text);
            komut.Parameters.AddWithValue("@p11", Cmbil.Text);
            komut.Parameters.AddWithValue("@p12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", lookUpEdit2.EditValue);
            komut.Parameters.AddWithValue("@p14", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğretmen Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            verileriGoster("Execute OgretmenBilgileri");
            temizle1();
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
                MskBaslamaTarihi.Text = dr["BASLANGICTARIHI"].ToString();
                txtmaas.Text = dr["MAAS"].ToString();
                txtbrans.Text = dr["BRANS"].ToString();
                MskDogumTarihi.Text = dr["DOGUMTARIHI"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                RchDetay.Text = dr["DETAY"].ToString();
            }
        }
    }
}
