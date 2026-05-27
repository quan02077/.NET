using Buoi12.Model;
using Buoi12.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Buoi12.BT2_BT4
{
    public class BT2VM:BaseVM
    {
        private string _tenDangNhap;
        public string TenDangNhap
        {
            get { return _tenDangNhap; }
            set
            {
                _tenDangNhap = value;
                OnPropertyChanged();
            }
        }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        private NHANVIEN currentNhanVien;
        public NHANVIEN CurrentNhanVien
        {
            get { return currentNhanVien; }
            set
            {
                currentNhanVien = value;
                OnPropertyChanged(nameof(CurrentNhanVien));
            }
        }
        private bool isQuanLy;
        public bool IsQuanLy
        {
            get => isQuanLy;
            set
            {
                isQuanLy = value;
                OnPropertyChanged(nameof(IsQuanLy));
            }
        }
        private bool isNhanVienKho;
        public bool IsNhanVienKho
        {
            get => isNhanVienKho;
            set
            {
                isNhanVienKho = value;
                OnPropertyChanged(nameof(IsNhanVienKho));
            }
        }
        private bool isNhanVienBanHang;
        public bool IsNhanVienBanHang
        {
            get => isNhanVienBanHang;
            set
            {
                isNhanVienBanHang = value;
                OnPropertyChanged(nameof(IsNhanVienBanHang));
            }
        }
        public ICommand DangNhapView { get; set; }
        public ICommand PhieuNhapView { get; set; }
        
        public BT2VM()
        {
            PhieuNhapView = new RelayCommand<object>(o => CurrentView = new ucPhieuNhap());
            DangNhapView = new RelayCommand<object>(ExecuteDangXuat);
        }
        public void PhanQuyen(string vaiTro)
        {
            IsQuanLy = false;
            IsNhanVienKho = false;
            IsNhanVienBanHang = false;
            if(vaiTro == "Quản lý")
            {
                IsQuanLy = true;
                IsNhanVienKho = true;
                IsNhanVienBanHang = true;
            }
            else if(vaiTro == "Nhân viên kho")
            {
                IsQuanLy = false;
                IsNhanVienKho = true;
                IsNhanVienBanHang = false;
            }
            else
            {
                IsQuanLy = false;
                IsNhanVienKho = false;
                IsNhanVienBanHang = true;
            }
        }
        private void ExecuteDangXuat(object obj)
        {
            Window currentWindow = obj as Window;
            Buoi12.BT1.DangNhap loginWindow = new Buoi12.BT1.DangNhap();
            loginWindow.Show();
            Application.Current.MainWindow = loginWindow;
            currentWindow?.Close();
        }
    }
}
