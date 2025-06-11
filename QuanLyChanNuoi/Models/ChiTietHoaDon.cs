namespace QuanLyChanNuoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaHoaDon { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int STT { get; set; }

        [StringLength(50)]
        public string LoaiMatHang { get; set; }

        [StringLength(10)]
        public string MaMatHang { get; set; }

        public int? SoLuong { get; set; }

        public decimal? DonGia { get; set; }

        [StringLength(10)]
        public string MaNhanVien { get; set; }

        [StringLength(10)]
        public string MaNhaCungCap { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }

        [ForeignKey("MaMatHang")]
        public virtual VatTu VatTu { get; set; }
        
        
    }
}
