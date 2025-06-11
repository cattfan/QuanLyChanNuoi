using QuanLyChanNuoi.Models;

using QuanLyChanNuoi.User_case_Admin;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace QuanLyChanNuoi

{

    public partial class FormNhaphoadon : Form

    {

        private LiveStockContextDB db = new LiveStockContextDB();
        private List<ChiTietHoaDon> danhSachChiTietHienTai;
        private ChiTietHoaDon selectedChiTiet;
        private static Random _random = new Random();

        public FormNhaphoadon()
        {
            InitializeComponent();
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            danhSachChiTietHienTai = new List<ChiTietHoaDon>();
            LoadComboBoxData();
            BindGrid(danhSachChiTietHienTai);
        }

        private void LoadHoadon()

        {
            danhSachChiTietHienTai = new List<ChiTietHoaDon>();

            LoadComboBoxData();
            // Hiển thị bảng trống ban đầu
            BindGrid(danhSachChiTietHienTai);
        }

        private void LoadComboBoxData()

        {

            try

            {

                // Lấy danh sách từ DB

                var danhsachVatTu = db.VatTu.ToList();

                var danhsachNCC = db.NhaCungCap.ToList();



                // Đổ dữ liệu vào ComboBox Vật tư

                cbbtenvattu.DataSource = danhsachVatTu;

                cbbtenvattu.DisplayMember = "TenVatTu";

                cbbtenvattu.ValueMember = "MaVatTu";



                // Đổ dữ liệu vào ComboBox Nhà cung cấp

                cbbManhacungcap.DataSource = danhsachNCC;

                cbbManhacungcap.DisplayMember = "TenNhaCungCap";

                cbbManhacungcap.ValueMember = "MaNhaCungCap";



                // Xóa lựa chọn mặc định ban đầu

                cbbtenvattu.SelectedIndex = -1;

                cbbManhacungcap.SelectedIndex = -1;

            }

            catch (Exception ex)

            {

                MessageBox.Show("Lỗi khi tải dữ liệu vật tư/nhà cung cấp: " + ex.Message);

            }

        }



        private void BindGrid(List<ChiTietHoaDon> danhSach)

        {

            dvghoadon.Rows.Clear();

            if (danhSach == null) return;



            foreach (var item in danhSach)

            {

                int index = dvghoadon.Rows.Add();

                // Lấy tên trực tiếp từ thuộc tính tham chiếu

                dvghoadon.Rows[index].Cells[0].Value = item.VatTu.TenVatTu;

                dvghoadon.Rows[index].Cells[1].Value = item.NhaCungCap?.TenNhaCungCap;

                dvghoadon.Rows[index].Cells[2].Value = item.SoLuong;

                dvghoadon.Rows[index].Cells[3].Value = item.DonGia;

                dvghoadon.Rows[index].Cells[4].Value = (item.SoLuong ?? 0) * (item.DonGia ?? 0);

                dvghoadon.Rows[index].Tag = item;

            }

            UpdateTongTien();

        }

        // Cập nhật tổng thành tiề

        // Xóa trắng các ô nhập liệu

        private void dvghoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)

        {

            if (e.RowIndex >= 0 && e.RowIndex < dvghoadon.Rows.Count)

            {

                selectedChiTiet = dvghoadon.Rows[e.RowIndex].Tag as ChiTietHoaDon;

                if (selectedChiTiet != null)

                {

                    cbbtenvattu.SelectedValue = selectedChiTiet.MaMatHang;

                    cbbManhacungcap.SelectedValue = selectedChiTiet.MaNhaCungCap;

                    lbsolluong.Value = selectedChiTiet.SoLuong ?? 1;

                    txtdongia.Text = selectedChiTiet.DonGia?.ToString();

                }

            }

        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbbtenvattu.SelectedValue == null || cbbManhacungcap.SelectedValue == null || lbsolluong.Value <= 0 || string.IsNullOrWhiteSpace(txtdongia.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo");
                return;
            }
            if (!decimal.TryParse(txtdongia.Text, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Lỗi");
                return;
            }

            var newDetail = new ChiTietHoaDon
            {
                MaMatHang = cbbtenvattu.SelectedValue as string,
                MaNhaCungCap = cbbManhacungcap.SelectedValue as string,
                SoLuong = (int)lbsolluong.Value,
                DonGia = donGia,
                VatTu = cbbtenvattu.SelectedItem as VatTu,
                NhaCungCap = cbbManhacungcap.SelectedItem as NhaCungCap,
                LoaiMatHang = null, // Set giá trị null tường minh
                MaNhanVien = null   // Set giá trị null tường minh
            };

            danhSachChiTietHienTai.Add(newDetail);
            BindGrid(danhSachChiTietHienTai);
            ClearInputFields();
        }

        private void ClearInputFields()

        {

            // Bỏ chọn trong các ComboBox

            cbbtenvattu.SelectedIndex = -1;

            cbbManhacungcap.SelectedIndex = -1;



            // Xóa nội dung TextBox và reset NumericUpDown

            txtdongia.Clear();

            lbsolluong.Value = 0;



            // Quan trọng: Reset đối tượng đang chọn để tránh sửa/xóa nhầm

            selectedChiTiet = null;



            // Di chuyển con trỏ về ô nhập liệu đầu tiên để tiện cho việc nhập mới

            cbbtenvattu.Focus();

        }



        private void btnSua_Click(object sender, EventArgs e)

        {

            if (selectedChiTiet == null)

            {

                MessageBox.Show("Vui lòng chọn một mục để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }

            if (!decimal.TryParse(txtdongia.Text, out decimal donGia) || donGia < 0)

            {

                MessageBox.Show("Đơn giá không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }



            selectedChiTiet.LoaiMatHang = cbbtenvattu.SelectedValue as string;

            selectedChiTiet.VatTu = cbbtenvattu.SelectedItem as VatTu;



            selectedChiTiet.MaNhaCungCap = cbbManhacungcap.SelectedValue as string;

            selectedChiTiet.NhaCungCap = cbbManhacungcap.SelectedItem as NhaCungCap;



            selectedChiTiet.SoLuong = (int)lbsolluong.Value;

            selectedChiTiet.DonGia = decimal.Parse(txtdongia.Text);



            BindGrid(danhSachChiTietHienTai);

            ClearInputFields();

        }



        private void btnXoa_Click(object sender, EventArgs e)

        {

            if (selectedChiTiet == null)

            {

                MessageBox.Show("Vui lòng chọn một mục để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {

                danhSachChiTietHienTai.Remove(selectedChiTiet);

                BindGrid(danhSachChiTietHienTai);

                ClearInputFields();

            }

        }



        private void btnThoat_Click(object sender, EventArgs e)

        {

            Close();

        }

        private void UpdateTongTien()

        {

            decimal tongTien = danhSachChiTietHienTai.Sum(item => (item.SoLuong ?? 0) * (item.DonGia ?? 0));

            txttongtien.Text = tongTien.ToString("N0");

        }



        private void button1_Click(object sender, EventArgs e)

        {

            this.Hide(); // Ẩn form admin trước khi mở form vật nuôi

            FormXemhoadon formxemhoadon = new FormXemhoadon();

            formxemhoadon.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form nhà cung cấp đóng

            formxemhoadon.Show();

        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!danhSachChiTietHienTai.Any())
            {
                MessageBox.Show("Hóa đơn trống, không có gì để lưu.", "Thông báo");
                return;
            }

            try
            {
                // 1. Tự động tạo một Mã Hóa Đơn mới và duy nhất
                string maHDMoi = TaoMaHoaDonTuDong();

                // 2. Tạo đối tượng Hóa Đơn (master) và KHỞI TẠO danh sách chi tiết của nó
                var hoaDonMoi = new HoaDon
                {
                    MaHoaDon = maHDMoi,
                    NgayLap = DateTime.Now,
                    ChiTietHoaDon = new List<ChiTietHoaDon>() // Quan trọng: Phải khởi tạo collection
                };

                // 3. Lặp qua danh sách tạm và gán từng chi tiết VÀO BÊN TRONG hóa đơn master
                int sttCounter = 1;
                foreach (var chiTietTam in danhSachChiTietHienTai)
                {
                    // Gán MaHoaDon và STT cho từng chi tiết
                    chiTietTam.MaHoaDon = maHDMoi;
                    chiTietTam.STT = sttCounter++;

                    // THAY ĐỔI QUAN TRỌNG:
                    // Thêm chi tiết này vào danh sách của đối tượng hoaDonMoi
                    hoaDonMoi.ChiTietHoaDon.Add(chiTietTam);
                }

                // 4. CHỈ CẦN THÊM ĐỐI TƯỢNG MASTER (Hóa đơn) VÀO DBCONTEXT
                //    Entity Framework sẽ tự động nhận biết và thêm các chi tiết con đi kèm.
                db.HoaDon.Add(hoaDonMoi);

                // 5. Lưu tất cả thay đổi vào DB trong một giao dịch duy nhất
                //    EF sẽ tự động lưu Hóa đơn cha trước, sau đó mới đến các chi tiết con.
                db.SaveChanges();

                MessageBox.Show($"Lưu hóa đơn thành công! Mã: {maHDMoi}", "Thành công");

                danhSachChiTietHienTai.Clear();
                BindGrid(danhSachChiTietHienTai);
            }
            catch (Exception ex)
            {
                // Nếu vẫn còn lỗi, hãy đặt breakpoint ở đây để kiểm tra lại
                MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.InnerException?.Message ?? ex.Message, "Lỗi Database");
            }
        }

        private string TaoMaHoaDonTuDong()
        {
            string uniquePart = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            return $"HD{uniquePart}";
        }


    }

}
