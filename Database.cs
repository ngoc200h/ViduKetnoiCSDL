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

namespace ViduKetnoiCSDL
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
        public DataTable LoadDataToTable(string sql) //LoadDataTable
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

        //public static bool CheckKey(string sql)
        //{
        //    SqlConnection con = TaoKetNoi();
        //    SqlCommand myCommand = new SqlCommand(sql, con);
        //    SqlDataReader myReader = myCommand.ExecuteReader();
        //    if (myReader.HasRows)
        //        return true;
        //    else
        //        return false;
        //}

        public void ExcuteNonQuery(string sql)
        {
            //SqlConnection con = TaoKetNoi();
            SqlCommand cmd = new SqlCommand(sql, Conn);
            //con.Open();
            cmd.ExecuteNonQuery();
            //con.Close();
            cmd.Dispose();
        }

    }
}
