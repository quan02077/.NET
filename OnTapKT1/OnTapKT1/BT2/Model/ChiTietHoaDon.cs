using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTapKT1.BT2.Model
{
    public class ChiTietHoaDon
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien
        {
            get { return DonGia * SoLuong; }
        }
        public override string ToString()
        {
            return $"{TenSanPham} | SL: {SoLuong} | Giá: {DonGia:N0} | Thành tiền: {ThanhTien:N0}";
        }
    }
}
