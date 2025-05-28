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
('NV01', N'Nguyễn Văn A', '1990-01-01', N'Nam', 'TO01', 'CV01'),
('NV02', N'Lê Thị B', '1995-05-05', N'Nữ', 'TO02', 'CV02');
GO

INSERT INTO ChuongVatNuoi (MaChuong, ViTri, DienTich) VALUES
('C01', N'Khu A1', 20.5),
('C02', N'Khu B1', 15.0);
GO

INSERT INTO VatNuoi (MaVatNuoi, TenVatNuoi, NgayNhap, SoLuong, MaChuong) VALUES
('VN01', N'Gà trống', '2024-10-01', 10, 'C01'),
('VN02', N'Vịt xiêm', '2024-11-01', 15, 'C02');
GO

INSERT INTO NhanVien_VatNuoi (MaNhanVien, MaVatNuoi) VALUES
('NV01', 'VN01'),
('NV02', 'VN02');
GO

INSERT INTO VatTu (MaVatTu, TenVatTu, DonViTinh, SoLuong, TrangThai) VALUES
('VT01', N'Thức ăn gà', N'Bao', 78, N'Còn'),
('VT02', N'Thức ăn vịt', N'Bao', 100, N'Còn');
GO

INSERT INTO NhaCungCap (MaNhaCungCap, TenNhaCungCap, DiaChi) VALUES
('NCC01', N'Công ty ABC', N'Hà Nội'),
('NCC02', N'Công ty XYZ', N'HCM');
GO

INSERT INTO NhaCungCap_VatTu (MaNhaCungCap, MaVatTu, DonGia, NgayCungCap) VALUES
('NCC01', 'VT01', 15000, '2025-01-15'),
('NCC02', 'VT02', 12000, '2025-01-20');
GO

INSERT INTO HoaDon (MaHoaDon, NgayLap) VALUES
('HD01', '2025-02-01');
GO

INSERT INTO ChiTietHoaDon (MaHoaDon, STT, LoaiMatHang, MaMatHang, SoLuong, DonGia, MaNhanVien, MaNhaCungCap) VALUES
('HD01', 1, N'Vật tư', 'VT01', 10, 15000, 'NV01', 'NCC01'),
('HD01', 2, N'Vật tư', 'VT02', 20, 12000, 'NV02', 'NCC02');
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
