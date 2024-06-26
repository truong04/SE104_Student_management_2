using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class ket_Noi : Form
    {
        public ket_Noi()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        modify modify = new modify();
        private void button1_Click(object sender, EventArgs e)
        {
            string tentk = textBox_tentk.Text;
            string Mk = textBox_mk.Text;
            if (tentk.Trim() == "") { MessageBox.Show("Vui lòng nhập lại tên tài khoản"); return; }
            else if (Mk.Trim() == "") { MessageBox.Show("Vui lòng nhập lại mật khẩu"); return; }
            else
            {
                string query = "Select * from account where SDT = '" + tentk + "' and MatKhau= '" + Mk + "'";
                if (modify.Taikhoans(query).Count != 0)
                {
                    MessageBox.Show("Đăng nhập thành công");
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng");
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            quenmatkhau f = new quenmatkhau();
            f.ShowDialog();

        }
    }
}
