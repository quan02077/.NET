using Buoi09.BT1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Buoi09.BT3.ViewModel
{
    public class LopVM : BaseVM
    {
        private QuanLySinhVienEntities db = new QuanLySinhVienEntities();

        public ObservableCollection<Lop> DS_Lop { get; set; }
        public ObservableCollection<Khoa> DS_Khoa { get; set; }

        private string _maLop;
        public string MaLop
        {
            get => _maLop;
            set { _maLop = value; OnPropertyChanged(nameof(MaLop)); }
        }

        private string selectedMaKhoa;
        public string SelectedMaKhoa
        {
            get => selectedMaKhoa;
            set { selectedMaKhoa = value; OnPropertyChanged(nameof(SelectedMaKhoa)); }
        }

        private Lop selectedLop;
        public Lop SelectedLop
        {
            get => selectedLop;
            set
            {
                selectedLop = value;
                OnPropertyChanged(nameof(SelectedLop));
                if (selectedLop != null)
                {
                    MaLop = selectedLop.MaLop;
                    SelectedMaKhoa = selectedLop.MaKhoa;
                }
            }
        }

        public ICommand ThemCmd { get; set; }
        public ICommand SuaCmd { get; set; }
        public ICommand XoaCmd { get; set; }
        public ICommand LuuCmd { get; set; }
        public ICommand HuyCmd { get; set; }

        public LopVM()
        {
            LoadData();
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoas.ToList());

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
            if (string.IsNullOrEmpty(MaLop) || string.IsNullOrEmpty(SelectedMaKhoa)) return;

            var lopMoi = new Lop { MaLop = MaLop, MaKhoa = SelectedMaKhoa };
            db.Lops.Add(lopMoi);
            db.SaveChanges();

            LoadData();
        }

        private void ThucThiSua(object p)
        {
            var lopSua = db.Lops.Find(SelectedLop.MaLop);
            if (lopSua != null)
            {
                lopSua.MaKhoa = SelectedMaKhoa;
                db.SaveChanges();
                LoadData();
            }
        }

        private void ThucThiXoa(object p)
        {
            var lopXoa = db.Lops.Find(SelectedLop.MaLop);
            if (lopXoa != null)
            {
                db.Lops.Remove(lopXoa);
                db.SaveChanges();
                LoadData();
                MaLop = string.Empty;
                SelectedMaKhoa = null; 
            }
        }

        private void ThucThiLuu(object p)
        {
            MessageBox.Show("Lưu thành công!");
        }

        private void ThucThiHuy(object p)
        {
            MaLop = string.Empty;
            SelectedMaKhoa = null;
        }

        void LoadData()
        {
            DS_Lop = new ObservableCollection<Lop>(db.Lops.ToList());
            OnPropertyChanged(nameof(DS_Lop));
        }
    }
}