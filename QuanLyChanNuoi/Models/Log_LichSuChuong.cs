namespace QuanLyChanNuoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log_LichSuChuong
    {
        [Key]
        [StringLength(10)]
        public string MaLog { get; set; }

        [StringLength(10)]
        public string MaNhanVien { get; set; }

        [StringLength(10)]
        public string MaChuong { get; set; }

        [StringLength(10)]
        public string MaVatNuoi { get; set; }

        public int? LichSuSoLuongVatNuoiTrongChuong { get; set; }

        public DateTime? log_thoigian { get; set; }
    }
}
