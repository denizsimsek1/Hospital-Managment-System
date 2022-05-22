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
    public partial class HastaGirisi : Form
    {
        public HastaGirisi()
        {
            InitializeComponent();
        }
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UyeOl uye = new UyeOl();
            uye.Show();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from HastaBilgiler where HastaTc=@p1 and HastaSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTc.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                HastaBilgileri hstbil = new HastaBilgileri();
                hstbil.tc = MskTc.Text;
                hstbil.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC No veya Şifre","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();

        }

        private void HastaGirisi_Load(object sender, EventArgs e)
        {

        }
    }
}
