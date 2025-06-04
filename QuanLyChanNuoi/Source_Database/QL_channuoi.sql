-- Tạo cơ sở dữ liệu
CREATE DATABASE QuanLychannuoi;
GO

-- Sử dụng cơ sở dữ liệu vừa tạo
USE QuanLychannuoi;
GO

-- ============================
-- Bảng Tổ Nhân Viên
-- ============================
CREATE TABLE ToNhanVien (
    MaTo VARCHAR(10) PRIMARY KEY,
    TenTo NVARCHAR(50) NOT NULL UNIQUE,
    TenDN NVARCHAR(30) NOT NULL,
    MatKhau VARCHAR(30)
);
GO

-- ============================
-- Bảng Chức Vụ Nhân Viên
-- ============================
CREATE TABLE ChucVuNhanVien (
    MaChucVu VARCHAR(10) PRIMARY KEY,
    TenChucVu NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- ============================
-- Bảng Nhân Viên
-- ============================
CREATE TABLE NhanVien (
    MaNhanVien VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    MaTo VARCHAR(10),
    MaChucVu VARCHAR(10),
    FOREIGN KEY (MaTo) REFERENCES ToNhanVien(MaTo),
    FOREIGN KEY (MaChucVu) REFERENCES ChucVuNhanVien(MaChucVu)
);
GO

-- ============================
-- Bảng Chuồng Vật Nuôi
-- ============================
CREATE TABLE ChuongVatNuoi (
    MaChuong VARCHAR(10) PRIMARY KEY,
    ViTri NVARCHAR(100),
    DienTich DECIMAL(10,2)
);
GO

-- ============================
-- Bảng Vật Nuôi
-- ============================
CREATE TABLE VatNuoi (
    MaVatNuoi VARCHAR(10) PRIMARY KEY,
    TenVatNuoi NVARCHAR(100),
    NgayNhap DATE,
    SoLuong INT,
    MaChuong VARCHAR(10) NULL,
    FOREIGN KEY (MaChuong) REFERENCES ChuongVatNuoi(MaChuong)
);
GO

-- ============================
-- Bảng Liên Kết Nhân Viên - Vật Nuôi
-- ============================
CREATE TABLE NhanVien_VatNuoi (
    MaNhanVien VARCHAR(10),
    MaVatNuoi VARCHAR(10),
    PRIMARY KEY (MaNhanVien, MaVatNuoi),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaVatNuoi) REFERENCES VatNuoi(MaVatNuoi)
);
GO

-- ============================
-- Bảng Vật Tư
-- ============================
CREATE TABLE VatTu (
    MaVatTu VARCHAR(10) PRIMARY KEY,
    TenVatTu NVARCHAR(100),
    DonViTinh NVARCHAR(30),
    SoLuong INT,
    TrangThai NVARCHAR(30)
);
GO

-- ============================
-- Bảng Nhà Cung Cấp
-- ============================
CREATE TABLE NhaCungCap (
    MaNhaCungCap VARCHAR(10) PRIMARY KEY,
    TenNhaCungCap NVARCHAR(100),
    DiaChi NVARCHAR(255)
);
GO

-- ============================
-- Bảng Liên Kết Nhà Cung Cấp - Vật Tư
-- ============================
CREATE TABLE NhaCungCap_VatTu (
    MaNhaCungCap VARCHAR(10),
    MaVatTu VARCHAR(10),
    DonGia DECIMAL(18,2),
    NgayCungCap DATE,
    PRIMARY KEY (MaNhaCungCap, MaVatTu),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap),
    FOREIGN KEY (MaVatTu) REFERENCES VatTu(MaVatTu)
);
GO

-- ============================
-- Bảng Hóa Đơn
-- ============================
CREATE TABLE HoaDon (
    MaHoaDon VARCHAR(10) PRIMARY KEY,
    NgayLap DATE
);
GO

-- ============================
-- Bảng Chi Tiết Hóa Đơn
-- ============================
CREATE TABLE ChiTietHoaDon (
    MaHoaDon VARCHAR(10),
    STT INT,
    LoaiMatHang NVARCHAR(50),
    MaMatHang VARCHAR(10),
    SoLuong INT,
    DonGia DECIMAL(18,2),
    MaNhanVien VARCHAR(10),
    MaNhaCungCap VARCHAR(10),
    PRIMARY KEY (MaHoaDon, STT),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap)
);
GO

