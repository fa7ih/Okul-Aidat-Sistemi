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
    public partial class FrmEgitimYili : Form
    {
        public FrmEgitimYili()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        string id;


        public void verileriGoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, bgl.baglanti());
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void FrmEgitimYili_Load(object sender, EventArgs e)
        {
            verileriGoster("select * from TBL_EGITIMYILI");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_EGITIMYILI (EGITIMYILI) values (@p1) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", textBox2.Text);
            komut.ExecuteNonQuery();
            verileriGoster("select * from TBL_EGITIMYILI");
            textBox2.Text = "";
            MessageBox.Show("Eğitim Yılı Başarıyla Kayıt Edilmiştir");

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            id = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_EGITIMYILI where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", id);
            komut.ExecuteNonQuery();
            verileriGoster("select * from TBL_EGITIMYILI ");
            MessageBox.Show("Eğitim yılı sistemden silinmiştir");
        }
    }
}
