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
    public partial class OdemeSekliGirisi : Form
    {
        public OdemeSekliGirisi()
        {
            InitializeComponent();
        }
        string islemTuru;
        SqlBaglantisi bgl = new SqlBaglantisi();

        public void verilerigöster()
        {
            SqlDataAdapter da = new SqlDataAdapter("select*from TBL_ODEMESEKLI", bgl.baglanti());
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void OdemeSekliGirisi_Load(object sender, EventArgs e)
        {
            verilerigöster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_ODEMESEKLI (ODEMESEKLI) values (@öş)", bgl.baglanti());
            komut.Parameters.AddWithValue("@öş", textBox2.Text);
            komut.ExecuteNonQuery();
            verilerigöster();
            bgl.baglanti().Close();
            textBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_ODEMESEKLI where ODEMESEKLI=@adi", bgl.baglanti());
            komut.Parameters.AddWithValue("@adi", islemTuru);
            komut.ExecuteNonQuery();
            verilerigöster();
            bgl.baglanti().Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            islemTuru = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
        }
    }
}
