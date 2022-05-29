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
using ViduKetnoiCSDL.Class;

namespace ViduKetnoiCSDL
{
    public partial class frmChatLieu : Form
    {

        public frmChatLieu()
        {
            InitializeComponent();
        }

        DataTable tblCL;

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaChatLieu.Enabled = true;
            txtMaChatLieu.Focus();
        }

        private void ResetValues()
        {
            txtMaChatLieu.Text = "";
            txtTenChatLieu.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e) //phải luôn có câu hỏi 'bạn có muốn xóa ko?'
        {
            string sql;
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaChatLieu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE ChatLieu WHERE MaChatLieu=N'" + txtMaChatLieu.Text + "'";
                Class.Database.RunSqlDel(sql);
                LoadDMChatLieu();
                ResetValues();
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
            //string sql = "update ChatLieu set TenChatLieu='" + txtTenChatLieu.Text + "' where MaChatLieu='" + txtMaChatLieu.Text + "'";
            //dt.ExcuteNonQuery(sql);
            //LoadDMChatLieu();
            string sql;
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaChatLieu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenChatLieu.Focus();
                return;
            }
            sql = "UPDATE ChatLieu SET TenChatLieu=N'" + txtTenChatLieu.Text.ToString() + "' WHERE MaChatLieu=N'" + txtMaChatLieu.Text + "'";
            Class.Database.RunSql(sql);
            LoadDMChatLieu();
            ResetValues();
            btnBoQua.Enabled = false;

        }

        private void LoadDMChatLieu() //Load_DataGridView()
        {
            string sql = "select * from ChatLieu";
            tblCL = Class.Database.LoadDataToTable(sql);
            dgvChatLieu.DataSource = tblCL;
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dgvChatLieu.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dgvChatLieu.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void frmChatLieu_Load(object sender, EventArgs e)
        {
            txtMaChatLieu.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            //check dữ liệu ko dc để trống
            if (txtMaChatLieu.Text.Trim() == "")
            {
                MessageBox.Show("Mã Chất liệu ko đc để trống!");
                txtMaChatLieu.Focus();
                return;
            }
            if (txtTenChatLieu.Text.Trim() == "")
            {
                MessageBox.Show("Ten Chất liệu ko đc để trống!");
                txtTenChatLieu.Focus();
                return;
            }
            sql = "SELECT MaChatlieu FROM ChatLieu WHERE MaChatlieu=N'" +
            txtMaChatLieu.Text.Trim() + "'";
            if (Class.Database.CheckKey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaChatLieu.Focus();
                txtMaChatLieu.Text = "";
                return;
            }
            sql = "INSERT INTO ChatLieu(MaChatlieu,TenChatLieu) VALUES(N'" + txtMaChatLieu.Text + "',N'" + txtTenChatLieu.Text + "')";
            Class.Database.RunSql(sql);
            LoadDMChatLieu();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaChatLieu.Enabled = false;

        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaChatLieu.Enabled = false;
        }

        private void txtMaChatLieu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
