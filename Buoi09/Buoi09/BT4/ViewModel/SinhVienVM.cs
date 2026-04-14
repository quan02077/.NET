using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Buoi09.BT1.Model;
using System.Windows.Input;
using System.Windows;

namespace Buoi09.BT4.ViewModel
{
    public class SinhVienVM : BaseVM
    {
        private QuanLySinhVienEntities db = new QuanLySinhVienEntities();

        public ObservableCollection<SinhVien> DS_SinhVien { get; set; }
        public ObservableCollection<Lop> DS_Lop { get; set; } 
        public List<string> DS_GioiTinh { get; set; } = new List<string> { "Nam", "Nữ" }; 

        private SinhVien selectedSinhVien;
        public SinhVien SelectedSinhVien
        {
            get => selectedSinhVien;
            set
            {
                selectedSinhVien = value;
                OnPropertyChanged(nameof(SelectedSinhVien));
                if (selectedSinhVien != null)
                {
                    MaSV = selectedSinhVien.MaSinhVien;
                    TenSV = selectedSinhVien.HoTen;
                    GioiTinh = selectedSinhVien.GioiTinh;
                    NgaySinh = selectedSinhVien.NgaySinh;
                    SelectedLop = selectedSinhVien.MaLop;
                }
            }
        }

        private string maSV;
        public string MaSV
        {
            get => maSV;
            set { maSV = value; OnPropertyChanged(nameof(MaSV)); }
        }

        private string tenSV;
        public string TenSV
        {
            get => tenSV;
            set { tenSV = value; OnPropertyChanged(nameof(TenSV)); }
        }

        private string gioiTinh;
        public string GioiTinh
        {
            get => gioiTinh;
            set { gioiTinh = value; OnPropertyChanged(nameof(GioiTinh)); }
        }

        private DateTime? ngaySinh;
        public DateTime? NgaySinh
        {
            get => ngaySinh;
            set { ngaySinh = value; OnPropertyChanged(nameof(NgaySinh)); }
        }

        private string selectedLop;
        public string SelectedLop
        {
            get => selectedLop;
            set { selectedLop = value; OnPropertyChanged(nameof(SelectedLop)); }
        }

        public ICommand ThemCmd { get; set; }
        public ICommand SuaCmd { get; set; }
        public ICommand XoaCmd { get; set; }
        public ICommand LuuCmd { get; set; }
        public ICommand HuyCmd { get; set; }

        public SinhVienVM()
        {
            LoadData();
            ThemCmd = new RelayCommand<object>(ThucThiThem, DieuKienThem);
            SuaCmd = new RelayCommand<object>(ThucThiSua, DieuKienSuaXoa);
            XoaCmd = new RelayCommand<object>(ThucThiXoa, DieuKienSuaXoa);
            LuuCmd = new RelayCommand<object>(ThucThiLuu, DieuKienLuu);
            HuyCmd = new RelayCommand<object>(ThucThiHuy, DieuKienHuy);
        }

        private bool DieuKienThem(object p) { return true; }
        private bool DieuKienSuaXoa(object p) { return SelectedLop != null; }
        private bool DieuKienLuu(object p) { return true; }
        private bool DieuKienHuy(object p) { return true; }

        private void ThucThiThem(object p)
        {
            if (string.IsNullOrEmpty(MaSV) || string.IsNullOrEmpty(TenSV) || string.IsNullOrEmpty(GioiTinh) || NgaySinh == null || string.IsNullOrEmpty(SelectedLop)) return;
            var newSV = new SinhVien
            {
                MaSinhVien = MaSV,
                HoTen = TenSV,
                GioiTinh = GioiTinh,
                NgaySinh = NgaySinh,
                MaLop = SelectedLop
            };
            db.SinhViens.Add(newSV);
            db.SaveChanges();
            LoadData();
            MaSV = TenSV = GioiTinh = SelectedLop = null;
        }

        private void ThucThiSua(object p)
        {
            if (SelectedSinhVien == null) return;
            var sv = db.SinhViens.Find(SelectedSinhVien.MaSinhVien);
            if (sv != null)
            {
                sv.HoTen = TenSV;
                sv.GioiTinh = GioiTinh;
                sv.NgaySinh = NgaySinh;
                sv.MaLop = SelectedLop;
                db.SaveChanges();
                LoadData();
                MaSV = TenSV = GioiTinh = SelectedLop = null;
            }
        }

        private void ThucThiXoa(object p)
        {
            if (SelectedSinhVien == null) return;
            var sv = db.SinhViens.Find(SelectedSinhVien.MaSinhVien);
            if (sv != null)
            {
                db.SinhViens.Remove(sv);
                db.SaveChanges();
                LoadData();
                MaSV = TenSV = GioiTinh = SelectedLop = null;
            }
        }
        private void ThucThiLuu(object p)
        {
            MessageBox.Show("Lưu thành công!");
            db.SaveChanges();
            LoadData();
            MaSV = TenSV = GioiTinh = SelectedLop = null;
        }
        private void ThucThiHuy(object p)
        {
            MessageBox.Show("Hủy thao tác!");
            LoadData();
            MaSV = TenSV = GioiTinh = SelectedLop = null;

        }
        private void LoadData()
        {
            DS_SinhVien = new ObservableCollection<SinhVien>(db.SinhViens.ToList());
            DS_Lop = new ObservableCollection<Lop>(db.Lops.ToList());

            OnPropertyChanged(nameof(DS_SinhVien));
            OnPropertyChanged(nameof(DS_Lop));
        }
    }
}