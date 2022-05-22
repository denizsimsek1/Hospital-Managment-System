using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneYönetimiveRandevuSistemi
{
    public partial class Girisler : Form
    {
        public Girisler()
        {
            InitializeComponent();
        }

        private void BtnHastaGirisi_Click(object sender, EventArgs e)
        {
            HastaGirisi hstgir = new HastaGirisi();
            hstgir.Show();
            this.Hide();
        }

        private void BtnDoktorGirisi_Click(object sender, EventArgs e)
        {
            DoktorGirisi drgir = new DoktorGirisi();
            drgir.Show();
            this.Hide();
        }

        private void BtnDanısmanGirisi_Click(object sender, EventArgs e)
        {
            DanısmanGirisi dnsgir = new DanısmanGirisi();
            dnsgir.Show();
            this.Hide();
        }
    }
}
