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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }
         public string TCno;

        sqlbaglantisi bgl = new sqlbaglantisi();  

        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {//hastanınn bilgilerini çekiyoruz
            msbTc.Text = TCno;

            SqlCommand komut = new SqlCommand("Select * From Tbl_Hastalar Where HastaTC='"+TCno+"'",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) {
                //dr[0] biz id yi yazdırmıyoruz ama
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                //dr[3] tc zaten atanmış uğramaya gerek yok onla
                msbTel.Text = dr[4].ToString();
                txtSifre.Text= dr[5].ToString();
                cmbCinsiyet.Text= dr[6].ToString();

            }
            bgl.baglanti().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Update Tbl_Hastalar Set HastaAd=@p1,HastaSoyad=@p2,HastaTC=@p3,HastaTelefon=@p4,HastaSifre=@p5,HastaCinsiyet=@p6 Where HastaTc=@p3",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",txtAd.Text);
            komut2.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3",msbTc.Text);
            komut2.Parameters.AddWithValue("@p4",msbTel.Text);
            komut2.Parameters.AddWithValue("@p5",txtSifre.Text);
            komut2.Parameters.AddWithValue("@p6",cmbCinsiyet.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Hasta Güncellendi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
