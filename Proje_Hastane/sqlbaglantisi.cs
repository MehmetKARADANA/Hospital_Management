using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    internal class sqlbaglantisi
    {
       public SqlConnection baglanti()//sürekli sql connection kurmak yerine fonksiyonunu yazıyoruz
        {
            SqlConnection baglan = new SqlConnection("Data Source=MEHMET;Initial Catalog=HastaneProje;Integrated Security=True;");
            baglan.Open();//çağırdığımızda bağlantı açık gelsin 
            return baglan;
        }
    }
}
