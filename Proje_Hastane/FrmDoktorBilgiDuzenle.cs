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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();


        public string TCNO;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            msbTC.Text = TCNO;
            //comboboxa branslar
            SqlCommand komut = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) { cmbBrans.Items.Add(dr[0]); }
            bgl.baglanti().Close();

            //boxlara bilgi çekem
            SqlCommand komut1 = new SqlCommand("Select * From Tbl_Doktorlar Where DoktorTC=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",TCNO);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                txtAd.Text = dr1[1].ToString();
                txtSoyad.Text = dr1[2].ToString();
                cmbBrans.Text = dr1[3].ToString();
                txtSifre.Text = dr1[5].ToString();
            }
            bgl.baglanti().Close();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Doktorlar Set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 Where DoktorTC=@p5",bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1",txtAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@p3",cmbBrans.Text);
            komutguncelle.Parameters.AddWithValue("@p4",txtSifre.Text);
            komutguncelle.Parameters.AddWithValue("@p5",msbTC.Text);

            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
