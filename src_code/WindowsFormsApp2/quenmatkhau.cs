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
    public partial class quenmatkhau : Form
    {
        public quenmatkhau()
        {
            InitializeComponent();
            label2.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        modify modify = new modify();
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox3.Text;
            if(email.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại đăng ký ");
            }
            else
            {
                string query = "Select * from Accounts where Email = '" + email + "'";
                if(modify.Taikhoans(query).Count != 0)
                {
                    label2.ForeColor = Color.Blue;
                    label2.Text = "Mật khẩu: " + modify.Taikhoans(query)[0].Matkhau1;
                }
                else
                {
                    label2.ForeColor = Color.Red;
                    label2.Text = "Tài khoản chưa được đăng kí";
                }
            }
        }
    }
}
