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
using System.Xml;

namespace OkulAidatSistemi
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("http://www.hurriyet.com.tr/rss/anasayfa");
            while (xmloku.Read())
            {
                if (xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }

        void subeMevcut()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT SINIF,SUBE,COUNT(*) AS 'SINIF MEVCUDU' FROM TBL_OGRENCILER GROUP BY SUBE,SINIF", bgl.baglanti());
            da.Fill(dt);
            GridControl1.DataSource= dt;             
        }

        void ogretmenDers()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("exec ogretmenverilen",bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource= dt;
        }

        void sinifCinsiyet()
        {
            DataTable dt = new DataTable(); 
            SqlDataAdapter da = new SqlDataAdapter("SELECT SINIF,SUBE,CINSIYET,COUNT(*) AS 'KİŞİ SAYISI' FROM TBL_OGRENCILER GROUP BY CINSIYET,SINIF,SUBE", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource= dt;
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            ogretmenDers();
            sinifCinsiyet();
            subeMevcut();
            webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");
            haberler();
        }
    }
}
