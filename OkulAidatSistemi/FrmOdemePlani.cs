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
    public partial class FrmOdemePlani : Form
    {
        public FrmOdemePlani()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void verileriGoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler,bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource= dt;
        }

        void temizle1()
        {
            lookUpEdit3.Text = "";
            txtbahar.Text = "";
            txtguz.Text = "";
            txttoplam.Text = "";
            Cmbtaksitsayisi.Text = "";
            MskBaslangicTarihi.Text = "";
            rchdetay.Text = "";
        }

        void temizle2()
        {
            lookUpEdit4.Text = "";
            MskNoBul.Text = "";
            MskTcBul.Text = "";
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

        void odemeSekli()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,ODEMESEKLI from TBL_ODEMESEKLI", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "ODEMESEKLI";
            lookUpEdit1.Properties.DataSource = dt;
        }
        
        void ogrenci()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from TBL_OGRENCILER", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit4.Properties.ValueMember = "ID";
            lookUpEdit4.Properties.DisplayMember = "AD";
            lookUpEdit4.Properties.DataSource = dt;
        }

        private void FrmOdemePlani_Load(object sender, EventArgs e)
        {
            verileriGoster("exec OdemePlaniBilgileri");
            ogrenci();
            egıtımYili1();
            temizle1();
            temizle2();
            odemeSekli();
        }

        private void BtnTemizle3_Click(object sender, EventArgs e)
        {
            temizle2();
        }

        private void BtnListeyiGoruntule_Click(object sender, EventArgs e)
        {
            verileriGoster("exec OdemePlaniBilgileri");
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            string sqlString = "select * from TBL_OGRENCILER WHERE";
            int control = 0;
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
                SqlCommand komut = new SqlCommand(sqlString, bgl.baglanti());
                SqlDataReader dt = komut.ExecuteReader();
                while (dt.Read())
                {
                    lblid.Text = dt["ID"].ToString();
                    lblad.Text = dt["AD"].ToString();
                    lblsoyad.Text = dt["SOYAD"].ToString();
                }
            }
            bgl.baglanti().Close();
            temizle2();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle1();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtbahar.Text = dr["BAHARTUTARI"].ToString();
                txtid.Text = dr["ID"].ToString();
                txtguz.Text = dr["GÜZTUTARI"].ToString();
                txttoplam.Text = dr["TOPLAMTUTAR"].ToString();
                MskBaslangicTarihi.Text = dr["BASLANGICTARIHI"].ToString();
                Cmbtaksitsayisi.Text = dr["TAKSITSAYISI"].ToString();
                rchdetay.Text = dr["DETAY"].ToString();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_ODEMEPLANI (OGRENCIID,TOPLAMTUTAR,BAHARTUTARI,GÜZTUTARI,BASLANGICTARIHI,TAKSITSAYISI,DETAY,ODEMESEKLI) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookUpEdit4.EditValue);
            komut.Parameters.AddWithValue("@p2", txttoplam.Text);
            komut.Parameters.AddWithValue("@p3", txtbahar.Text);
            komut.Parameters.AddWithValue("@p4", txtguz.Text);
            komut.Parameters.AddWithValue("@p5", MskBaslangicTarihi.Text);
            komut.Parameters.AddWithValue("@p6", Cmbtaksitsayisi.Text);
            komut.Parameters.AddWithValue("@p7", rchdetay.Text);
            komut.Parameters.AddWithValue("@p8", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            verileriGoster("exec OdemePlaniBilgileri");
            bgl.baglanti().Close();
            MessageBox.Show("Ödeme Planı Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_ODEMEPLANI set OGRENCIID=@p1,TOPLAMTUTAR=@p2,BAHARTUTARI=@p3,GÜZTUTARI=@p4,BASLANGICTARIHI=@p5,TAKSITSAYISI=@p6,DETAY=@p7,ODEMESEKLI=@p8 where ID=@p9 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookUpEdit4.EditValue);
            komut.Parameters.AddWithValue("@p2", txttoplam.Text);
            komut.Parameters.AddWithValue("@p3", txtbahar.Text);
            komut.Parameters.AddWithValue("@p4", txtguz.Text);
            komut.Parameters.AddWithValue("@p5", MskBaslangicTarihi.Text);
            komut.Parameters.AddWithValue("@p6", Cmbtaksitsayisi.Text);
            komut.Parameters.AddWithValue("@p7", rchdetay.Text);
            komut.Parameters.AddWithValue("@p8", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p9",txtid.Text);
            komut.ExecuteNonQuery();
            verileriGoster("exec OdemePlaniBilgileri");
            bgl.baglanti().Close();
            MessageBox.Show("Ödeme Planı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
