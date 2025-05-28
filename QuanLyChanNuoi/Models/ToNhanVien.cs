namespace QuanLyChanNuoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ToNhanVien")]
    public partial class ToNhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ToNhanVien()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        [Key]
        [StringLength(10)]
        public string MaTo { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTo { get; set; }

        [Required]
        [StringLength(30)]
        public string TenDN { get; set; }

        [StringLength(30)]
        public string MatKhau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
