using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTapKT1.BT2.Model
{
    public class KhoDuLieu
    {
        public List<LoaiSanPham> DanhSachLoaiSanPham { get; set; }
        public List<SanPham> DanhSachSanPham { get; set; }

        public KhoDuLieu()
        {
            KhoiTaoDuLieu();
        }

        private void KhoiTaoDuLieu()
        {
            DanhSachLoaiSanPham = new List<LoaiSanPham>
        {
            new LoaiSanPham("N1", "Cafe"),
            new LoaiSanPham("N2", "Trà"),
            new LoaiSanPham("N3", "Sinh tố")
        };
            DanhSachSanPham = new List<SanPham>
        {
            new SanPham("1", "Café đá", 23000, "N1"),
            new SanPham("2", "Café sữa", 28000, "N1"),
            new SanPham("3", "Bạc xỉu", 30000, "N1"),
            new SanPham("4", "Trà sữa Oolong", 35000, "N2"),
            new SanPham("5", "Trà lài", 32000, "N2"),
            new SanPham("6", "Trà hoa cúc", 34000, "N2"),
            new SanPham("7", "Sinh tố dâu", 40000, "N3")
        };
        }
    }
}
