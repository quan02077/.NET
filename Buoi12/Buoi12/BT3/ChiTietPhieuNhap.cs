using Buoi12.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi12.BT3
{
    public class ChiTietPhieuNhapModel : BaseVM
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }

        private int _soLuong;
        public int SoLuong { get => _soLuong; set { _soLuong = value; OnPropertyChanged(); OnPropertyChanged(nameof(ThanhTien)); } }

        private decimal _donGia;
        public decimal DonGia { get => _donGia; set { _donGia = value; OnPropertyChanged(); OnPropertyChanged(nameof(ThanhTien)); } }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