-- ============================
-- Bảng Log Lịch Sử Chuồng
-- ============================
CREATE TABLE Log_LichSuChuong (
    MaLog VARCHAR(10) PRIMARY KEY,
    MaNhanVien VARCHAR(10),
    MaChuong VARCHAR(10),
    MaVatNuoi VARCHAR(10),
    LichSuSoLuongVatNuoiTrongChuong INT,
    log_thoigian DATETIME DEFAULT(GETDATE())
);
GO

-- ============================
-- Dữ liệu mẫu
-- ============================
INSERT INTO ToNhanVien (MaTo, TenTo, TenDN, MatKhau) VALUES
('TO01', N'Tổ chăm sóc 1', 'user1', '123'),
('TO02', N'Tổ kỹ thuật', 'user2', '456');
GO

INSERT INTO ChucVuNhanVien (MaChucVu, TenChucVu) VALUES
('CV01', N'Quản lý'),
('CV02', N'Nhân viên');
GO

INSERT INTO NhanVien (MaNhanVien, HoTen, NgaySinh, GioiTinh, MaTo, MaChucVu) VALUES
('NV03', N'Trần Văn C', '1985-03-15', N'Nam', 'TO01', 'CV02'),
('NV04', N'Nguyễn Thị D', '1992-07-20', N'Nữ', 'TO02', 'CV02'),
('NV05', N'Hoàng Văn E', '1988-11-05', N'Nam', 'TO01', 'CV02'),
('NV06', N'Phạm Thị F', '1996-01-30', N'Nữ', 'TO02', 'CV01'),
('NV07', N'Vũ Đình G', '1979-09-10', N'Nam', 'TO01', 'CV02'),
('NV08', N'Đặng Thị H', '1993-04-25', N'Nữ', 'TO02', 'CV02'),
('NV09', N'Bùi Văn I', '1982-08-12', N'Nam', 'TO01', 'CV02'),
('NV10', N'Hồ Thị K', '1998-12-01', N'Nữ', 'TO02', 'CV02'),
('NV11', N'Lý Văn L', '1991-06-18', N'Nam', 'TO01', 'CV01'),
('NV12', N'Trịnh Thị M', '1986-10-22', N'Nữ', 'TO02', 'CV02'),
('NV13', N'Dương Văn N', '1994-02-08', N'Nam', 'TO01', 'CV02'),
('NV14', N'Mai Thị O', '1980-07-14', N'Nữ', 'TO02', 'CV02'),
('NV15', N'Đỗ Văn P', '1997-05-03', N'Nam', 'TO01', 'CV02'),
('NV16', N'Ngô Thị Q', '1989-11-28', N'Nữ', 'TO02', 'CV01'),
('NV17', N'Châu Văn R', '1983-01-19', N'Nam', 'TO01', 'CV02'),
('NV18', N'Đinh Thị S', '1995-09-07', N'Nữ', 'TO02', 'CV02'),
('NV19', N'Kiều Văn T', '1987-04-02', N'Nam', 'TO01', 'CV02'),
('NV20', N'Lương Thị U', '1999-08-21', N'Nữ', 'TO02', 'CV02'),
('NV21', N'Phan Văn V', '1990-12-11', N'Nam', 'TO01', 'CV01'),
('NV22', N'Tạ Thị X', '1984-06-26', N'Nữ', 'TO02', 'CV02');
GO

INSERT INTO ChuongVatNuoi (MaChuong, ViTri, DienTich) VALUES
('C03', N'Khu A2', 25.0),
('C04', N'Khu B2', 18.5),
('C05', N'Khu C1', 30.0),
('C06', N'Khu A3', 22.0),
('C07', N'Khu B3', 20.0),
('C08', N'Khu C2', 28.0),
('C09', N'Khu D1', 15.0),
('C10', N'Khu A4', 26.5),
('C11', N'Khu B4', 19.0),
('C12', N'Khu C3', 32.5),
('C13', N'Khu D2', 17.0),
('C14', N'Khu A5', 23.5),
('C15', N'Khu B5', 21.5),
('C16', N'Khu C4', 29.5),
('C17', N'Khu D3', 16.0),
('C18', N'Khu A6', 27.0),
('C19', N'Khu B6', 20.5),
('C20', N'Khu C5', 31.0),
('C21', N'Khu D4', 18.0),
('C22', N'Khu A7', 24.0);
GO

