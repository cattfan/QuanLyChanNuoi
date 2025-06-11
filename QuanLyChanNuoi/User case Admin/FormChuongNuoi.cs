using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi
{
    public partial class FormChuongNuoi : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();

        public FormChuongNuoi()
        {
            InitializeComponent();
        }

        private void FormChuongNuoi_Load(object sender, EventArgs e)
        {
            LoadChuong();
        }

        private void LoadChuong()
        {
            try
            {
                List<ChuongVatNuoi> danhSachChuong = db.ChuongVatNuois.ToList();
                BindGrid(danhSachChuong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void BindGrid(List<ChuongVatNuoi> chuongVatNuois)
        {
            dvgChuongnuoi.Rows.Clear();
            foreach (var item in chuongVatNuois)
            {
                int index = dvgChuongnuoi.Rows.Add();
                dvgChuongnuoi.Rows[index].Cells[0].Value = item.MaChuong;
                dvgChuongnuoi.Rows[index].Cells[1].Value = item.ViTri;
                dvgChuongnuoi.Rows[index].Cells[2].Value = item.DienTich.HasValue
                    ? item.DienTich.Value.ToString("F2")
                    : "0.00";
            }
        }

        private void dvgChuongnuoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgChuongnuoi.Rows[e.RowIndex];

                txtMachuong.Text = row.Cells[0].Value?.ToString();
                txtVitri.Text = row.Cells[1].Value?.ToString();
                txtDientich.Text = row.Cells[2].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(txtMachuong.Text) ||
                string.IsNullOrWhiteSpace(txtVitri.Text) ||
                string.IsNullOrWhiteSpace(txtDientich.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Kiểm tra Mã chuồng đã tồn tại chưa
            string maChuong = txtMachuong.Text.Trim();
            var existing = db.ChuongVatNuois.Find(maChuong);
            if (existing != null)
            {
                MessageBox.Show("Mã chuồng đã tồn tại. Vui lòng nhập mã khác.");
                return;
            }

            // Parse diện tích
            if (!double.TryParse(txtDientich.Text.Trim(), out double dientich))
            {
                MessageBox.Show("Diện tích không hợp lệ.");
                return;
            }

            ChuongVatNuoi chuong = new ChuongVatNuoi
            {
                MaChuong = maChuong,
                ViTri = txtVitri.Text.Trim(),
                DienTich = (decimal?)dientich 
            };

            try
            {
                db.ChuongVatNuois.Add(chuong);
                db.SaveChanges();
                MessageBox.Show("Thêm chuồng mới thành công.");
                LoadChuong();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMachuong.Text))
            {
                MessageBox.Show("Vui lòng chọn chuồng để sửa.");
                return;
            }

            string maChuong = txtMachuong.Text.Trim();
            var chuong = db.ChuongVatNuois.Find(maChuong);
            if (chuong == null)
            {
                MessageBox.Show("Không tìm thấy chuồng để sửa.");
                return;
            }

            // Parse diện tích
            if (!double.TryParse(txtDientich.Text.Trim(), out double dientich))
            {
                MessageBox.Show("Diện tích không hợp lệ.");
                return;
            }

            chuong.ViTri = txtVitri.Text.Trim();
            chuong.DienTich = (decimal?)dientich;

            try
            {
                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thành công.");
                LoadChuong();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dvgChuongnuoi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn chuồng cần xóa.");
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc muốn xóa chuồng này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.No) return;

            string maChuong = dvgChuongnuoi.SelectedRows[0].Cells[0].Value.ToString();
            var chuong = db.ChuongVatNuois.Find(maChuong);

            if (chuong != null)
            {
                db.ChuongVatNuois.Remove(chuong);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Xóa chuồng thành công.");
                    LoadChuong();
                    ClearTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa chuồng: " + ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Hàm xóa nội dung các textbox
        private void ClearTextBoxes()
        {
            txtMachuong.Clear();
            txtVitri.Clear();
            txtDientich.Clear();
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimkiem.Text.Trim();

            try
            {
                List<ChuongVatNuoi> searchResults;

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    // If search box is empty, load all
                    searchResults = db.ChuongVatNuois.ToList();
                }
                else
                {
                    // Search by ViTri (Khu)
                    searchResults = db.ChuongVatNuois
                                      .Where(c => c.ViTri.Contains(keyword))
                                      .ToList();
                }
                BindGrid(searchResults);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
    }
}
