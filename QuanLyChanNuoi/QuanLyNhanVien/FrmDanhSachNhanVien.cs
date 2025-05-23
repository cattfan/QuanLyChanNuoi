using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyChanNuoi.QuanLyNhanVien
{
    public partial class FrmDanhSachNhanVien : Form
    {
        public FrmDanhSachNhanVien()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmThemNhanVien NhanVien = new FrmThemNhanVien();
            NhanVien.FormClosed += (s, args) => this.Show();
            NhanVien.FormTitle = "Thêm Nhân Viên";
            NhanVien.Show();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmThemNhanVien NhanVien = new FrmThemNhanVien();
            NhanVien.FormClosed += (s, args) => this.Show();
            NhanVien.FormTitle = "Sửa Nhân Viên";
            NhanVien.Show();
        }
    }
}
