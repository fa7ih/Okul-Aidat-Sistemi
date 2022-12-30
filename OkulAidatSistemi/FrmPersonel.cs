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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        void personelliste(string veri)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(veri, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
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

        void temizle()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtGorev.Text = "";
            TxtSoyad.Text = "";
            TxtMail.Text = "";
            MskTC.Text = "";
            MskTelefon1.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            RchAdres.Text = "";
        }

        void temizle2()
        {
            TxtAdBul.Text = "";
            TxtSoyadBul.Text = "";
            txtgörevbul.Text = "";
            MskTcBul.Text = "";
        }

        void egıtımYili()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_EGITImYILI", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit2.Properties.ValueMember = "ID";
            lookUpEdit2.Properties.DisplayMember = "EGITIMYILI";
            lookUpEdit2.Properties.DataSource = dt;
        }

        void egıtımYili1()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_EGITImYILI", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit4.Properties.ValueMember = "ID";
            lookUpEdit4.Properties.DisplayMember = "EGITIMYILI";
            lookUpEdit4.Properties.DataSource = dt;
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();

            SqlCommand komut = new SqlCommand("Select IlCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            personelliste("exec personelbilgileri");
            sehirlistesi();
            egıtımYili();
            egıtımYili1();
            temizle();
            temizle2();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnTemizle3_Click(object sender, EventArgs e)
        {
            temizle2();
        }

        private void BtnListeyiGoruntule_Click(object sender, EventArgs e)
        {
            personelliste("exec personelbilgileri");
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            string sqlString = "select * from tbl_personeller  WHERE";
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
            if (txtgörevbul.Text != "")
            {
                control++;
                sqlString += " GOREV LIKE '" + txtgörevbul.Text + "%' AND";
            }
            if (lookUpEdit4.Text != "")
            {
                control++;
                sqlString += " EGITIMYILIID LIKE '" + lookUpEdit4.EditValue + "%' AND";
            }
            if (control == 0)
            {
                MessageBox.Show("Lütfen en az bir değer giriniz.");
            }
            else
            {
                sqlString = sqlString.Remove(sqlString.Length - 3, 3);
                personelliste(sqlString);
            }
            bgl.baglanti().Close();
            temizle2();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_personeller (ad,soyad,telefon,tc,maıl,ıl,ılce,adres,gorev,maas,egıtımyılııd) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTC.Text);
            komut.Parameters.AddWithValue("@p5", TxtMail.Text);
            komut.Parameters.AddWithValue("@p6", Cmbil.Text);
            komut.Parameters.AddWithValue("@p7", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p8", RchAdres.Text);
            komut.Parameters.AddWithValue("@p9", txtmaas.Text);
            komut.Parameters.AddWithValue("@p10", TxtGorev.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit2.EditValue);
            komut.ExecuteNonQuery();
            personelliste("exec personelbilgileri");
            bgl.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tbl_personeller set AD=@P1,SOYAD=@P2,TELEFON=@P3,TC=@P4,MAIL=@P5,IL=@P6,ILCE=@P7,ADRES=@P8,GOREV=@P9,maas=@p10,egıtımyılııd=@p12 WHERE ID=@P11", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtMail.Text);
            komut.Parameters.AddWithValue("@P6", Cmbil.Text);
            komut.Parameters.AddWithValue("@P7", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P8", RchAdres.Text);
            komut.Parameters.AddWithValue("@P9", TxtGorev.Text);
            komut.Parameters.AddWithValue("@P10", txtmaas.Text);
            komut.Parameters.AddWithValue("@P12",lookUpEdit2.EditValue);
            komut.Parameters.AddWithValue("@P11", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            personelliste("exec personelbilgileri");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("delete from TBL_PERSONELLER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", Txtid.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.None);
            personelliste("exec personelbilgileri");
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtGorev.Text = dr["GOREV"].ToString();
                txtmaas.Text = dr["maas"].ToString();
            }
        }
    }
}
