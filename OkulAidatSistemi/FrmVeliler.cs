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
    public partial class FrmVeliler : Form
    {
        public FrmVeliler()
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

        void ogrenciListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from TBL_OGRENCILER", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        void temizle()
        {
            txtannead.Text = "";
            Txtid.Text = "";
            txtbabaad.Text = "";
            MskAnaTelefon.Text = "";
            MskBabaTelefon.Text = "";
            RchDetay.Text = "";
        }

        private void FrmVeliler_Load(object sender, EventArgs e)
        {
            verileriGoster("execute VeliBilgileri");
            ogrenciListesi();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_VELI (OGRENCIID,ANNEAD,BABAAD,ANNETELEFON,BABATELEFON,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p2",txtannead.Text);
            komut.Parameters.AddWithValue("@p3",txtbabaad.Text);
            komut.Parameters.AddWithValue("@p4",MskAnaTelefon.Text);
            komut.Parameters.AddWithValue("@p5",MskBabaTelefon.Text);
            komut.Parameters.AddWithValue("@p6",RchDetay.Text);
            komut.ExecuteNonQuery();
            verileriGoster("exec VeliBilgileri");
            bgl.baglanti().Close();
            MessageBox.Show("Veli Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                txtannead.Text = dr["ANNE ADI"].ToString();
                txtbabaad.Text = dr["BABA ADI"].ToString();
                MskAnaTelefon.Text = dr["ANNE TELEFONU"].ToString();
                MskBabaTelefon.Text = dr["BABA TELEFONU"].ToString();
                RchDetay.Text = dr["DETAY"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update  TBL_VELI set OGRENCIID=@p1,ANNEAD=@p2,BABAAD=@p3,ANNETELEFON=@p4,BABATELEFON=@p5,DETAY=@p6 where ID=@p7 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p2", txtannead.Text);
            komut.Parameters.AddWithValue("@p3", txtbabaad.Text);
            komut.Parameters.AddWithValue("@p4", MskAnaTelefon.Text);
            komut.Parameters.AddWithValue("@p5", MskBabaTelefon.Text);
            komut.Parameters.AddWithValue("@p6", RchDetay.Text);
            komut.Parameters.AddWithValue("@p7", Txtid.Text);
            komut.ExecuteNonQuery();
            verileriGoster("exec VeliBilgileri");
            bgl.baglanti().Close();
            MessageBox.Show("Veli Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }
    }
}
