using QuanLyChanNuoi.User_case_Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyChanNuoi
{
    public partial class FormHomeAdmin : Form
    {
        public FormHomeAdmin()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close(); // Đóng form admin
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form chuồng nuôi
            FormChuongNuoi formChuongNuoi = new FormChuongNuoi();
            formChuongNuoi.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form chuồng nuôi đóng
            formChuongNuoi.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form vật nuôi
            FormQuanLyNhanVien formNhanVien = new FormQuanLyNhanVien();
            formNhanVien.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form nhân viên đóng
            formNhanVien.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form vật nuôi
            FormBaocaothongke formbaocao = new FormBaocaothongke();
            formbaocao.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form nhân viên đóng
            formbaocao.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form báo cáo
            FormNhaphoadon formHoaDon = new FormNhaphoadon();
            formHoaDon.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form báo cáo đóng
            formHoaDon.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form thống kê
            FormQuanLyVatTu formVatTu = new FormQuanLyVatTu();
            formVatTu.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form vật tư đóng
            formVatTu.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form vật nuôi
            FormNhaCungCap formNhaCungCap = new FormNhaCungCap();
            formNhaCungCap.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form nhà cung cấp đóng
             formNhaCungCap.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Không có ai nhắn tin cho ban cả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã ở phiên bản mới nhất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

<<<<<<< HEAD
        private void FormHomeAdmin_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

=======
        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form vật nuôi
            FormVatNuoi formvatnuoi = new FormVatNuoi();
            formvatnuoi.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form nhà cung cấp đóng
            formvatnuoi.Show();
>>>>>>> main
        }
    }
}
