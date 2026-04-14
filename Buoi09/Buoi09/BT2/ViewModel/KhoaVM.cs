using Buoi09.BT1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Buoi09.BT2.ViewModel
{
    public class KhoaVM : BaseVM
    {
        private QuanLySinhVienEntities db = new QuanLySinhVienEntities();

        public ObservableCollection<Khoa> DS_Khoa { get; set; }
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
        private Khoa _SelectedKhoa;
        public Khoa SelectedKhoa
        {
            get => _SelectedKhoa;
            set
            {
                _SelectedKhoa = value;
                OnPropertyChanged(nameof(SelectedKhoa));
                if (_SelectedKhoa != null)
                {
                    MaKhoa = _SelectedKhoa.MaKhoa;
                    TenKhoa = _SelectedKhoa.TenKhoa;
                }
            }
        }
        public ICommand ThemCmd { get; set; }
        public ICommand SuaCmd { get; set; }
        public ICommand XoaCmd { get; set; }

        public KhoaVM()
        {
            LoadData();
            ThemCmd = new RelayCommand<object>(ThucThiThem, DieuKienThem);
            SuaCmd = new RelayCommand<object>(ThucThiSua, DieuKienSuaXoa);
            XoaCmd = new RelayCommand<object>(ThucThiXoa, DieuKienSuaXoa);
        }

        private bool DieuKienThem(object p)
        {
            return true;
        }

        private bool DieuKienSuaXoa(object p)
        {
            return SelectedKhoa != null;
        }

        private void ThucThiThem(object p)
        {
            if (string.IsNullOrEmpty(MaKhoa) || string.IsNullOrEmpty(TenKhoa)) return;

            var khoaMoi = new Khoa { MaKhoa = MaKhoa, TenKhoa = TenKhoa };
            db.Khoas.Add(khoaMoi);
            db.SaveChanges();

            LoadData();
        }

        private void ThucThiSua(object p)
        {
            var khoaSua = db.Khoas.Find(SelectedKhoa.MaKhoa);
            if (khoaSua != null)
            {
                khoaSua.TenKhoa = TenKhoa;
                db.SaveChanges();
                LoadData();
            }
        }

        private void ThucThiXoa(object p)
        {
            var khoaXoa = db.Khoas.Find(SelectedKhoa.MaKhoa);
            if (khoaXoa != null)
            {
                db.Khoas.Remove(khoaXoa);
                db.SaveChanges();
                LoadData();
                MaKhoa = "";
                TenKhoa = "";
            }
        }
        void LoadData()
        {
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoas.ToList());
            OnPropertyChanged(nameof(DS_Khoa));
        }
    }
}
