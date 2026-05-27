using Buoi12.Model;
using Buoi12.VM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Data.Entity;
using Buoi12.BT2_BT4;

namespace Buoi12.BT1
{
    public class DangNhapVM : BaseVM, IDataErrorInfo
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        private string _tenDangNhap;
        public string TenDangNhap
        {
            get { return _tenDangNhap; }
            set
            {
                _tenDangNhap = value;
                OnPropertyChanged(nameof(TenDangNhap));
            }
        }
        private string _matKhau;
        public string MatKhau
        {
            get { return _matKhau; }
            set
            {
                _matKhau = value;
                OnPropertyChanged(nameof(MatKhau));
            }
        }
        private Visibility thongBaoVisibility = Visibility.Collapsed;
        public Visibility ThongBaoVisibility
        {
            get { return thongBaoVisibility; }
            set
            {
                thongBaoVisibility = value;
                OnPropertyChanged(nameof(ThongBaoVisibility));
            }
        }
        #region Validation (IDataErrorInfo)
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == nameof(TenDangNhap))
                {
                    if (string.IsNullOrWhiteSpace(TenDangNhap))
                    {
                        result = "Tên đăng nhập không được để trống.";
                    }
                }
                else if (columnName == nameof(MatKhau))
                {
                    if (string.IsNullOrWhiteSpace(MatKhau))
                    {
                        result = "Mật khẩu không được để trống.";
                    }
                }
                return result;
            }
        }
        #endregion
        public ICommand DangNhapCmd { get; set; }
        public ICommand DoiViewCmd { get; set; }

        public DangNhapVM()
        {
            DangNhapCmd = new RelayCommand<object>(ExeDangNhap, DieuKienDN);
        }
        private bool DieuKienDN(object obj)
        {
            return string.IsNullOrWhiteSpace(this[nameof(TenDangNhap)]) && string.IsNullOrWhiteSpace(this[nameof(MatKhau)]);
        }
        private async void ExeDangNhap(object obj)
        {
            ThongBaoVisibility = Visibility.Collapsed;
            using (var db = new QL_KhoEntities())
            {
                var user = await db.NHANVIENs.FirstOrDefaultAsync(nv => nv.MaNV == TenDangNhap && nv.MatKhau == MatKhau);
                if (user != null)
                {
                    MessageBox.Show($"Đăng nhập thành công! Chào mừng {user.TenNV}.");
                    Buoi12.BT2_BT4.BT2 mainWindow = new Buoi12.BT2_BT4.BT2();

                    if (mainWindow.DataContext is Buoi12.BT2_BT4.BT2VM vm)
                    {
                        vm.TenDangNhap = user.TenNV;
                        vm.PhanQuyen(user.VaiTro);
                    }
                    mainWindow.Show();
                    Window formDangNhap = Application.Current.MainWindow;
                    Application.Current.MainWindow = mainWindow;
                    formDangNhap.Close();
                }
                else
                {
                    TenDangNhap = "";
                    MatKhau = "";
                    ThongBaoVisibility = Visibility.Visible;
                }
            }    
        }
    }
}
