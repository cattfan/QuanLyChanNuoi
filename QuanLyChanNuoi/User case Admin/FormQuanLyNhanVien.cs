using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity; 
namespace QuanLyChanNuoi
{
    public partial class FormQuanLyNhanVien : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();

        public FormQuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void FormQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadComboBoxes();
            ClearTextBoxes();
        }

        private void LoadNhanVien()
        {
            try
            {
                List<NhanVien> danhSach = db.NhanViens
                                            .Include(nv => nv.ToNhanVien)
                                            .Include(nv => nv.ChucVuNhanVien)
                                            .ToList();
                BindGrid(danhSach);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadComboBoxes()
        {
            cmbGioiTinh.DataSource = new List<string> { "Nam", "Nữ" };
            try
            {
                var danhSachTo = db.ToNhanViens.ToList();
                cmbMaTo.DataSource = danhSachTo;
                cmbMaTo.DisplayMember = "MaTo"; 
                cmbMaTo.ValueMember = "MaTo"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Tổ nhân viên: " + ex.Message);
            }
            try
            {
                var danhSachChucVu = db.ChucVuNhanViens.ToList();
                cmbChucvu.DataSource = danhSachChucVu;
                cmbChucvu.DisplayMember = "MaChucVu"; 
                cmbChucvu.ValueMember = "MaChucVu";   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Chức vụ: " + ex.Message);
            }
        }


        private void BindGrid(List<NhanVien> nhanViens)
        {
            dvgNhanVien.Rows.Clear();
            foreach (var nv in nhanViens)
            {
                int index = dvgNhanVien.Rows.Add();
                dvgNhanVien.Rows[index].Cells[0].Value = nv.MaNhanVien;
                dvgNhanVien.Rows[index].Cells[1].Value = nv.HoTen;
                dvgNhanVien.Rows[index].Cells[2].Value = nv.NgaySinh?.ToString("dd/MM/yyyy") ?? "";
                dvgNhanVien.Rows[index].Cells[3].Value = nv.GioiTinh;
                dvgNhanVien.Rows[index].Cells[4].Value = nv.ToNhanVien?.TenTo ?? nv.MaTo;
                dvgNhanVien.Rows[index].Cells[5].Value = nv.MaChucVu;
            }
        }

        private void dvgNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells[0].Value?.ToString();
                txtHoTen.Text = row.Cells[1].Value?.ToString();
                dtpNgaySinh.Value = DateTime.TryParse(row.Cells[2].Value?.ToString(), out var date) ? date : DateTime.Now;
                cmbGioiTinh.Text = row.Cells[3].Value?.ToString();
                string maToFromGrid = row.Cells[4].Value?.ToString();
                if (cmbMaTo.DataSource is IList<QuanLyChanNuoi.Models.ToNhanVien> listTo)
                {
                    var selectedTo = listTo.FirstOrDefault(t => t.TenTo == maToFromGrid);
                    if (selectedTo != null)
                    {
                        cmbMaTo.SelectedValue = selectedTo.MaTo;
                    }
                    else
                    {
                        cmbMaTo.SelectedValue = maToFromGrid;
                    }
                }
                else
                {
                    cmbMaTo.SelectedValue = maToFromGrid;
                }
                string maChucVuFromGrid = row.Cells[5].Value?.ToString();
                if (!string.IsNullOrEmpty(maChucVuFromGrid))
                {
                    cmbChucvu.SelectedValue = maChucVuFromGrid;
                }
                else
                {
                    cmbChucvu.SelectedIndex = -1;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            string maNV = txtMaNV.Text.Trim();
            var existing = db.NhanViens.Find(maNV);
            if (existing != null)
            {
                MessageBox.Show("Mã nhân viên đã tồn tại.");
                return;
            }

            NhanVien nv = new NhanVien
            {
                MaNhanVien = maNV,
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cmbGioiTinh.Text.Trim(), 
                MaTo = cmbMaTo.SelectedValue?.ToString(),
                MaChucVu = cmbChucvu.SelectedValue?.ToString()
            };

            try
            {
                db.NhanViens.Add(nv);
                db.SaveChanges();
                MessageBox.Show("Thêm nhân viên thành công.");
                LoadNhanVien();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            var nv = db.NhanViens.Find(maNV);
            if (nv == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên để sửa.");
                return;
            }

            nv.HoTen = txtHoTen.Text.Trim();
            nv.NgaySinh = dtpNgaySinh.Value;
            nv.GioiTinh = cmbGioiTinh.Text.Trim(); 
            nv.MaTo = cmbMaTo.SelectedValue?.ToString();
            nv.MaChucVu = cmbChucvu.SelectedValue?.ToString();

            try
            {
                db.SaveChanges();
                MessageBox.Show("Cập nhật thành công.");
                LoadNhanVien();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dvgNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.No) return;

            string maNV = dvgNhanVien.SelectedRows[0].Cells[0].Value.ToString();
            var nv = db.NhanViens.Find(maNV);

            if (nv != null)
            {
                db.NhanViens.Remove(nv);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công.");
                    LoadNhanVien();
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
            txtMaNV.Clear();
            txtHoTen.Clear(); 
            if (cmbGioiTinh.Items.Count > 0) cmbGioiTinh.SelectedIndex = 0;
            else cmbGioiTinh.SelectedIndex = -1;

            if (cmbMaTo.Items.Count > 0) cmbMaTo.SelectedIndex = 0;
            else cmbMaTo.SelectedIndex = -1;

            if (cmbChucvu.Items.Count > 0) cmbChucvu.SelectedIndex = 0;
            else cmbChucvu.SelectedIndex = -1;

            dtpNgaySinh.Value = DateTime.Now;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();

            var filteredList = db.NhanViens
                                 .Where(nv => nv.MaNhanVien.ToLower().Contains(keyword)
                                            || nv.HoTen.ToLower().Contains(keyword))
                                 .ToList();

            BindGrid(filteredList);
        }
    }
}