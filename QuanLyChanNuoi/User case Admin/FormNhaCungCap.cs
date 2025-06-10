using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity; 
namespace QuanLyChanNuoi
{
    public partial class FormNhaCungCap : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB(); 

        public FormNhaCungCap()
        {
            InitializeComponent();
        }

        private void FormNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadNhaCungCap();
            ClearInputFields(); 
        }

        private void LoadNhaCungCap()
        {
            try
            {
                List<NhaCungCap> dsnhacungcap = db.NhaCungCaps.ToList();
                BindGrid(dsnhacungcap); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Nhà cung cấp: " + ex.Message);
            }
        }
        private void BindGrid(List<NhaCungCap> nhaCungCaps)
        {
            dgvNhaCungCap.Rows.Clear();
            foreach (var item in nhaCungCaps)
            {
                int index = dgvNhaCungCap.Rows.Add();
                dgvNhaCungCap.Rows[index].Cells[0].Value = item.MaNhaCungCap;
                dgvNhaCungCap.Rows[index].Cells[1].Value = item.TenNhaCungCap;
                dgvNhaCungCap.Rows[index].Cells[2].Value = item.DiaChi;
            }
        }

        // Hàm để xóa trắng các trường nhập liệu
        private void ClearInputFields()
        {
            txtMaNhaCungCap.Clear();
            txtTenNhaCungCap.Clear();
            txtDiaChiNhaCungCap.Clear();
            // Clear các trường khác nếu có
            // txtSoDienThoai.Clear();
            // txtEmail.Clear();
            txtMaNhaCungCap.Focus(); // Đặt con trỏ vào ô nhập mã
        }

        // Sự kiện click vào DataGridView để hiển thị thông tin lên TextBox
        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo click vào một hàng hợp lệ
            {
                DataGridViewRow row = dgvNhaCungCap.Rows[e.RowIndex];
                txtMaNhaCungCap.Text = row.Cells[0].Value?.ToString();
                txtTenNhaCungCap.Text = row.Cells[1].Value?.ToString();
                txtDiaChiNhaCungCap.Text = row.Cells[2].Value?.ToString();
                // Gán giá trị cho các trường khác nếu có
                // txtSoDienThoai.Text = row.Cells[3].Value?.ToString();
                // txtEmail.Text = row.Cells[4].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(txtMaNhaCungCap.Text) || string.IsNullOrWhiteSpace(txtTenNhaCungCap.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên Nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNCC = txtMaNhaCungCap.Text.Trim();

            // Kiểm tra trùng mã
            if (db.NhaCungCaps.Any(ncc => ncc.MaNhaCungCap == maNCC))
            {
                MessageBox.Show("Mã Nhà cung cấp đã tồn tại. Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NhaCungCap newNhaCungCap = new NhaCungCap
            {
                MaNhaCungCap = maNCC,
                TenNhaCungCap = txtTenNhaCungCap.Text.Trim(),
                DiaChi = txtDiaChiNhaCungCap.Text.Trim()
            };

            try
            {
                db.NhaCungCaps.Add(newNhaCungCap);
                db.SaveChanges();
                MessageBox.Show("Thêm Nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhaCungCap(); 
                ClearInputFields(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm Nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một Nhà cung cấp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maNCC = dgvNhaCungCap.SelectedRows[0].Cells[0].Value?.ToString();

            if (string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Không tìm thấy Mã Nhà cung cấp để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa Nhà cung cấp có mã '{maNCC}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    NhaCungCap nccToDelete = db.NhaCungCaps.Find(maNCC);
                    if (nccToDelete != null)
                    {
                        db.NhaCungCaps.Remove(nccToDelete);
                        db.SaveChanges();
                        MessageBox.Show("Xóa Nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhaCungCap(); 
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Nhà cung cấp để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa Nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNhaCungCap.Text) || string.IsNullOrWhiteSpace(txtTenNhaCungCap.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên Nhà cung cấp để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNCC = txtMaNhaCungCap.Text.Trim();

            try
            {
                NhaCungCap nccToUpdate = db.NhaCungCaps.Find(maNCC);

                if (nccToUpdate != null)
                {
                    nccToUpdate.TenNhaCungCap = txtTenNhaCungCap.Text.Trim();
                    nccToUpdate.DiaChi = txtDiaChiNhaCungCap.Text.Trim();


                    db.SaveChanges();
                    MessageBox.Show("Cập nhật Nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhaCungCap();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Nhà cung cấp có mã này để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa Nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();

            var filteredList = db.NhaCungCaps
                                 .Where(ncc => ncc.MaNhaCungCap.ToLower().Contains(keyword) ||
                                               ncc.TenNhaCungCap.ToLower().Contains(keyword))
                                 .ToList();

            BindGrid(filteredList);
        }
    }
}