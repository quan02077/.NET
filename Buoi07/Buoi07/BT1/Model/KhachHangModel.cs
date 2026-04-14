using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi07.BT1.Model
{
    public class KhachHangModel
    {
        public int STT { get; set; }
        public string HoTen { get; set; }
        public string SĐT { get; set; }
        public bool LaSinhVien { get; set; }

        public string ViTriBan { get; set; }
        public string NuocUong { get; set; }
        public string ThucAn { get; set; }
        public decimal TongTien { get; set; }
        public string HienThiSinhVien => LaSinhVien ? "Có" : "Không";
    }
}
