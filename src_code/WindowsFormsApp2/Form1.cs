using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using WindowsFormsApp2;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GiaoDien
{
    public partial class Form1 : Form
    {
        string connection = @"Data Source=LAPTOP-P0OHVEG9;Initial Catalog=QLHSnew;Integrated Security=True;Encrypt=False";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adt = new SqlDataAdapter();
        DataTable dt = new DataTable();

        void load_datat()
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM HSinh";
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    DataTable dtHSinh = new DataTable();
                    adt.Fill(dtHSinh);
                    dataGridView3.DataSource = dtHSinh;
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
                    dataGridView3.DataSource = dtHSinh;
                }
            }
        }

        void load_datat2()
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Lop";
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    DataTable dtLop = new DataTable();
                    adt.Fill(dtLop);
                    dtv.DataSource = dtLop;
                }
            }
        }
        void load_datat2(SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Lop";
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    DataTable dtLop = new DataTable();
                    adt.Fill(dtLop);
                    dtv.DataSource = dtLop; // Đảm bảo rằng dtv là DataGridView của bạn
                }
            }
        }


        public Form1()
        {
            InitializeComponent();
            tabControl3.DrawItem += new DrawItemEventHandler(tabControl3_DrawItem);
            tabControl2.DrawItem += new DrawItemEventHandler(tabControl3_DrawItem);
            tabControl4.DrawItem += new DrawItemEventHandler(tabControl3_DrawItem);
            conn = new SqlConnection(connection);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Thiết lập nguồn dữ liệu cho ComboBox giới tính
            CBOGioiTinh.DataSource = ListGioiTinh;

            // Mở kết nối và tải dữ liệu khi form tải
            using (conn = new SqlConnection(connection))
            {
                conn.Open();
                load_datat();
                load_datat2();
            }
        }
        

        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chứcNăngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tabControl3_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            Graphics g = e.Graphics;
            Brush _textBrush;

            TabPage _tabPage = tabControl.TabPages[e.Index];
            Rectangle _tabBounds = tabControl.GetTabRect(e.Index);

            _textBrush = new SolidBrush(SystemColors.ControlText);
            Font _tabFont = new Font("Arial", 10.0f, FontStyle.Bold, GraphicsUnit.Pixel);
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;

            // Vẽ văn bản với khoảng cách
            Rectangle textBounds = _tabBounds;
            textBounds.Offset(-20, 3); // Dịch chuyển văn bản xuống một chút
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, textBounds, _stringFlags);

            // Đo kích thước của văn bản
            SizeF textSize = g.MeasureString(_tabPage.Text, _tabFont);

            // Tính toán vị trí mới cho ảnh
            float imageX = _tabBounds.Right - 20 - (tabControl.ImageList.ImageSize.Width); // 20 là khoảng cách giữa ảnh và viền phải của tab
            float imageY = _tabBounds.Top + (_tabBounds.Height - tabControl.ImageList.ImageSize.Height) / 2;


            // Vẽ ảnh từ ImageList của tabControl
            if (e.Index < tabControl.ImageList.Images.Count)
            {
                System.Drawing.Image tabImage = tabControl.ImageList.Images[e.Index];
                g.DrawImage(tabImage, imageX, imageY);
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = null;
            //dt.Clear();
            string ten_hs = name.Text;
            string lop = COMBO_LOP.Text;
            string nam_sinh = bday.Text;

            // Kiểm tra xem tất cả các trường thông tin đều trống hay không
            if (string.IsNullOrEmpty(ten_hs) && string.IsNullOrEmpty(lop) && string.IsNullOrEmpty(nam_sinh))
            {
                MessageBox.Show("Xin hãy nhập thông tin để thực hiện tìm kiếm.");
                return; // Thoát khỏi phương thức để không thực hiện truy vấn
            }

            // Tiếp tục với các bước truy vấn nếu có ít nhất một trường thông tin không trống
            string query = "SELECT DISTINCT HoVaTen, Diem45, Diem15 FROM HSinh, Lop, Diem_HS, MONHOC WHERE HSinh.MaLop = Lop.IdLop AND HSinh.idHocSinh = Diem_HS.idHocSinh AND Diem_HS.idMonHoc = MONHOC.MAMH";

            // Thêm điều kiện cho tên học sinh nếu được cung cấp
            if (!string.IsNullOrEmpty(ten_hs))
            {
                query += " AND HoVaTen = '" + ten_hs + "'";
            }

            // Thêm điều kiện cho lớp nếu được cung cấp
            if (!string.IsNullOrEmpty(lop))
            {
                query += " AND TenLop = '" + lop + "'";
            }

            // Thêm điều kiện cho năm sinh nếu được cung cấp
            if (!string.IsNullOrEmpty(nam_sinh))
            {
                query += " AND NamSinh = '" + nam_sinh + "'";
            }

            try
            {
                conn.Open();
                cmd = new SqlCommand(query, conn);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        List<string> ListGioiTinh = new List<string>() { "Nam", "Nu", "Khong xac dinh" };
        private void textHoTeHoTen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        modify modify = new modify();
        void loaddata()
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các điều khiển
            string Idlop = txtidlop.Text;
            string IdGVCN = txtidGv.Text;
            string TenLop = txtTenLop.Text;
            string Khoi = txtkhoi.Text;
            int siso = 0;

            // Kiểm tra nhập liệu
            if (KiemtraNhap2())
            {
                // Tạo kết nối cơ sở dữ liệu mới và mở kết nối
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Tạo và thực thi câu lệnh SQL để chèn dữ liệu vào bảng Lop
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Lop (IdLop, IdGVCN, TenLop, SiSo, Khoi) VALUES (@IdLop, @IdGVCN, @TenLop, @SiSo, @Khoi)";
                        cmd.Parameters.AddWithValue("@IdLop", Idlop);
                        cmd.Parameters.AddWithValue("@IdGVCN", IdGVCN);
                        cmd.Parameters.AddWithValue("@TenLop", TenLop);
                        cmd.Parameters.AddWithValue("@SiSo", siso);
                        cmd.Parameters.AddWithValue("@Khoi", Khoi);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Lớp đã được tạo");
                    }

                    // Tải lại dữ liệu
                    load_datat2(conn);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string hoten = textHoTen.Text;
            string MaLop = null; // Giá trị MaLop hiện tại là null
            string Diachi = TxbDiaChi.Text;
            string ID = TxBId.Text;
            string Sdt = TxbSdt.Text;
            string Stt = TxbStt.Text;
            string Gioitinh = CBOGioiTinh.SelectedItem.ToString();
            string Ngaysinh = dateTimePicker1.Value.ToShortDateString();

            if (KiemtraNhap())
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Kiểm tra xem SDT có tồn tại trong bảng account hay không
                    string checkQuery = "SELECT COUNT(*) FROM account WHERE SDT = @SDT";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@SDT", Sdt);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            // Nếu không tồn tại, chèn giá trị vào bảng account
                            string insertAccountQuery = "INSERT INTO account (SDT) VALUES (@SDT)";
                            using (SqlCommand insertAccountCmd = new SqlCommand(insertAccountQuery, conn))
                            {
                                insertAccountCmd.Parameters.AddWithValue("@SDT", Sdt);
                                insertAccountCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Chèn giá trị vào bảng Hsinh
                    string insertHsinhQuery = "INSERT INTO Hsinh (idHocSinh, MaLop, SDT, HoVaTen, GioiTinh, NgayGioSinh, DiaChi, STT) VALUES (@Id, @MaLop, @Sdt, @hoten, @Gioitinh, @Ngaysinh, @Diachi, @Stt)";
                    using (SqlCommand insertHsinhCmd = new SqlCommand(insertHsinhQuery, conn))
                    {
                        insertHsinhCmd.Parameters.AddWithValue("@Id", ID);
                        insertHsinhCmd.Parameters.AddWithValue("@MaLop", MaLop ?? (object)DBNull.Value); // Sử dụng DBNull.Value nếu MaLop là null
                        insertHsinhCmd.Parameters.AddWithValue("@Sdt", Sdt);
                        insertHsinhCmd.Parameters.AddWithValue("@hoten", hoten);
                        insertHsinhCmd.Parameters.AddWithValue("@Gioitinh", Gioitinh);
                        insertHsinhCmd.Parameters.AddWithValue("@Ngaysinh", DateTime.Parse(Ngaysinh));
                        insertHsinhCmd.Parameters.AddWithValue("@Diachi", Diachi);
                        insertHsinhCmd.Parameters.AddWithValue("@Stt", Stt);
                        insertHsinhCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đã tiếp nhận học sinh");
                    load_datat(conn);
                }
            }
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
            if (TxBId.Text == "")
            {
                MessageBox.Show("Hãy nhập lại Mã lớp", "Thông báo");
                TxbMaLop.Focus();
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

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtv.CurrentRow.Index;
            txtidlop.Text = dtv.Rows[i].Cells[0].Value.ToString();
            txtidGv.Text = dtv.Rows[i].Cells[1].Value.ToString();
            txtkhoi.Text = dtv.Rows[i].Cells[2].Value.ToString();
            txtTenLop.Text = dtv.Rows[i].Cells[3].Value.ToString();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
        bool KiemtraNhap2()
        {
            if (txtidlop.Text == "")
            {
                MessageBox.Show("Hãy nhập lại id Lớp", "Thông báo");
                txtidlop.Focus();
                return false;
            }
            if (txtidGv.Text == "")
            {
                MessageBox.Show("Hãy nhập lại Id giáo viên chủ nhiệm", "Thông báo");
                txtidGv.Focus();
                return false;
            }
            if (txtkhoi.Text == "")
            {
                MessageBox.Show("Hãy nhập lại khối", "Thông báo");
                txtkhoi.Focus();
                return false;
            }
            if (txtTenLop.Text == "")
            {
                MessageBox.Show("Hãy nhập lại tên lớp", "Thông báo");
                txtTenLop.Focus();
                return false;
            }
            
            return true;
        }

        private void dtv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtv.CurrentRow.Index;
            txtidlop.Text = dtv.Rows[i].Cells[0].Value.ToString();
            txtidGv.Text = dtv.Rows[i].Cells[1].Value.ToString();
            txtkhoi.Text = dtv.Rows[i].Cells[2].Value.ToString();
            txtTenLop.Text = dtv.Rows[i].Cells[3].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (KiemtraNhap2())
            {
                string idlop = txtidlop.Text;
                int siso = 0;

                // Tạo kết nối với cơ sở dữ liệu
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    // Tạo câu lệnh SQL để lấy giá trị siso hiện tại từ bảng Lop
                    using (SqlCommand cmd = new SqlCommand("SELECT SiSo FROM Lop WHERE IdLop = @IdLop", conn))
                    {
                        cmd.Parameters.AddWithValue("@IdLop", idlop);
                        // Thực thi câu lệnh và lấy giá trị siso
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            siso = (int)result;
                        }
                    }
                    // Tăng giá trị siso lên 1
                    siso += 1;
                    // Cập nhật giá trị siso mới vào bảng Lop
                    using (SqlCommand cmd = new SqlCommand("UPDATE Lop SET SiSo = @SiSo WHERE IdLop = @IdLop", conn))
                    {
                        cmd.Parameters.AddWithValue("@SiSo", siso);
                        cmd.Parameters.AddWithValue("@IdLop", idlop);
                        cmd.ExecuteNonQuery();
                    }
                }
                
                Form2 f = new Form2(idlop);
                f.ShowDialog();

            }
        }
    }
    }
