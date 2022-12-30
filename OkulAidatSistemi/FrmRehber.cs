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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        //öğrenci rehberi
        void ogrenci()
        {
            SqlDataAdapter da = new SqlDataAdapter("select AD,SOYAD,TELEFON,MAIL from TBL_OGRENCILER", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource= dt;    
        }

        //kırtasiye rehberi
        void kirtasiye()
        {
            SqlDataAdapter da = new SqlDataAdapter("select KIRTASIYEADI,YETKILIADSOYAD,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX from TBL_KIRTASIYE",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource= dt;
        }

        //veli rehberi
        void veli()
        {
            SqlDataAdapter da = new SqlDataAdapter("exec VeliRehberBilgileri", bgl.baglanti());  
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl3.DataSource= dt;
        }

        //öğretmen rehberi
        void ogretmen()
        {
            SqlDataAdapter da = new SqlDataAdapter("select AD,SOYAD,TELEFON,MAIL from TBL_OGRETMEN", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl4.DataSource= dt;
        }

        //personel rehberi
        void personel() 
        {
            SqlDataAdapter da = new SqlDataAdapter("select AD,SOYAD,TELEFON,MAIL from TBL_PERSONELLER", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl5.DataSource= dt;
        }

        //banka rehberi
        void banka()
        {
            SqlDataAdapter da = new SqlDataAdapter("select BANKAADI,YETKILI,TELEFON from TBL_BANKALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl6.DataSource= dt;
        }


        private void FrmRehber_Load(object sender, EventArgs e)
        {
            ogrenci();
            ogretmen();
            veli();
            personel();
            banka();
            kirtasiye();
            MessageBox.Show("Mesaj göndermek istiyorsanız kişinin üzerine çift tıklayınız");
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);

            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }

        private void gridControl4_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView4.GetDataRow(gridView4.FocusedRowHandle);

            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }

        private void gridControl5_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView5.GetDataRow(gridView5.FocusedRowHandle);

            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }
    }
}
