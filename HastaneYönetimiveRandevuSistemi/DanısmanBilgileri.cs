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

namespace HastaneYönetimiveRandevuSistemi
{
    public partial class DanısmanBilgileri : Form
    {
        public DanısmanBilgileri()
        {
            InitializeComponent();
        }
        public string TCnumara;
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        private void DanısmanBilgileri_Load(object sender, EventArgs e)
        {
            LblTc.Text = TCnumara;

            //Adsoyad
            SqlCommand komut1 = new SqlCommand("select sekreteradsoyad from sekreterbilgiler where sekretertc=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();


            //Bransları Datagride Aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktorları Datagride Aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select DoktorAd,DoktorSoyad,DoktorBrans from doktorbilgiler", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Bransları ComboBox'a Aktarma
            SqlCommand komut2 = new SqlCommand("select BransAd from Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2", MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", CmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad from doktorbilgiler where DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void BtnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Duyurular (Duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDrPanel_Click(object sender, EventArgs e)
        {
            DoktorPaneli drp = new DoktorPaneli();
            drp.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            BransPaneli br = new BransPaneli();
            br.Show();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            RandevuListesi rd = new RandevuListesi();
            rd.Show();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Txtid.Text = "";
            MskSaat.Text = "";
            MskTarih.Text = "";
            MskTc.Text = "";
            CmbBrans.Text = "";
            CmbDoktor.Text = "";
            ChkDurum.Checked = false;
            Txtid.Focus();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            Duyurular dyr = new Duyurular();
            dyr.Show();
        }
    }
}