-- Bảng Vật Nuôi (Thêm 20 loại/lô vật nuôi)
INSERT INTO VatNuoi (MaVatNuoi, TenVatNuoi, NgayNhap, SoLuong, MaChuong) VALUES
('VN03', N'Bò sữa Holstein', '2024-05-10', 5, 'C03'),
('VN04', N'Lợn Móng Cái', '2024-06-15', 20, 'C04'),
('VN05', N'Dê Bách Thảo', '2024-07-01', 15, 'C05'),
('VN06', N'Gà Đông Tảo', '2024-08-20', 50, 'C06'),
('VN07', N'Cá Trắm Cỏ', '2024-09-05', 200, 'C07'), -- Giả sử C07 là ao cá
('VN08', N'Thỏ New Zealand', '2024-10-12', 30, 'C08'),
('VN09', N'Ngan Pháp', '2024-11-25', 25, 'C09'),
('VN10', N'Bò Vàng Nghệ An', '2024-12-01', 8, 'C10'),
('VN11', N'Lợn Yorkshire', '2025-01-10', 22, 'C11'),
('VN12', N'Dê Saanen', '2025-02-18', 12, 'C12'),
('VN13', N'Gà Ri', '2025-03-03', 60, 'C13'),
('VN14', N'Cá Chép Vàng', '2025-04-09', 150, 'C14'), -- Giả sử C14 là ao cá
('VN15', N'Thỏ California', '2025-05-16', 35, 'C15'),
('VN16', N'Ngan Trắng', '2025-06-22', 28, 'C16'),
('VN17', N'Bò Lai Sind', '2025-07-30', 7, 'C17'),
('VN18', N'Lợn Duroc', '2025-08-05', 18, 'C18'),
('VN19', N'Dê Alpine', '2025-09-11', 10, 'C19'),
('VN20', N'Gà Tre', '2025-10-19', 40, 'C20'),
('VN21', N'Cá Rô Phi', '2025-11-23', 250, 'C21'), -- Giả sử C21 là ao cá
('VN22', N'Thỏ Xám', '2025-12-28', 20, 'C22');
GO

-- Bảng Liên Kết Nhân Viên - Vật Nuôi (Thêm 20 liên kết)
INSERT INTO NhanVien_VatNuoi (MaNhanVien, MaVatNuoi) VALUES
('NV03', 'VN03'), ('NV04', 'VN04'), ('NV05', 'VN05'), ('NV06', 'VN06'),
('NV07', 'VN07'), ('NV08', 'VN08'), ('NV09', 'VN09'), ('NV10', 'VN10'),
('NV11', 'VN11'), ('NV12', 'VN12'), ('NV13', 'VN13'), ('NV14', 'VN14'),
('NV15', 'VN15'), ('NV16', 'VN16'), ('NV17', 'VN17'), ('NV18', 'VN18'),
('NV19', 'VN19'), ('NV20', 'VN20'), ('NV21', 'VN21'), ('NV22', 'VN22');
GO

-- Bảng Vật Tư (Thêm 20 vật tư)
INSERT INTO VatTu (MaVatTu, TenVatTu, DonViTinh, SoLuong, TrangThai) VALUES
('VT03', N'Cám tổng hợp cho bò', N'Bao 25kg', 50, N'Còn'),
('VT04', N'Thuốc thú y Paracetamol', N'Vỉ 10 viên', 100, N'Còn'),
('VT05', N'Vắc xin LMLM bò', N'Lọ 10 liều', 20, N'Còn'),
('VT06', N'Cám cho lợn con', N'Bao 20kg', 60, N'Còn'),
('VT07', N'Men tiêu hóa cho gia cầm', N'Gói 100g', 150, N'Còn'),
('VT08', N'Thuốc sát trùng chuồng trại', N'Chai 1 lít', 30, N'Còn'),
('VT09', N'Cám cám viên nổi', N'Bao 10kg', 80, N'Còn'),
('VT10', N'Vitamin C tổng hợp', N'Gói 50g', 200, N'Còn'),
('VT11', N'Kháng sinh Amoxicillin', N'Hộp 100g', 40, N'Còn'),
('VT12', N'Cám cho dê', N'Bao 15kg', 70, N'Còn'),
('VT13', N'Vắc xin Newcastle cho gà', N'Lọ 100 liều', 25, N'Còn'),
('VT14', N'Chế phẩm sinh học xử lý đáy ao', N'Gói 1kg', 50, N'Còn'),
('VT15', N'Cám cho thỏ', N'Bao 5kg', 90, N'Còn'),
('VT16', N'Thuốc trị nội ngoại ký sinh trùng', N'Chai 50ml', 35, N'Còn'),
('VT17', N'Đá liếm khoáng cho bò dê', N'Cục 2kg', 60, N'Còn'),
('VT18', N'Cám cho ngan vịt', N'Bao 20kg', 55, N'Còn'),
('VT19', N'Vắc xin Gumboro cho gà', N'Lọ 100 liều', 22, N'Còn'),
('VT20', N'Tỏi bột (phụ gia thức ăn)', N'Túi 500g', 120, N'Còn'),
('VT21', N'Dụng cụ gieo tinh nhân tạo', N'Bộ', 10, N'Còn'),
('VT22', N'Bình sữa cho bê con', N'Cái', 15, N'Còn');
GO

