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

namespace QuanLyChanNuoi
{
    public partial class FormQuanLyVatTu : Form
    {
        public FormQuanLyVatTu()
        {
            InitializeComponent();
        }

        private void FormQuanLyVatTu_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataIntoDataGridView();
                LoadDonViTinhComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataIntoDataGridView()
        {
            using (LiveStockContextDB context = new LiveStockContextDB())
            {
                List<VatTu> listVattu = context.VatTus.ToList();
                BindGrid(listVattu);
            }
        }

        private void BindGrid(List<VatTu> listVattu)
        {
            dvgVattu.Rows.Clear();
            foreach (var item in listVattu)
            {
                int index = dvgVattu.Rows.Add();
                dvgVattu.Rows[index].Cells[0].Value = item.MaVatTu;
                dvgVattu.Rows[index].Cells[1].Value = item.TenVatTu;
                dvgVattu.Rows[index].Cells[2].Value = item.DonViTinh;
                dvgVattu.Rows[index].Cells[3].Value = item.SoLuong;
                dvgVattu.Rows[index].Cells[4].Value = item.SoLuong > 0 ? "Còn hàng" : "Hết hàng";
            }
        }

        private void LoadDonViTinhComboBox()
        {
            using (LiveStockContextDB context = new LiveStockContextDB())
            {
                var donViTinhs = context.VatTus.Select(vt => vt.DonViTinh).Distinct().ToList();
                ccbDonViTinh.DataSource = donViTinhs;
            }
        }

        private void ClearInputFields()
        {
            txtMaVatTu.Text = "";
            txtTenVatTu.Text = "";
            ccbDonViTinh.SelectedIndex = -1;
            txtSoLuong.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (LiveStockContextDB context = new LiveStockContextDB())
                {
                    if (string.IsNullOrWhiteSpace(txtMaVatTu.Text))
                    {
                        MessageBox.Show("Mã vật tư không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (context.VatTus.Any(vt => vt.MaVatTu == txtMaVatTu.Text))
                    {
                        MessageBox.Show("Mã vật tư này đã tồn tại. Vui lòng nhập mã khác.", "Trùng mã vật tư", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    VatTu newVatTu = new VatTu();
                    newVatTu.MaVatTu = txtMaVatTu.Text;
                    newVatTu.TenVatTu = txtTenVatTu.Text;
                    newVatTu.DonViTinh = ccbDonViTinh.SelectedItem?.ToString(); 
                    if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong))
                    {
                        MessageBox.Show("Số lượng phải là một số nguyên hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    newVatTu.SoLuong = soLuong;
                    newVatTu.TrangThai = newVatTu.SoLuong > 0 ? "Còn hàng" : "Hết hàng"; 

                    context.VatTus.Add(newVatTu);
                    context.SaveChanges();
                    MessageBox.Show("Thêm vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataIntoDataGridView();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvgVattu.SelectedRows.Count > 0)
                {
                    string maVatTuToUpdate = dvgVattu.SelectedRows[0].Cells[0].Value.ToString();

                    using (LiveStockContextDB context = new LiveStockContextDB())
                    {
                        VatTu vatTuToUpdate = context.VatTus.FirstOrDefault(vt => vt.MaVatTu == maVatTuToUpdate);
                        if (vatTuToUpdate != null)
                        {
                            vatTuToUpdate.TenVatTu = txtTenVatTu.Text;
                            vatTuToUpdate.DonViTinh = ccbDonViTinh.SelectedItem?.ToString();
                            if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong))
                            {
                                MessageBox.Show("Số lượng phải là một số nguyên hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            vatTuToUpdate.SoLuong = soLuong;
                            vatTuToUpdate.TrangThai = vatTuToUpdate.SoLuong > 0 ? "Còn hàng" : "Hết hàng";

                            context.SaveChanges();
                            MessageBox.Show("Cập nhật vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataIntoDataGridView();
                            ClearInputFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy vật tư để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một vật tư để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvgVattu.SelectedRows.Count > 0)
                {
                    string maVatTuToDelete = dvgVattu.SelectedRows[0].Cells[0].Value.ToString();

                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa vật tư có mã '{maVatTuToDelete}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (LiveStockContextDB context = new LiveStockContextDB())
                        {
                            VatTu vatTuToDelete = context.VatTus.FirstOrDefault(vt => vt.MaVatTu == maVatTuToDelete);
                            if (vatTuToDelete != null)
                            {
                                context.VatTus.Remove(vatTuToDelete);
                                context.SaveChanges();
                                MessageBox.Show("Xóa vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataIntoDataGridView();
                                ClearInputFields();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy vật tư để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một vật tư để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dvgVattu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgVattu.Rows[e.RowIndex];
                txtMaVatTu.Text = row.Cells[0].Value?.ToString();
                txtTenVatTu.Text = row.Cells[1].Value?.ToString();
                ccbDonViTinh.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuong.Text = row.Cells[3].Value?.ToString();
            }
        }

        private void txtTimKiemVatTu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (LiveStockContextDB context = new LiveStockContextDB())
                {
                    string searchText = txtTimKiemVatTu.Text.Trim();
                    List<VatTu> searchResults = context.VatTus
                        .Where(vt => vt.MaVatTu.Contains(searchText) || vt.TenVatTu.Contains(searchText))
                        .ToList();
                    BindGrid(searchResults);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}