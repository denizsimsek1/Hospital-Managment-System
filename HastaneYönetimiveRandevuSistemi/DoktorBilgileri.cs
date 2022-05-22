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
    public partial class DoktorBilgileri : Form
    {
        public DoktorBilgileri()
        {
            InitializeComponent();
        }
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public string TC;
        private void DoktorBilgileri_Load(object sender, EventArgs e)
        {
            LblTc.Text = TC;

            //Doktor Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("select DoktorAd, DoktorSoyad from DoktorBilgiler where DoktorTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Randevu çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Randevular where RandevuDoktor='" + LblAdSoyad.Text + "'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            DoktorBilgiDüzenle dbd = new DoktorBilgiDüzenle();
            dbd.TCNO = LblTc.Text;
            dbd.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            Duyurular dy = new Duyurular();
            dy.Show();
        }

        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
