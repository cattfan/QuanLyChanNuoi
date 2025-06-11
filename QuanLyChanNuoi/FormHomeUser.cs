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
    public partial class FormHomeUser : Form
    {
        public FormHomeUser()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form chuồng nuôi
            FormChuongNuoi formChuongNuoi = new FormChuongNuoi();
            formChuongNuoi.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form chuồng nuôi đóng
            formChuongNuoi.Show();
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Không có ai nhắn tin cho ban cả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã ở phiên bản mới nhất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
