using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OnTapKT1.BT2.Model;
using System.Windows.Input;
using System.Windows;

namespace OnTapKT1.BT2.ViewModel
{
    public class MainVM : BaseVM
    {
        private KhoDuLieu _khoDuLieu;
        private HoaDon _hoaDonHienTai;
        public ObservableCollection<SanPham> DanhSachSanPhamLoc {  get; set; }
        private SanPham selectionSP;
        public SanPham SelectionSP
        {
            get { return selectionSP; }
            set
            {
                selectionSP = value;
                OnPropertyChanged("SelectionSP");
            }
        }
        private ObservableCollection<LoaiSanPham> loaiSanPham;
        public ObservableCollection<LoaiSanPham> LoaiSanPham
        {
            get { return loaiSanPham; }
            set
            {
                loaiSanPham = value;
                OnPropertyChanged("LoaiSanPham");
            }
        }
        private LoaiSanPham selectionLSP;
        public LoaiSanPham SelectionLSP
        {
            get { return selectionLSP; }
            set
            {
                selectionLSP = value;
                OnPropertyChanged("SelectionLSP");
                LocDanhSachSanPham();
            }
        }
        private ObservableCollection<ChiTietHoaDon> chiTietHoaDon;
        public ObservableCollection<ChiTietHoaDon> ChiTietHoaDon
        {
            get { return chiTietHoaDon; }
            set
            {
                chiTietHoaDon = value;
                OnPropertyChanged("ChiTietHoaDon");
            }
        }
        public double TongTienHienTai => _hoaDonHienTai.TongTien;

        private ObservableCollection<string> viTri;
        public ObservableCollection<string> ViTri
        {
            get { return viTri; }
            set
            {
                viTri = value;
                OnPropertyChanged("ViTri");
            }
        }
        private string selectionViTri;
        public string SelectionViTri
        {
            get { return selectionViTri; }
            set
            {
                selectionViTri = value;
                OnPropertyChanged("SelectionViTri");
            }
        }
        private string newSL;
        public string NewSL
        {
            get { return newSL; }
            set
            {
                newSL = value;
                OnPropertyChanged("NewSL");
            }
        }
        private string newTenKH;
        public string NewTenKH
        {
            get { return newTenKH; }
            set
            {
                newTenKH = value;
                OnPropertyChanged("NewTenKH");
            }
        }
        private string newDienThoai;
        public string NewDienThoai
        {
            get
            {
                return newDienThoai;
            }
            set
            {
                newDienThoai = value;
                OnPropertyChanged("NewDienThoai");
            }
        }

        public ICommand ThemCommand { get; set; }
        public ICommand TinhTienCommand { get; set; }
        public ICommand ThoatCommand { get; set; }

        public MainVM()
        {
            _khoDuLieu = new KhoDuLieu();
            _hoaDonHienTai = new HoaDon();

            LoaiSanPham = new ObservableCollection<LoaiSanPham>(_khoDuLieu.DanhSachLoaiSanPham);
            DanhSachSanPhamLoc = new ObservableCollection<SanPham>();
            ChiTietHoaDon = new ObservableCollection<ChiTietHoaDon>();
            ViTri = new ObservableCollection<string> { "Bàn 1", "Bàn 2", "Bàn 3", "Bàn 4", "Bàn 5" };

            ThemCommand = new RelayCommand(ExecuteThem);
            TinhTienCommand = new RelayCommand(ExecuteTinhTien);
            ThoatCommand = new RelayCommand(ExecuteThoat);
        }

        private void LocDanhSachSanPham()
        {
            if (SelectionLSP != null)
            {
                DanhSachSanPhamLoc.Clear();
                var sanPhamLoc = _khoDuLieu.DanhSachSanPham.Where(sp => sp.MaLoai == SelectionLSP.MaLoai).ToList();
                foreach (var sp in sanPhamLoc)
                {
                    DanhSachSanPhamLoc.Add(sp);
                }
            }
        }
        private void ExecuteThem(object parameter)
        {
            if(SelectionSP == null)
            {
                MessageBox.Show("Vui lòng chọn thức uống!");
                return;
            }
            if (!int.TryParse(NewSL, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên lớn hơn 0!");
                return;
            }
            var chiTiet = new ChiTietHoaDon
            {
                MaSanPham = SelectionSP.MaSanPham,
                TenSanPham = SelectionSP.TenSanPham,
                DonGia = SelectionSP.DonGia,
                SoLuong = soLuong
            };

            ChiTietHoaDon.Add(chiTiet);
            _hoaDonHienTai.DanhSachChiTietHoaDon.Add(chiTiet);
                
            OnPropertyChanged(nameof(TongTienHienTai));

            NewSL = "";
        }
        private void ExecuteTinhTien(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewTenKH) || string.IsNullOrWhiteSpace(NewDienThoai) || string.IsNullOrWhiteSpace(SelectionViTri))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin khách hàng và chọn vị trí bàn!");
                return;
            }

            if (_hoaDonHienTai.DanhSachChiTietHoaDon.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có sản phẩm nào!");
                return;
            }

            _hoaDonHienTai.TenKhachHang = NewTenKH;
            _hoaDonHienTai.DienThoai = NewDienThoai;
            _hoaDonHienTai.TenBan = SelectionViTri;

            MessageBox.Show(_hoaDonHienTai.ToString(), "Thông tin hóa đơn tính tiền", MessageBoxButton.OK, MessageBoxImage.Information);
            NewTenKH = string.Empty;
            NewDienThoai = string.Empty;

        }

        private void ExecuteThoat(object parameter)
        {
            if (parameter is Window window)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    window.Close();
                }
            }
        }
    }
}
