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
        string connection = @"Data Source=LAPTOP-IBB8RS68\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adt = new SqlDataAdapter();
        DataTable dt = new DataTable();
     
        void load_datat()
        {
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from HSinh";
            adt.SelectCommand = cmd;
            dt.Clear();
            adt.Fill(dt);
            dataGridView3.DataSource = dt;
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
            CBOGioiTinh.DataSource = ListGioiTinh;
            conn = new SqlConnection(connection);
            conn.Open();

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
        private void button4_Click(object sender, EventArgs e)
        {

            string hoten = textHoTen.Text;
            string Id = TxBId.Text;
            string Diachi = TxbDiaChi.Text;
            string MaLop = TxBId.Text;
            string Sdt = TxbSdt.Text;
            string Stt = TxbStt.Text;
            string Gioitinh = CBOGioiTinh.SelectedItem.ToString();
            string Ngaysinh = dateTimePicker1.Value.ToShortDateString();
            if (KiemtraNhap())
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into Hsinh values('" + Id + "','" + MaLop + "','" + Sdt + "','" + hoten + "','" + Gioitinh + "','" + Ngaysinh + "','" + Diachi + "','" + Stt + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã tiếp nhận học sinh");
                conn = new SqlConnection(connection);
                conn.Open();
                load_datat();

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

        }
    }
    }
