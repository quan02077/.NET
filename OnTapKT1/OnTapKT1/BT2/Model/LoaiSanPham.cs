using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTapKT1.BT2.Model
{
    public class LoaiSanPham
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public LoaiSanPham(string maLoai, string tenLoai)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
        }
    }
}
