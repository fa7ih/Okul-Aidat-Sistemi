using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OkulAidatSistemi
{
    public class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            ///SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-BIJ875P;initial catalog=DboOkulAidatSistemi;Integrated Security=True");
            SqlConnection baglan = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DboOkulAidatSistemi.mdf;initial catalog=DboOkulAidatSistemi;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
