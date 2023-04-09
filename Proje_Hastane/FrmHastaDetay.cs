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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;

        sqlbaglantisi bgl = new sqlbaglantisi();

        //Ad Soyad çekme
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;

            SqlCommand komut = new SqlCommand("SELECT HastaAd,HastaSoyad From Tbl_Hastalar Where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) {
                lblAd.Text = dr[0].ToString();
                lblSoyad.Text = dr[1].ToString();
                
            }
            bgl.baglanti().Close();

            //Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Randevular WHERE HastaTC=" + tc,bgl.baglanti());
            da.Fill(dt);//bu işlemi bir araştır//sanal tablo oluşturma gibi
            dataGridView1.DataSource = dt;


            //Branşları çekme
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read()) {
                //  cmbBrans.Text = dr2[0].ToString(); // bu koda göre veri tabanındaki son bransa eşitlenir
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

            

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bransa göre doktor listeleme cmbde
            cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar Where DoktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read()) {
                cmbDoktor.Items.Add(dr3[0]+" " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //doktor seçince cmbtan `data gridde randevuları listele
            /* SqlCommand komut4 = new SqlCommand("SELECT * FROM Tbl_Randevular WHERE RandevuDoktor=@p1 And RandevuBrans=@p2",bgl.baglanti());
             komut4.Parameters.AddWithValue("@p1",cmbDoktor.Text);
             komut4.Parameters.AddWithValue("@p2",cmbBrans.Text);          // bu kod bloğundan gurur duyuyorum ama datagride veri çekmek için doğru yol değil
             SqlDataReader dr4 = komut4.ExecuteReader();
             while (dr4.Read()) {}
             */
            //aktif randevular
            //bu kodlar başarılı
             DataTable dt2 = new DataTable();
             SqlDataAdapter da2 = new SqlDataAdapter("Select * From Tbl_Randevular Where RandevuBrans='"+ cmbBrans.Text +"' And RandevuDoktor='"+cmbDoktor.Text+"' And RandevuDurum=0",bgl.baglanti());
             da2.Fill(dt2);
             dataGridView2.DataSource = dt2;
           
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = lblTc.Text;
            fr.Show();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {//güncellemede dokotr brans gibi özelliklere etki edemiyoruz ve böylece raandevualrı bozmuyoruz hasta, hasta tc(formdan) ve sikayeti hastadan alıyoruz
            SqlCommand komut = new SqlCommand("Update Tbl_Randevular Set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 Where Randevuid=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTc.Text);
            komut.Parameters.AddWithValue("@p2",rtxtSikayet.Text);
            komut.Parameters.AddWithValue("p3",txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu alındı","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);


        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;

            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();//sadece id ile iş yapıcaz burada 
           // cmbBrans.Text= dataGridView2.Rows[secilen].Cells[1].Value.ToString();
           // cmbDoktor.Text= dataGridView2.Rows[secilen].Cells[2].Value.ToString();
          //  rtxtSikayet.Text= dataGridView2.Rows[secilen].Cells[3].Value.ToString();

        }
    }
}
