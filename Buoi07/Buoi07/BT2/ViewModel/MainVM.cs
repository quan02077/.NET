using Buoi07.BT2.Model;
using Buoi07.BT3.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Buoi07.BT2.ViewModel
{
    public class MainVM:BaseVM
    {
        private ObservableCollection<KhachHangModel> customers;
        public ObservableCollection<KhachHangModel> Customers
        {
            get => customers;
            set
            {
                customers = value;
                OnPropertyChanged("Customers");
            }
        }
        private string newName;
        public string NewName
        {
            get => newName;
            set
            {
                newName = value;
                OnPropertyChanged("NewName");
            }
        }
        private string newAddress;
        public string NewAddress
        {
            get => newAddress;
            set
            {
                newAddress = value;
                OnPropertyChanged("NewAddress");
            }
        }
        private int dayStay;
        public int DayStay
        {
            get => dayStay;
            set
            {
                dayStay = value;
                OnPropertyChanged("DayStay");
            }
        }
        private bool _isPhongDon = true;
        public bool IsPhongDon { get => _isPhongDon; set { _isPhongDon = value; OnPropertyChanged("IsPhongDon"); } }

        private bool _isPhongDoi;
        public bool IsPhongDoi { get => _isPhongDoi; set { _isPhongDoi = value; OnPropertyChanged("IsPhongDoi"); } }

        private bool _isPhongBa;
        public bool IsPhongBa { get => _isPhongBa; set { _isPhongBa = value; OnPropertyChanged("IsPhongBa"); } }

        private bool _hasTivi;
        public bool HasTivi { get => _hasTivi; set { _hasTivi = value; OnPropertyChanged("HasTivi"); } }

        private bool _hasInternet;
        public bool HasInternet { get => _hasInternet; set { _hasInternet = value; OnPropertyChanged("HasInternet"); } }

        private bool _hasMayNuocNong;
        public bool HasMayNuocNong { get => _hasMayNuocNong; set { _hasMayNuocNong = value; OnPropertyChanged("HasMayNuocNong"); } }

        private bool _hasKaraoke;
        public bool HasKaraoke { get => _hasKaraoke; set { _hasKaraoke = value; OnPropertyChanged("HasKaraoke"); } }

        private bool _hasAnSang;
        public bool HasAnSang { get => _hasAnSang; set { _hasAnSang = value; OnPropertyChanged("HasAnSang"); } }

        private decimal _thanhTien;
        public decimal ThanhTien { get => _thanhTien; set { _thanhTien = value; OnPropertyChanged("ThanhTien"); } }

        private int _tongSoLuotNguoi;
        public int TongSoLuotNguoi { get => _tongSoLuotNguoi; set { _tongSoLuotNguoi = value; OnPropertyChanged("TongSoLuotNguoi"); } }

        private decimal _tongDoanhThu;
        public decimal TongDoanhThu { get => _tongDoanhThu; set { _tongDoanhThu = value; OnPropertyChanged("TongDoanhThu"); } }

        public ICommand ThanhToanCommand { get; set; }
        public ICommand NhapMoiCommand { get; set; }
        public ICommand TongKetCommand { get; set; }
        public ICommand ThoatCommand { get; set; }
        public MainVM()
        {
            Customers = new ObservableCollection<KhachHangModel>();

            ThanhToanCommand = new RelayCommand(ExecuteThanhToan, CanExecuteThanhToan);
            NhapMoiCommand = new RelayCommand(ExecuteNhapMoi);
            TongKetCommand = new RelayCommand(ExecuteTongKet);
            ThoatCommand = new RelayCommand(ExecuteThoat);
        }
        private bool CanExecuteThanhToan(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewName) && DayStay > 0; 
        }
        private void ExecuteThanhToan(object parameter)
        {
            decimal giaPhong = IsPhongDon ? 300000 : (IsPhongDoi ? 350000 : 400000);
            decimal tienPhong = giaPhong * DayStay;

            decimal tienTienNghi = 0;
            if (HasTivi) tienTienNghi += 10000;
            if (HasInternet) tienPhong += 10000;
            if (HasMayNuocNong) tienPhong += 10000;

            decimal tienDichVu = 0;
            if (HasKaraoke) tienDichVu += 50000;
            if (HasAnSang) tienDichVu += (15000 * DayStay);

            ThanhTien = tienPhong + tienTienNghi + tienDichVu;

            var khachHangMoi = new KhachHangModel
            {
                HoTen = this.NewName,
                DiaChi = this.NewAddress,
                SoNgay = this.DayStay,
                ThanhTien = this.ThanhTien
            };
            Customers.Add(khachHangMoi);

            MessageBox.Show($"Đã thanh toán {ThanhTien:N0} VNĐ cho khách {NewName}!", "Thành công");
        }
        private void ExecuteNhapMoi(object parameter)
        {
            NewName = string.Empty;
            NewAddress = string.Empty;
            DayStay = 0;
            ThanhTien = 0;
            IsPhongDon = true;
            HasTivi = HasInternet = HasMayNuocNong = HasKaraoke = HasAnSang = false;
        }
        private void ExecuteTongKet(object parameter)
        {
            TongSoLuotNguoi = Customers.Count;
            TongDoanhThu = Customers.Sum(x => x.ThanhTien);
        }
        private void ExecuteThoat(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}