-- Bảng Nhà Cung Cấp (Thêm 20 nhà cung cấp)
INSERT INTO NhaCungCap (MaNhaCungCap, TenNhaCungCap, DiaChi) VALUES
('NCC03', N'Công ty CP GreenFeed Việt Nam', N'KCN Biên Hòa 2, Đồng Nai'),
('NCC04', N'Tập đoàn De Heus', N'Bình Dương'),
('NCC05', N'Công ty TNHH Cargill Việt Nam', N'KCN Phú Mỹ, Bà Rịa - Vũng Tàu'),
('NCC06', N'Công ty CP Thuốc Thú Y TW (NAVETCO)', N'Hà Nội'),
('NCC07', N'Công ty TNHH Japfa Comfeed Việt Nam', N'Vĩnh Phúc'),
('NCC08', N'Công ty CP Chăn nuôi C.P. Việt Nam', N'KCN Biên Hòa, Đồng Nai'),
('NCC09', N'Công ty TNHH ANT', N'Hưng Yên'),
('NCC10', N'Công ty Bayer Việt Nam (Nhánh Thuốc Thú Y)', N'TP. Hồ Chí Minh'),
('NCC11', N'Công ty TNHH Vemedim', N'Cần Thơ'),
('NCC12', N'Công ty CP Proconco', N'KCN Biên Hòa 1, Đồng Nai'),
('NCC13', N'Công ty TNHH Anova Pharma', N'Long An'),
('NCC14', N'Công ty TNHH Uni-President Việt Nam', N'Bình Dương'),
('NCC15', N'Công ty TNHH Bio-Pharmachemie', N'TP. Hồ Chí Minh'),
('NCC16', N'Công ty CP Việt Pháp (Viphavet)', N'Hà Nội'),
('NCC17', N'Công ty TNHH Sunjin Vina', N'Đồng Nai'),
('NCC18', N'Công ty TNHH Khoa Kỹ Sinh Vật Thăng Long', N'Hải Dương'),
('NCC19', N'Công ty TNHH Emivest Feedmill Việt Nam', N'Bình Dương'),
('NCC20', N'Công ty TNHH Guyomarc''h Việt Nam', N'Đồng Nai'),
('NCC21', N'Công ty TNHH Á Châu Hoá Sinh', N'Long An'),
('NCC22', N'Công ty TNHH Dinh Dưỡng Ánh Hồng', N'Tiền Giang');
GO

-- Bảng Liên Kết Nhà Cung Cấp - Vật Tư (Thêm 20 liên kết)
INSERT INTO NhaCungCap_VatTu (MaNhaCungCap, MaVatTu, DonGia, NgayCungCap) VALUES
('NCC03', 'VT03', 350000, '2025-03-01'), ('NCC04', 'VT04', 25000, '2025-03-05'),
('NCC05', 'VT05', 1200000, '2025-03-10'), ('NCC06', 'VT06', 280000, '2025-03-15'),
('NCC07', 'VT07', 15000, '2025-03-20'), ('NCC08', 'VT08', 75000, '2025-03-25'),
('NCC09', 'VT09', 180000, '2025-04-01'), ('NCC10', 'VT10', 12000, '2025-04-05'),
('NCC11', 'VT11', 90000, '2025-04-10'), ('NCC12', 'VT12', 220000, '2025-04-15'),
('NCC13', 'VT13', 850000, '2025-04-20'), ('NCC14', 'VT14', 55000, '2025-04-25'),
('NCC15', 'VT15', 95000, '2025-05-01'), ('NCC16', 'VT16', 150000, '2025-05-05'),
('NCC17', 'VT17', 45000, '2025-05-10'), ('NCC18', 'VT18', 300000, '2025-05-15'),
('NCC19', 'VT19', 900000, '2025-05-20'), ('NCC20', 'VT20', 30000, '2025-05-25'),
('NCC21', 'VT21', 500000, '2025-06-01'), ('NCC22', 'VT22', 20000, '2025-06-05');
GO

