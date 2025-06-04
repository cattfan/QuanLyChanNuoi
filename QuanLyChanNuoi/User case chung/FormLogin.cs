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
    public partial class FormLogin : Form
    {
        private Dictionary<string, (string password, string role)> taiKhoans = new Dictionary<string, (string, string)>
        {
            { "admin", ("admin123", "Admin") },
            { "nhanvien", ("123456", "NhanVien") }
        };
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassWord.Text.Trim();

            if (taiKhoans.ContainsKey(username))
            {
                var account = taiKhoans[username];

                if (account.password == password)
                {
                    // Phân quyền và mở form tương ứng
                    if (account.role == "Admin")
                    {
                        FormHomeAdmin adminForm = new FormHomeAdmin();
                        adminForm.FormClosed += (s, args) => this.Show(); // Hiển thị lại form đăng nhập khi form admin đóng
                        adminForm.Show();
                    }
                    else if (account.role == "NhanVien")
                    {
                        FormHomeUser nvForm = new FormHomeUser();
                        nvForm.FormClosed += (s, args) => this.Show(); // Hiển thị lại form đăng nhập khi form nhân viên đóng
                        nvForm.Show();
                    }

                    this.Hide(); // Ẩn form đăng nhập
                    txtPassWord.Clear();
                    txtUserName.Clear();
                    chkHienMatKhau.Checked = false;
                    return;
                }
            }
            MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassWord.Clear();
            txtUserName.Clear();
            chkHienMatKhau.Checked = false;
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtPassWord.PasswordChar = chkHienMatKhau.Checked ? '\0' : '*';
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtPassWord.Clear();
            txtUserName.Clear();
            chkHienMatKhau.Checked = false;
        }

        private void FormLogin_Shown(object sender, EventArgs e)
        {
         
        }
    }
}

