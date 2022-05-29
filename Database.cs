using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViduKetnoiCSDL.Class
{
    class Database
    {
        //lớp database có thể gọi là DAO
        public static SqlConnection Conn;  //Khai báo đối tượng kết nối
        public static string connString;   //Khai báo biến chứa chuỗi kết nối

        public static void Connect()
        {
           connString =  "Data Source=NGOC9YO\\SQLEXPRESS;" +
                                    "Initial Catalog = QLyBanHang;" +
                                    "Integrated Security=True";
            Conn = new SqlConnection();
            Conn.ConnectionString = connString;

            try
            {
                Conn.Open();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.ToString());
            }
        }

        public static void Disconnect()
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();   	//Đóng kết nối
                Conn.Dispose();     //Giải phóng tài nguyên
                Conn = null;
            }
        }

        public static DataTable LoadDataToTable(string sql) //LoadDataTable hoặc GetDataToTable
        {
            SqlDataAdapter Mydata = new SqlDataAdapter();	// Khai báo
            // Tạo đối tượng Command thực hiện câu lệnh SELECT        
            Mydata.SelectCommand = new SqlCommand();
            Mydata.SelectCommand.Connection = Database.Conn; 	// Kết nối CSDL
            Mydata.SelectCommand.CommandText = sql;	// Gán câu lệnh SELECT
            DataTable table = new DataTable();    // Khai báo DataTable nhận dữ liệu trả về
            Mydata.Fill(table);
            //Thực hiện câu lệnh SELECT và đổ dữ liệu vào bảng table
            return table;

        }
        public static void FillDataToCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Conn);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma;    // Truong gia tri
            cbo.DisplayMember = ten;    // Truong hien thi
        }

        public static bool CheckKey(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Conn);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }


        public void ExcuteNonQuery(string sql)
        {
            //SqlConnection con = TaoKetNoi();
            SqlCommand cmd = new SqlCommand(sql, Conn);
            //con.Open();
            cmd.ExecuteNonQuery();
            //con.Close();
            cmd.Dispose();
        }

        public static void RunSql(string sql)
        {
            SqlCommand cmd;		                // Khai báo đối tượng SqlCommand
            cmd = new SqlCommand();	         // Khởi tạo đối tượng
            cmd.Connection = Class.Database.Conn;	  // Gán kết nối
            cmd.CommandText = sql;			  // Gán câu lệnh SQL
            try
            {
                cmd.ExecuteNonQuery();		  // Thực hiện câu lệnh SQL
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }

        public static void RunSqlDel(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.Database.Conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Dữ liệu đang được dùng, không thể xóa...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            cmd.Dispose();
            cmd = null;
        }
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, Class.Database.Conn);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;
        }


    }
}
