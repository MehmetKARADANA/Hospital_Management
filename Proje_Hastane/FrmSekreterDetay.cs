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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

       
        public string tc;
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;

            //Ad Soyadı getireceğim
            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter Where SekreterTC=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",tc);
            SqlDataReader dr1 = komut1.ExecuteReader();
            if (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //branşları datagride aktarma
            DataTable dt1 = new DataTable();            //tablo oluşturduk
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tbl_Branslar",bgl.baglanti());//datadapter ile tablo için kullanılacak veriyi aldık
            da1.Fill(dt1);//bir sanal tablo(gibide olabilir) oluşturduk
            dataGridView1.DataSource = dt1;//datagridin kaynağını dt1 yaptık

            //Doktorları çekiyoruz
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd+' '+DoktorSoyad) AS 'Doktorlar',DoktorBrans From Tbl_Doktorlar",bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşı comboboxa aktarma
            SqlCommand komut3 = new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut3.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0].ToString());
            }

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {   //yeni randevu ekleme
            SqlCommand komut2 = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@p1,@p2,@p3,@p4)",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",msbTarih.Text);
            komut2.Parameters.AddWithValue("@p2", msbSaat.Text);
            komut2.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut2.Parameters.AddWithValue("@p4", cmbDoktor.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Eklendi");

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {   //brans seçince doktorları cmb ye atan kod
            cmbDoktor.Items.Clear();
            SqlCommand komut4 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar Where DoktorBrans=@p1",bgl.baglanti());
            komut4.Parameters.AddWithValue("@p1",cmbBrans.Text);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read()) {
                cmbDoktor.Items.Add( dr4[0].ToString()+" " + dr4[1].ToString() );
            }
            bgl.baglanti().Close();

        }

        private void btnolustur_Click(object sender, EventArgs e)
        {//Duyuru oluşturuyoruz
            SqlCommand komut5 = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@p1)",bgl.baglanti());
            komut5.Parameters.AddWithValue("@p1",richTextBox1.Text);
            komut5.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frd = new FrmDoktorPaneli();
            frd.Show();
        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frr = new FrmRandevuListesi();
            frr.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frd = new FrmDuyurular();
            frd.Show();
        }
    }
}
