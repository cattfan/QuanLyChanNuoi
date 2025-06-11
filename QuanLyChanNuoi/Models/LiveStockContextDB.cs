using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyChanNuoi.Models
{
    public partial class LiveStockContextDB : DbContext
    {
        public LiveStockContextDB()
            : base("name=LiveStockContextDB")
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChucVuNhanVien> ChucVuNhanViens { get; set; }
        public virtual DbSet<ChuongVatNuoi> ChuongVatNuois { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<Log_LichSuChuong> Log_LichSuChuong { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhaCungCap_VatTu> NhaCungCap_VatTu { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<ToNhanVien> ToNhanViens { get; set; }
        public virtual DbSet<VatNuoi> VatNuois { get; set; }
        public virtual DbSet<VatTu> VatTus { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaMatHang)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaNhaCungCap)
                .IsUnicode(false);

            modelBuilder.Entity<ChucVuNhanVien>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<ChuongVatNuoi>()
                .Property(e => e.MaChuong)
                .IsUnicode(false);

            modelBuilder.Entity<ChuongVatNuoi>()
                .Property(e => e.DienTich)
                .HasPrecision(10, 2);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Log_LichSuChuong>()
                .Property(e => e.MaLog)
                .IsUnicode(false);

            modelBuilder.Entity<Log_LichSuChuong>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<Log_LichSuChuong>()
                .Property(e => e.MaChuong)
                .IsUnicode(false);

            modelBuilder.Entity<Log_LichSuChuong>()
                .Property(e => e.MaVatNuoi)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.MaNhaCungCap)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.NhaCungCap_VatTu)
                .WithRequired(e => e.NhaCungCap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaCungCap_VatTu>()
                .Property(e => e.MaNhaCungCap)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap_VatTu>()
                .Property(e => e.MaVatTu)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaTo)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.VatNuois)
                .WithMany(e => e.NhanViens)
                .Map(m => m.ToTable("NhanVien_VatNuoi").MapLeftKey("MaNhanVien").MapRightKey("MaVatNuoi"));

            modelBuilder.Entity<ToNhanVien>()
                .Property(e => e.MaTo)
                .IsUnicode(false);

            modelBuilder.Entity<ToNhanVien>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<VatNuoi>()
                .Property(e => e.MaVatNuoi)
                .IsUnicode(false);

            modelBuilder.Entity<VatNuoi>()
                .Property(e => e.MaChuong)
                .IsUnicode(false);

            modelBuilder.Entity<VatTu>()
                .Property(e => e.MaVatTu)
                .IsUnicode(false);

            modelBuilder.Entity<VatTu>()
                .HasMany(e => e.NhaCungCap_VatTu)
                .WithRequired(e => e.VatTu)
                .WillCascadeOnDelete(false);
        }
    }
}
