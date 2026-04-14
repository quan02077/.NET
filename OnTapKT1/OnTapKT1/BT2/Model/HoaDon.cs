using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTapKT1.BT2.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class HoaDon
    {
        public string TenKhachHang { get; set; }
        public string DienThoai { get; set; }
        public string TenBan { get; set; }
        public List<ChiTietHoaDon> DanhSachChiTietHoaDon { get; set; } = new List<ChiTietHoaDon>();
        public double TongTien
        {
            get
            {
                return DanhSachChiTietHoaDon.Sum(ct => ct.ThanhTien);
            }
        }
        public override string ToString()
        {
            return $"Khách: {TenKhachHang} - Bàn: {TenBan} - Tổng tiền: {TongTien:N0} VNĐ";
        }
    }
}
