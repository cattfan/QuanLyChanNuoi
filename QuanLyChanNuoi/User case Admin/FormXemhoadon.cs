using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

namespace QuanLyChanNuoi.User_case_Admin
{
    public partial class FormXemhoadon : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();
        private List<ChiTietHoaDon> allInvoiceDetails;
        public FormXemhoadon()
        {
            InitializeComponent();
        }

        private void FormXemhoadon_Load(object sender, EventArgs e)
        {
            LoadAllData();
            // Gắn sự kiện TextChanged cho ô tìm kiếm
            txttimkiem.TextChanged += TxtTimKiem_TextChanged;
        }
        private void LoadAllData()
        {
            try
            {
                // Dùng Include để lấy luôn dữ liệu của VatTu và NhaCungCap liên quan
                // Giúp tăng hiệu năng, tránh truy vấn N+1
                this.allInvoiceDetails = db.ChiTietHoaDon
                                           .Include(ct => ct.VatTu)
                                           .Include(ct => ct.NhaCungCap)
                                           .ToList();

                // Hiển thị toàn bộ dữ liệu lên bảng
                BindGrid(this.allInvoiceDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không thể tải dữ liệu hóa đơn: " + ex.InnerException?.Message ?? ex.Message);
            }

        }
        private void BindGrid(List<ChiTietHoaDon> dataToDisplay)
        {
            dvgbangxemtt.Rows.Clear();

            foreach (var item in dataToDisplay)
            {
                int index = dvgbangxemtt.Rows.Add();
                // Lấy tên trực tiếp từ thuộc tính tham chiếu
                dvgbangxemtt.Rows[index].Cells[0].Value = item.VatTu.TenVatTu;
                dvgbangxemtt.Rows[index].Cells[1].Value = item.NhaCungCap.TenNhaCungCap;
                dvgbangxemtt.Rows[index].Cells[2].Value = item.SoLuong;
                dvgbangxemtt.Rows[index].Cells[3].Value = item.DonGia;
                dvgbangxemtt.Rows[index].Cells[4].Value = (item.SoLuong ?? 0) * (item.DonGia ?? 0);
                dvgbangxemtt.Rows[index].Tag = item;
            }
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm, chuyển về chữ thường và xóa khoảng trắng thừa
            string searchTerm = txttimkiem.Text.ToLower().Trim();

            // Nếu ô tìm kiếm trống, hiển thị lại toàn bộ danh sách gốc
            if (string.IsNullOrEmpty(searchTerm))
            {
                BindGrid(this.allInvoiceDetails);
                return;
            }

            // Dùng LINQ để lọc danh sách gốc theo từ khóa
            // Tìm kiếm dựa trên Tên vật tư hoặc Tên nhà cung cấp
            var filteredList = this.allInvoiceDetails
                .Where(item =>
                    (item.VatTu.TenVatTu?.ToLower().Contains(searchTerm) ?? false) ||
                    (item.NhaCungCap.TenNhaCungCap?.ToLower().Contains(searchTerm) ?? false)
                ).ToList();

            // Hiển thị danh sách đã được lọc lên bảng
            BindGrid(filteredList);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
