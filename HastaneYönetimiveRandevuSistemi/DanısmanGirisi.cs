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
    public partial class DanısmanGirisi : Form
    {
        public DanısmanGirisi()
        {
            InitializeComponent();
        }
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from sekreterbilgiler where sekretertc=@p1 and sekretersifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTc.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                DanısmanBilgileri dnb = new DanısmanBilgileri();
                dnb.TCnumara = MskTc.Text;
                dnb.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Bilgileri Hatalı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
        }
    }
}
