using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViduKetnoiCSDL
{
    public partial class frmChatLieu : Form
    {

        public frmChatLieu()
        {
            InitializeComponent();
        }

        Database dt = new Database();


        private void btnThem_Click(object sender, EventArgs e)
        {
            Reset();
            btnLuu.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaChatLieu.Focus();
        }

        private void Reset()
        {
            txtMaChatLieu.Text = "";
            txtTenChatLieu.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e) //phải luôn có câu hỏi 'bạn có muốn xóa ko?'
        {

            if (MessageBox.Show("Bạn có chắc chắn xóa  " + txtMaChatLieu.Text 
                + " không? Nếu có ấn nút Lưu, không thì ấn nút Hủy", "Xóa sản phẩm", 
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string sql = "delete from ChatLieu where MaChatLieu='" + txtMaChatLieu.Text + "'";
                    dt.ExcuteNonQuery(sql);
                    string sql1 = "select * from ChatLieu";
                    dgvChatLieu.DataSource = dt.LoadDataToTable(sql1);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Xóa ko nổi.. " + ex.ToString());
                }
            }

        }

        int dong;
        private void dgvChatLieu_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dong = e.RowIndex; //dòng
            txtMaChatLieu.Text = dgvChatLieu.Rows[dong].Cells[0].Value.ToString();
            txtTenChatLieu.Text = dgvChatLieu.Rows[dong].Cells[1].Value.ToString();
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql = "update ChatLieu set TenChatLieu='" + txtTenChatLieu.Text + "' where MaChatLieu='" + txtMaChatLieu.Text + "'";
            dt.ExcuteNonQuery(sql);
            LoadDMChatLieu();
        }

        private void LoadDMChatLieu()
        {
            string sql1 = "select * from ChatLieu";
            dgvChatLieu.DataSource = dt.LoadDataToTable(sql1);
        }

        private void frmChatLieu_Load(object sender, EventArgs e)
        {
            LoadDMChatLieu();
            //string sql1 = "select * from ChatLieu";
            //dgvChatLieu.DataSource = dt.TaoBang(sql1);
        }

        private void dgvChatLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgvChatLieu.Rows[e.RowIndex];
            txtMaChatLieu.Text = Convert.ToString(row.Cells["machatlieu"].Value);
            txtTenChatLieu.Text = Convert.ToString(row.Cells["tenchatlieu"].Value);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //check dữ liệu ko dc để trống
            //if (txtMaChatLieu.Text.Trim() == "")
            //{
            //    MessageBox.Show("Mã Chất liệu ko đc để trống!");
            //    txtMaChatLieu.Focus();
            //    return;
            //}
            //if (txtTenChatLieu.Text.Trim() == "")
            //{
            //    MessageBox.Show("Ten Chất liệu ko đc để trống!");
            //    txtTenChatLieu.Focus();
            //    return;
            //}

            //Check xem có bị trùng key ko
            //string sql1 = "select * from ChatLieu where MaChatLieu = '"
            //            + txtMaChatLieu.Text + "'";

            string sql = "insert into ChatLieu values('" + txtMaChatLieu.Text + "','" + txtTenChatLieu.Text + "')";
            try
            {
                dt.ExcuteNonQuery(sql);
                btnLuu.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnCapNhat.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Đã tồn tại mã chất liệu", "Úi có lỗi");
            }
            string sql1 = "select * from ChatLieu";
            dgvChatLieu.DataSource = dt.LoadDataToTable(sql1);
            //LoadDMChatLieu();
        }
    }
}
