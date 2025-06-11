// QuanLyChanNuoi/LivestockRepost.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChanNuoi
{
    // Đổi tên các thuộc tính để khớp với Fields trong file .rdlc
    internal class LivestockRepost
    {
        public int Nhanvien { get; set; }
        public int Vatnuoi { get; set; }
        public int Chuongnuoi { get; set; }
        public int Vattu { get; set; }
        public int Nhacungcap { get; set; }
        public int Hoadon { get; set; }
    }
}