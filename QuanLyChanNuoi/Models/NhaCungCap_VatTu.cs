namespace QuanLyChanNuoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NhaCungCap_VatTu
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaNhaCungCap { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaVatTu { get; set; }

        public decimal? DonGia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCungCap { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual VatTu VatTu { get; set; }
    }
}
