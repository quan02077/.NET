using Buoi12.Model;
using Buoi12.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Buoi12.BT3
{
    public class PhieuNhapVM : BaseVM
    {
        public DateTime NgayNhap { get; set; } = DateTime.Now;

        public ObservableCollection<NHACUNGCAP> DanhSachNCC { get; set; }
        public ObservableCollection<SANPHAM> DanhSachSP { get; set; }

        private NHACUNGCAP _selectedNCC;
        public NHACUNGCAP SelectedNCC { get => _selectedNCC; set { _selectedNCC = value; OnPropertyChanged(); } }

        private SANPHAM _selectedSP;
        public SANPHAM SelectedSP { get => _selectedSP; set { _selectedSP = value; OnPropertyChanged(); } }

        private int _soLuongNhap;
        public int SoLuongNhap { get => _soLuongNhap; set { _soLuongNhap = value; OnPropertyChanged(); } }

        private decimal _donGiaNhap;
        public decimal DonGiaNhap { get => _donGiaNhap; set { _donGiaNhap = value; OnPropertyChanged(); } }

        private decimal _tongTien;
        public decimal TongTien { get => _tongTien; set { _tongTien = value; OnPropertyChanged(); } }
        public ObservableCollection<ChiTietPhieuNhapModel> DanhSachChiTiet { get; set; }
        public ChiTietPhieuNhapModel SelectedChiTiet { get; set; } 

        private bool _isChoPhepTaoPhieu = true;
        public bool IsChoPhepTaoPhieu { get => _isChoPhepTaoPhieu; set { _isChoPhepTaoPhieu = value; OnPropertyChanged(); } }
        public ICommand TaoPhieuCmd { get; set; }
        public ICommand ThemChiTietCmd { get; set; }
        public ICommand XoaChiTietCmd { get; set; }
        public ICommand LuuPhieuCmd { get; set; }
        public ICommand HuyCmd { get; set; }

        public PhieuNhapVM()
        {
            DanhSachChiTiet = new ObservableCollection<ChiTietPhieuNhapModel>();
            LoadComboboxData();

            TaoPhieuCmd = new RelayCommand<object>(ExeTaoPhieu, (p) => SelectedNCC != null && IsChoPhepTaoPhieu);
            ThemChiTietCmd = new RelayCommand<object>(ExeThemChiTiet, CanThemChiTiet);
            XoaChiTietCmd = new RelayCommand<object>(ExeXoaChiTiet, (p) => SelectedChiTiet != null);
            LuuPhieuCmd = new RelayCommand<object>(ExeLuuPhieu, (p) => DanhSachChiTiet.Count > 0);
            HuyCmd = new RelayCommand<object>(ExeHuy, (p) => true);
        }

        private void LoadComboboxData()
        {
            using (var db = new QL_KhoEntities())
            {
                DanhSachNCC = new ObservableCollection<NHACUNGCAP>(db.NHACUNGCAPs.ToList());
                DanhSachSP = new ObservableCollection<SANPHAM>(db.SANPHAMs.ToList());
            }
        }

        private void ExeTaoPhieu(object obj)
        { 
            IsChoPhepTaoPhieu = false;
        }

        private bool CanThemChiTiet(object obj)
        {
            return !IsChoPhepTaoPhieu && SelectedSP != null && SoLuongNhap > 0 && DonGiaNhap > 0;
        }

        private void ExeThemChiTiet(object obj)
        {
            var item = new ChiTietPhieuNhapModel
            {
                MaSP = SelectedSP.MASANPHAM,
                TenSP = SelectedSP.TENSANPHAM,
                SoLuong = SoLuongNhap,
                DonGia = DonGiaNhap
            };
            DanhSachChiTiet.Add(item);
            CapNhatTongTien();
        }

        private void ExeXoaChiTiet(object obj)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DanhSachChiTiet.Remove(SelectedChiTiet);
                CapNhatTongTien();
            }
        }

        private void CapNhatTongTien()
        {
            TongTien = DanhSachChiTiet.Sum(x => x.ThanhTien);
        }

        private void ExeLuuPhieu(object obj)
        {
            using (var db = new QL_KhoEntities())
            {
                int count = db.PHIEUNHAPs.Count();
                string maPhieuMoi = "PN" + (count + 1).ToString("D6");
                var phieuNhap = new PHIEUNHAP
                {
                    MAPHIEUNHAP = maPhieuMoi,
                    MANCC = SelectedNCC.MANCC,
                    NGAYNHAP = NgayNhap,
                    THANHTIEN = TongTien,
                    MANV = "NV01" 
                };
                db.PHIEUNHAPs.Add(phieuNhap);

                foreach (var item in DanhSachChiTiet)
                {
                    var chiTiet = new CHITIETPHIEUNHAP
                    {
                        MAPHIEUNHAP = maPhieuMoi,
                        MASANPHAM = item.MaSP,
                        SOLUONG = item.SoLuong,
                        DONGIA = item.DonGia
                    };
                    db.CHITIETPHIEUNHAPs.Add(chiTiet);
                    var tonKho = db.TONKHO_NGAY.FirstOrDefault(t => t.MASANPHAM == item.MaSP);
                    if (tonKho != null)
                    {
                        tonKho.SOLUONGTON += item.SoLuong;
                    }
                    else
                    {
                        db.TONKHO_NGAY.Add(new TONKHO_NGAY { MASANPHAM = item.MaSP, SOLUONGTON = item.SoLuong });
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Lưu phiếu nhập thành công!");
                ExeHuy(null);
            }
        }

        private void ExeHuy(object obj)
        {
            DanhSachChiTiet.Clear();
            TongTien = 0;
            SoLuongNhap = 0;
            DonGiaNhap = 0;
            SelectedNCC = null;
            SelectedSP = null;
            IsChoPhepTaoPhieu = true;
        }
    }
}