-- Bảng Hóa Đơn (Thêm 20 hóa đơn)
INSERT INTO HoaDon (MaHoaDon, NgayLap) VALUES
('HD02', '2025-02-10'), ('HD03', '2025-02-15'), ('HD04', '2025-02-20'),
('HD05', '2025-02-25'), ('HD06', '2025-03-01'), ('HD07', '2025-03-05'),
('HD08', '2025-03-10'), ('HD09', '2025-03-15'), ('HD10', '2025-03-20'),
('HD11', '2025-03-25'), ('HD12', '2025-04-01'), ('HD13', '2025-04-05'),
('HD14', '2025-04-10'), ('HD15', '2025-04-15'), ('HD16', '2025-04-20'),
('HD17', '2025-04-25'), ('HD18', '2025-05-01'), ('HD19', '2025-05-05'),
('HD20', '2025-05-10'), ('HD21', '2025-05-15');
GO

-- Bảng Chi Tiết Hóa Đơn (Thêm 20 chi tiết)
INSERT INTO ChiTietHoaDon (MaHoaDon, STT, LoaiMatHang, MaMatHang, SoLuong, DonGia, MaNhanVien, MaNhaCungCap) VALUES
('HD01', 3, N'Vật tư', 'VT03', 5, 350000, 'NV03', 'NCC03'), -- Thêm vào HD01
('HD02', 1, N'Vật tư', 'VT04', 20, 25000, 'NV04', 'NCC04'),
('HD03', 1, N'Vật tư', 'VT05', 2, 1200000, 'NV05', 'NCC05'),
('HD04', 1, N'Vật tư', 'VT06', 10, 280000, 'NV06', 'NCC06'),
('HD05', 1, N'Vật tư', 'VT07', 50, 15000, 'NV07', 'NCC07'),
('HD06', 1, N'Vật tư', 'VT08', 3, 75000, 'NV08', 'NCC08'),
('HD07', 1, N'Vật tư', 'VT09', 15, 180000, 'NV09', 'NCC09'),
('HD08', 1, N'Vật tư', 'VT10', 30, 12000, 'NV10', 'NCC10'),
('HD09', 1, N'Vật tư', 'VT11', 5, 90000, 'NV11', 'NCC11'),
('HD10', 1, N'Vật tư', 'VT12', 8, 220000, 'NV12', 'NCC12'),
('HD11', 1, N'Vật tư', 'VT13', 1, 850000, 'NV13', 'NCC13'),
('HD12', 1, N'Vật tư', 'VT14', 12, 55000, 'NV14', 'NCC14'),
('HD13', 1, N'Vật tư', 'VT15', 25, 95000, 'NV15', 'NCC15'),
('HD14', 1, N'Vật tư', 'VT16', 4, 150000, 'NV16', 'NCC16'),
('HD15', 1, N'Vật tư', 'VT17', 30, 45000, 'NV17', 'NCC17'),
('HD16', 1, N'Vật tư', 'VT18', 7, 300000, 'NV18', 'NCC18'),
('HD17', 1, N'Vật tư', 'VT19', 1, 900000, 'NV19', 'NCC19'),
('HD18', 1, N'Vật tư', 'VT20', 40, 30000, 'NV20', 'NCC20'),
('HD19', 1, N'Vật tư', 'VT21', 2, 500000, 'NV21', 'NCC21'),
('HD20', 1, N'Vật tư', 'VT22', 10, 20000, 'NV22', 'NCC22');
GO

-- ============================
-- Stored Procedure
-- ============================
CREATE PROCEDURE sp_ThemLogSauXoaVatNuoi
    @MaNhanVien VARCHAR(10),
    @MaChuong VARCHAR(10),
    @MaVatNuoi VARCHAR(10),
    @SoLuong INT
AS
BEGIN
    DECLARE @MaLog VARCHAR(10)
    SET @MaLog = 'LOG' + FORMAT(GETDATE(), 'HHmmss')

    INSERT INTO Log_LichSuChuong (MaLog, MaNhanVien, MaChuong, MaVatNuoi, LichSuSoLuongVatNuoiTrongChuong)
    VALUES (@MaLog, @MaNhanVien, @MaChuong, @MaVatNuoi, @SoLuong)
END;
GO

-- ============================
-- Trigger
-- ============================
CREATE TRIGGER trg_AfterDelete_VatNuoi
ON VatNuoi
AFTER DELETE
AS
BEGIN
    DECLARE @MaVatNuoi VARCHAR(10)
    DECLARE @MaChuong VARCHAR(10)
    DECLARE @SoLuong INT
    DECLARE @MaNhanVien VARCHAR(10)

    SET @MaNhanVien = 'NV01' -- Giả lập mã nhân viên

    SELECT TOP 1
        @MaVatNuoi = MaVatNuoi,
        @MaChuong = MaChuong,
        @SoLuong = SoLuong
    FROM deleted

    EXEC sp_ThemLogSauXoaVatNuoi @MaNhanVien, @MaChuong, @MaVatNuoi, @SoLuong
END;
GO
