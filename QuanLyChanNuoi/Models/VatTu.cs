namespace QuanLyChanNuoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VatTu")]
    public partial class VatTu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VatTu()
        {
            NhaCungCap_VatTu = new HashSet<NhaCungCap_VatTu>();
        }

        [Key]
        [StringLength(10)]
        public string MaVatTu { get; set; }

        [StringLength(100)]
        public string TenVatTu { get; set; }

        [StringLength(30)]
        public string DonViTinh { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhaCungCap_VatTu> NhaCungCap_VatTu { get; set; }
    }
}
