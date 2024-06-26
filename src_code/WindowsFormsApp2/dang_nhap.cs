using GiaoDien;
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
    public partial class dang_nhap : Form
    {
        public dang_nhap()
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
                string query = "Select * from account where SDT = '" + tentk+"' and MatKhau= '"+Mk+"'";
                if (modify.Taikhoans(query).Count != 0)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    this.Hide();
                    Form1 f = new Form1();
                    f.ShowDialog();
                    this.Close();
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

        private void Btn_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if(Btn_checkbox.Checked)
                textBox_mk.UseSystemPasswordChar = false;
            else
                textBox_mk.UseSystemPasswordChar= true;
        }

        private void dang_nhap_Load(object sender, EventArgs e)
        {

        }


        private void textBox_tentk_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
