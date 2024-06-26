using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class Taikhoan
    {
        private string tenTaiKhoan;
        private string Matkhau;

        public Taikhoan()
        {
            
        }
        public Taikhoan(string tenTaiKhoan, string Matkhau)
        {
            this.tenTaiKhoan = tenTaiKhoan; 
            this.Matkhau = Matkhau; 
        }

        public string TenTaiKhoan { get => tenTaiKhoan; set => tenTaiKhoan = value; }
        public string Matkhau1 { get => Matkhau; set => Matkhau = value; }
    }
}
