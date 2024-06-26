using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        string connection = @"Data Source=LAPTOP-P0OHVEG9;Initial Catalog=QLHSnew;Integrated Security=True;Encrypt=False";
        SqlConnection conn;
        SqlDataAdapter adt = new SqlDataAdapter();
        public string IdLop { get; set; }
        string idlop;
        public Form2(string idlop)
        {
            InitializeComponent();
            IdLop = idlop;
        }
        void load_datat()
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM HSinh";
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    DataTable dtHSinh = new DataTable();
                    adt.Fill(dtHSinh);
                    dataGridView4.DataSource = dtHSinh;
                }
            }
        }
        void load_datat(SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM HSinh";
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    DataTable dtHSinh = new DataTable();
                    adt.Fill(dtHSinh);
                    dataGridView4.DataSource = dtHSinh;
                }
            }
        }
        public Form2()
        {
            InitializeComponent();
        }


        bool KiemtraNhap()
        {
            if (textHoTen.Text == "")
            {
                MessageBox.Show("Hãy nhập lại họ và tên", "Thông báo");
                textHoTen.Focus();
                return false;
            }
            if (TxBId.Text == "")
            {
                MessageBox.Show("Hãy nhập lại Id", "Thông báo");
                TxBId.Focus();
                return false;
            }
            if (TxbDiaChi.Text == "")
            {
                MessageBox.Show("Hãy nhập lại địa chỉ", "Thông báo");
                TxbDiaChi.Focus();
                return false;
            }
            if (TxbSdt.Text == "")
            {
                MessageBox.Show("Hãy nhập lại Số điện thoại", "Thông báo");
                TxbSdt.Focus();
                return false;
            }
            if (TxbStt.Text == "")
            {
                MessageBox.Show("Hãy nhập lại Số thứ tự", "Thông báo");
                TxbStt.Focus();
                return false;
            }

            return true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (KiemtraNhap())
            {
                if (string.IsNullOrEmpty(TxbMaLop.Text))
                {
                    string idHocsinh = TxBId.Text;
                    string malop = IdLop; // Lấy mã lớp từ thuộc tính IdLop được truyền từ Form1

                    // Tạo kết nối với cơ sở dữ liệu
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();

                        // Cập nhật giá trị MaLop trong bảng HSinh
                        using (SqlCommand cmd = new SqlCommand("UPDATE HSinh SET MaLop = @MaLop WHERE idHocSinh = @idHocSinh", conn))
                        {
                            cmd.Parameters.AddWithValue("@MaLop", malop);
                            cmd.Parameters.AddWithValue("@idHocSinh", idHocsinh);
                            cmd.ExecuteNonQuery();
                        }

                        // Cập nhật giá trị MaLop vào TextBox
                        TxbMaLop.Text = malop;

                        // Reload lại dữ liệu bảng HSinh để hiển thị thông tin mới
                        load_datat(conn);
                    }

                    MessageBox.Show("Đã cập nhật mã lớp cho học sinh.");
                }

            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView4.CurrentRow.Index;
            textHoTen.Text = dataGridView4.Rows[i].Cells[3].Value.ToString();
            TxbSdt.Text = dataGridView4.Rows[i].Cells[2].Value.ToString();
            TxbDiaChi.Text = dataGridView4.Rows[i].Cells[6].Value.ToString();
            CBOGioiTinh.Text = dataGridView4.Rows[i].Cells[4].Value.ToString();
            TxBId.Text = dataGridView4.Rows[i].Cells[0].Value.ToString();
            TxbStt.Text = dataGridView4.Rows[i].Cells[7].Value.ToString();
            TxbMaLop.Text = dataGridView4.Rows[i].Cells[1].Value.ToString();
            // Bạn có thể cần phải chuyển đổi giá trị này sang kiểu DateTime nếu cần thiết
            string dateValue = dataGridView4.Rows[i].Cells[5].Value.ToString();
            DateTime parsedDate;
            if (DateTime.TryParse(dateValue, out parsedDate))
            {
                dateTimePicker1.Value = parsedDate;
            }
            else
            {
                MessageBox.Show("Giá trị ngày tháng không hợp lệ");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        List<string> ListGioiTinh = new List<string>() { "Nam", "Nu", "Khong xac dinh" };
        private void Form2_Load(object sender, EventArgs e)
        {
            CBOGioiTinh.DataSource = ListGioiTinh;

            // Mở kết nối và tải dữ liệu khi form tải
            using (conn = new SqlConnection(connection))
            {
                conn.Open();
                load_datat();
            }
        }
    }
}