﻿using System;
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
    public partial class RandevuListesi : Form
    {
        public RandevuListesi()
        {
            InitializeComponent();
        }
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        private void RandevuListesi_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from randevular", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
