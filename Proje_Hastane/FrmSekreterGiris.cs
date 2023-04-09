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

namespace Proje_Hastane
{
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Select * From Tbl_Sekreter Where SekreterTC=@p1 And SekreterSifre=@p2",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",mskTC.Text);
            komut1.Parameters.AddWithValue("@p2",txtSifre.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            if (dr1.Read())
            {
                FrmSekreterDetay frs = new FrmSekreterDetay();
                frs.tc = mskTC.Text;
                frs.Show();
                this.Hide();
            }
            else { MessageBox.Show("Hatalı TC & Şifre"); }
            bgl.baglanti().Close();

        }
    }
}
