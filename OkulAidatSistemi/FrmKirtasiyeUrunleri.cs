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
    public partial class FrmKirtasiyeUrunleri : Form
    {
        public FrmKirtasiyeUrunleri()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void verileriGoster()
        {
            SqlDataAdapter da = new SqlDataAdapter("execute kirtasiyeurunlerbilgileri", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource= dt;
        }

        void temizle()
        {
            Txturunad.Text = "";
            TxtAlis.Text = "";
            Txtid.Text = "";
            MskYil.Text = "";
            NudAdet.Value = 0;
            RchDetay.Text = "";
        }


        void kirtasiyeAdi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,KIRTASIYEADI from TBL_KIRTASIYE", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "KIRTASIYEADI";
            lookUpEdit1.Properties.DataSource = dt;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_KIRTASIYEURUNLERI (KIRTASIYEID,URUNAD,URUNADET,URUNFIYAT,ALISTARIHI,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6) ", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", lookUpEdit1.EditValue);
            cmd.Parameters.AddWithValue("@p2",Txturunad.Text);
            cmd.Parameters.AddWithValue("@p3", int.Parse((NudAdet.Value).ToString()));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtAlis.Text));
            cmd.Parameters.AddWithValue("@p5",MskYil.Text);
            cmd.Parameters.AddWithValue("@p6",RchDetay.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün sisteme eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            verileriGoster();
            temizle();
        }

        private void FrmKirtasiyeUrunleri_Load(object sender, EventArgs e)
        {
            verileriGoster();
            kirtasiyeAdi();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TBL_KIRTASIYEURUNLERI where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", Txtid.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            verileriGoster();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_KIRTASIYEURUNLERI set URUNAD=@P1,ALISTARIHI=@P4,URUNADET=@P5,URUNFIYAT=@P6,DETAY=@P8,KIRTASIYEID=@P7 where ID=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txturunad.Text);
            komut.Parameters.AddWithValue("@p4", MskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlis.Text));
            komut.Parameters.AddWithValue("@p7",lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p8", RchDetay.Text);
            komut.Parameters.AddWithValue("@p9", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            verileriGoster();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            Txtid.Text = dr["ID"].ToString();
            Txturunad.Text = dr["URUNAD"].ToString();
            lookUpEdit1.EditValue = dr["KIRTASİYE ADI"].ToString();
            MskYil.Text = dr["ALISTARIHI"].ToString();
            NudAdet.Value = decimal.Parse(dr["URUNADET"].ToString());
            TxtAlis.Text = dr["URUNFIYAT"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();
        }
    }
}
