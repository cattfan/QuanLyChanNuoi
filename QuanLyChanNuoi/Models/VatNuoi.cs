namespace QuanLyChanNuoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VatNuoi")]
    public partial class VatNuoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VatNuoi()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        [Key]
        [StringLength(10)]
        public string MaVatNuoi { get; set; }

        [StringLength(100)]
        public string TenVatNuoi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayNhap { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(10)]
        public string MaChuong { get; set; }

        public virtual ChuongVatNuoi ChuongVatNuois { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
