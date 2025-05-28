namespace QuanLyChanNuoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuongVatNuoi")]
    public partial class ChuongVatNuoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongVatNuoi()
        {
            VatNuois = new HashSet<VatNuoi>();
        }

        [Key]
        [StringLength(10)]
        public string MaChuong { get; set; }

        [StringLength(100)]
        public string ViTri { get; set; }

        public decimal? DienTich { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VatNuoi> VatNuois { get; set; }
    }
}
