using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ViduKetnoiCSDL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Database.Connect();       
        }

        private void chatLieuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChatLieu f = new frmChatLieu();
            f.Show();
        }

        private void hàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDMHang f = new frmDMHang();
            f.Show();
        }
    }
}
