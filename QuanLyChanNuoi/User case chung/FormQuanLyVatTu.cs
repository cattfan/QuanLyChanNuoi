using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi
{
    public partial class FormVatTu : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();

        public FormVatTu()
        {
            InitializeComponent();
        }

        private void FormVatTu_Load(object sender, EventArgs e)
        {
            LoadVatTu();
        }

        private void LoadVatTu()
        {
            try
            {
                List<VatTu> danhSachVatTu = db.VatTus.ToList();
                BindGrid(danhSachVatTu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void BindGrid(List<VatTu> vatTus)
        {
            dvgVattu.Rows.Clear();
            foreach (var vt in vatTus)
            {
                int index = dvgVattu.Rows.Add();
                dvgVattu.Rows[index].Cells[0].Value = vt.MaVatTu;
                dvgVattu.Rows[index].Cells[1].Value = vt.TenVatTu;
                dvgVattu.Rows[index].Cells[2].Value = vt.DonViTinh;
                dvgVattu.Rows[index].Cells[3].Value = vt.SoLuong.HasValue ? vt.SoLuong.ToString() : "0";
                dvgVattu.Rows[index].Cells[4].Value = vt.TrangThai;
            }
        }

        private void dvgVattu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgVattu.Rows[e.RowIndex];
                txtMaVatTu.Text = row.Cells[0].Value?.ToString();
                txtTenVatTu.Text = row.Cells[1].Value?.ToString();
                ccbDonViTinh.Text = row.Cells[2].Value?.ToString();
                txtSoLuong.Text = row.Cells[3].Value?.ToString();
                txtTrangThai.Text = row.Cells[4].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaVatTu.Text) ||
                string.IsNullOrWhiteSpace(txtTenVatTu.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin vật tư.");
                return;
            }

            string maVT = txtMaVatTu.Text.Trim();
            if (db.VatTus.Find(maVT) != null)
            {
                MessageBox.Show("Mã vật tư đã tồn tại.");
                return;
            }

            int.TryParse(txtSoLuong.Text.Trim(), out int soLuong);

            VatTu vt = new VatTu
            {
                MaVatTu = maVT,
                TenVatTu = txtTenVatTu.Text.Trim(),
                DonViTinh = ccbDonViTinh.Text.Trim(),
                SoLuong = soLuong,
                TrangThai = txtTrangThai.Text.Trim()
            };

            try
            {
                db.VatTus.Add(vt);
                db.SaveChanges();
                MessageBox.Show("Thêm vật tư thành công.");
                LoadVatTu();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            var ketQua = db.VatTus
                           .Where(vt => vt.MaVatTu.ToLower().Contains(tuKhoa) ||
                                        vt.TenVatTu.ToLower().Contains(tuKhoa) ||
                                        (vt.DonViTinh != null && vt.DonViTinh.ToLower().Contains(tuKhoa)) ||
                                        (vt.TrangThai != null && vt.TrangThai.ToLower().Contains(tuKhoa)))
                           .ToList();
            BindGrid(ketQua);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maVT = txtMaVatTu.Text.Trim();
            var vt = db.VatTus.Find(maVT);
            if (vt == null)
            {
                MessageBox.Show("Không tìm thấy vật tư để sửa.");
                return;
            }

            int.TryParse(txtSoLuong.Text.Trim(), out int soLuong);

            vt.TenVatTu = txtTenVatTu.Text.Trim();
            vt.DonViTinh = ccbDonViTinh.Text.Trim();
            vt.SoLuong = soLuong;
            vt.TrangThai = txtTrangThai.Text.Trim();

            try
            {
                db.SaveChanges();
                MessageBox.Show("Cập nhật vật tư thành công.");
                LoadVatTu();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dvgVattu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vật tư để xóa.");
                return;
            }

            string maVT = dvgVattu.SelectedRows[0].Cells[0].Value.ToString();
            var vt = db.VatTus.Find(maVT);

            if (vt != null)
            {
                var confirm = MessageBox.Show("Bạn có chắc muốn xóa vật tư này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) return;

                db.VatTus.Remove(vt);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Xóa vật tư thành công.");
                    LoadVatTu();
                    ClearTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearTextBoxes()
        {
            txtMaVatTu.Clear();
            txtTenVatTu.Clear();
            ccbDonViTinh.SelectedIndex = -1;
            txtSoLuong.Clear();
            txtTrangThai.Clear();
        }
    }
}
