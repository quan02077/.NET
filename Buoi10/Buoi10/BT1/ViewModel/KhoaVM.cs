using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buoi10.BT1.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Buoi10.BT1.ViewModel
{
    public class KhoaVM:BaseVM
    {
        private QuanLySinhVienEntities db = new QuanLySinhVienEntities();
        public ObservableCollection<Khoa> DS_Khoa { get; set; }
        private Khoa selectedKhoa;
        public Khoa SelectedKhoa
        {
            get => selectedKhoa;
            set
            {
                selectedKhoa = value;
                OnPropertyChanged(nameof(SelectedKhoa));
            }
        }
        private string _maKhoa;
        public string MaKhoa
        {
            get => _maKhoa;
            set { _maKhoa = value; OnPropertyChanged(nameof(MaKhoa)); }
        }

        private string _tenKhoa;
        public string TenKhoa
        {
            get => _tenKhoa;
            set { _tenKhoa = value; OnPropertyChanged(nameof(TenKhoa)); }
        }
        public ICommand ThemCmd { get; set; }
        public ICommand SuaCmd { get; set; }
        public ICommand XoaCmd { get; set; }
        public KhoaVM()
        {
            LoadData();
            ThemCmd = new RelayCommand<object>(ThucThiThem, DieuKienThem);
            SuaCmd = new RelayCommand<object>(ThucThiSua, DieuKienSua);
            XoaCmd = new RelayCommand<object>(ThucThiXoa, DieuKienXoa);
        }
        private bool DieuKienThem(object obj)
        {
            return !string.IsNullOrEmpty(MaKhoa) && !string.IsNullOrEmpty(TenKhoa);
        }
        private void ThucThiThem(object obj)
        {
            if(string.IsNullOrWhiteSpace(MaKhoa) || string.IsNullOrWhiteSpace(TenKhoa))
            {
                return;
            }
            var khoaMoi = new Khoa
            {
                MaKhoa = MaKhoa,
                TenKhoa = TenKhoa
            };
            db.Khoas.Add(khoaMoi);
            db.SaveChanges();
            LoadData();
            MaKhoa = string.Empty;
            TenKhoa = string.Empty;
        }
        private bool DieuKienSua(object obj)
        {
            return SelectedKhoa != null && !string.IsNullOrEmpty(MaKhoa) && !string.IsNullOrEmpty(TenKhoa);
        }
        private void ThucThiSua(object obj)
        {
            if (SelectedKhoa == null) return;
            var khoaSua = db.Khoas.Find(SelectedKhoa.MaKhoa);
            if (khoaSua != null)
            {
                khoaSua.MaKhoa = MaKhoa;
                khoaSua.TenKhoa = TenKhoa;
                db.SaveChanges();
                LoadData();
                MaKhoa = string.Empty;
                TenKhoa = string.Empty;
            }
        }
        private bool DieuKienXoa(object obj)
        {
            return SelectedKhoa != null;
        }
        private void ThucThiXoa(object obj)
        {
            if (SelectedKhoa == null) return;
            var khoaXoa = db.Khoas.Find(SelectedKhoa.MaKhoa);
            if (khoaXoa != null)
            {
                db.Khoas.Remove(khoaXoa);
                db.SaveChanges();
                LoadData();
                MaKhoa = string.Empty;
                TenKhoa = string.Empty;
            }
        }
        void LoadData()
        {
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoas.ToList());
            OnPropertyChanged(nameof(DS_Khoa));
        }
    }
}
