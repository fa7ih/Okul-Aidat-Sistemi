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
using DevExpress.XtraBars;

namespace OkulAidatSistemi
{
    public partial class FrmKirtasiye : Form
    {
        public FrmKirtasiye()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

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

        void temizle()
        {
            TxtAd.Text = "";
            Txtid.Text = "";
            TxtMail.Text = "";
            TxtYetkili.Text = "";
            MskFax.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            MskYetkiliTC.Text = "";
            RchAdres.Text = "";
            TxtAd.Focus();
        }

        void verileriGoster()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("exec KirtasiyeBilgileri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource= dt;
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

        private void FrmKirtasiye_Load(object sender, EventArgs e)
        {
            verileriGoster();
            egıtımYili();
            sehirlistesi();
            temizle();
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["KIRTASIYEADI"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskYetkiliTC.Text = dr["YETKILITC"].ToString();
                MskTelefon1.Text = dr["TELEFON1"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTelefon3.Text = dr["TELEFON3"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_KIRTASIYE (KIRTASIYEADI,YETKILIADSOYAD,YETKILITC,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,ADRES,EGITIMYILIID) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@p12)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p3", MskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p5", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p6", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@p7", TxtMail.Text);
            komut.Parameters.AddWithValue("@p8", MskFax.Text);
            komut.Parameters.AddWithValue("@p9", Cmbil.Text);
            komut.Parameters.AddWithValue("@p10", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p11", RchAdres.Text);
            komut.Parameters.AddWithValue("@p12", lookUpEdit2.EditValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            verileriGoster();
            MessageBox.Show("Kırtasiye Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_KIRTASIYE where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            verileriGoster();
            MessageBox.Show("Kırtasiye listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_KIRTASIYE set KIRTASIYEADI=@P1,YETKILIADSOYAD=@P2,YETKILITC=@P3,TELEFON1=@P4,TELEFON2=@P5,TELEFON3=@P6,MAIL=@P7,FAX=@P8,IL=@P9,ILCE=@P10,ADRES=@P11,EGITIMYILIID=@P12 WHERE ID=@P13", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p3", MskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p5", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p6", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@p7", TxtMail.Text);
            komut.Parameters.AddWithValue("@p8", MskFax.Text);
            komut.Parameters.AddWithValue("@p9", Cmbil.Text);
            komut.Parameters.AddWithValue("@p10", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p11", RchAdres.Text);
            komut.Parameters.AddWithValue("@p12", lookUpEdit2.EditValue);
            komut.Parameters.AddWithValue("@p13", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            verileriGoster();
            MessageBox.Show("Kırtasiye Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            temizle();
        }
    }
}
