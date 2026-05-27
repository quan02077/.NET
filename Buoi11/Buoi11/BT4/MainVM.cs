using Buoi11.Model; 
using Buoi11.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Buoi11.BT4
{
    public class MainVM:BaseVM
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }
        public ICommand ShowKhoaCommand { get; set; }
        public ICommand ShowLopCommand { get; set; }
        public ICommand ShowMonHocCommand { get; set; }
        public ICommand ShowSinhVienCommand { get; set; }   
        public ICommand ShowDiemCommand { get; set; }

        public MainVM()
        {
            ShowKhoaCommand = new RelayCommand<object>(o => CurrentView = new ucKhoa());
            ShowLopCommand = new RelayCommand<object>(o => CurrentView = new ucLop());
            ShowMonHocCommand = new RelayCommand<object>(o => CurrentView = new ucMonHoc());
            ShowSinhVienCommand = new RelayCommand<object>(o => CurrentView = new ucSinhVien());
            ShowDiemCommand = new RelayCommand<object>(o => CurrentView = new ucDiem());
        }
    }
}
