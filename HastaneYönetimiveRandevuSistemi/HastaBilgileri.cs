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
    public partial class HastaBilgileri : Form
    {
        public HastaBilgileri()
        {
            InitializeComponent();
        }
        public string tc;
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        private void HastaBilgileri_Load(object sender, EventArgs e)
        {
            LblTc.Text = tc;
            //Adsoyad Çekme
            SqlCommand komut = new SqlCommand("select hastaad,hastasoyad from hastabilgiler where hastatc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from randevular where hastatc=" + tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branş Çekme
            SqlCommand komut2 = new SqlCommand("select bransad from branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select doktorad,doktorsoyad from doktorbilgiler where doktorbrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from randevular where randevubrans='" + CmbBrans.Text+"'"+" and RandevuDoktor='"+CmbDoktor.Text+"' and RandevuDurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LnkBilgi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BilgiDüzenle bl = new BilgiDüzenle();
            bl.TCno = LblTc.Text;
            bl.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Randevular set RandevuDurum=1,HastaTc=@p1,HastaSikayet=@p2 where Randevuid=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTc.Text);
            komut.Parameters.AddWithValue("@p2", RchSikayet.Text);
            komut.Parameters.AddWithValue("@p3", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevunuz Onaylandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
