using Buoi09.BT1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Buoi09.BT5.ViewModel
{
    public class MonHocVM : BaseVM
    {
        private QuanLySinhVienEntities db = new QuanLySinhVienEntities();
        public ObservableCollection<MonHoc> DS_MonHoc { get; set; }
        public List<string> DS_TinhChat { get; set; } = new List<string> { "Bắt buộc", "Tự chọn" };

        private MonHoc selectedMonHoc;
        public MonHoc SelectedMonHoc
        {
            get => selectedMonHoc;
            set
            {
                selectedMonHoc = value;
                OnPropertyChanged(nameof(SelectedMonHoc));
                if (selectedMonHoc != null)
                {
                    MaMon = selectedMonHoc.MaMonHoc;
                    TenMH = selectedMonHoc.TenMonHoc;
                    SoTC = selectedMonHoc.SoTC;
                    TinhChat = selectedMonHoc.TinhChat;
                }
            }
        }

        private string maMon;
        public string MaMon
        {
            get => maMon;
            set { maMon = value; OnPropertyChanged(nameof(MaMon)); }
        }

        private string tenMH;
        public string TenMH
        {
            get => tenMH;
            set { tenMH = value; OnPropertyChanged(nameof(TenMH)); }
        }

        private int? soTC;
        public int? SoTC
        {
            get => soTC;
            set { soTC = value; OnPropertyChanged(nameof(SoTC)); }
        }

        private string tinhChat;
        public string TinhChat
        {
            get => tinhChat;
            set { tinhChat = value; OnPropertyChanged(nameof(TinhChat)); }
        }

        public ICommand ThemCmd { get; set; }
        public ICommand SuaCmd { get; set; }
        public ICommand XoaCmd { get; set; }
        public ICommand LuuCmd { get; set; }
        public ICommand HuyCmd { get; set; }

        public MonHocVM()
        {
            LoadData();
            ThemCmd = new RelayCommand<object>(ThucThiThem, DieuKienThem);
            SuaCmd = new RelayCommand<object>(ThucThiSua, DieuKienSuaXoa);
            XoaCmd = new RelayCommand<object>(ThucThiXoa, DieuKienSuaXoa);
            LuuCmd = new RelayCommand<object>(ThucThiLuu, DieuKienLuu);
            HuyCmd = new RelayCommand<object>(ThucThiHuy, DieuKienHuy);
        }
        private bool DieuKienThem(object p) { return true; }
        private bool DieuKienSuaXoa(object p) { return SelectedMonHoc != null; }
        private bool DieuKienLuu(object p) { return true; }
        private bool DieuKienHuy(object p) { return true; }

        private void ThucThiThem(object p)
        {
            if (string.IsNullOrWhiteSpace(MaMon) || string.IsNullOrWhiteSpace(TenMH) || string.IsNullOrWhiteSpace(TinhChat) || SoTC == null) return;
            var newMH = new MonHoc
            {
                MaMonHoc = MaMon,
                TenMonHoc = TenMH,
                SoTC = SoTC,
                TinhChat = TinhChat,
            };
            db.MonHocs.Add(newMH);
            db.SaveChanges();
            LoadData();
            MaMon = TenMH = TinhChat = string.Empty;
            SoTC = null;
        }
        private void ThucThiSua(object p)
        {
            if(SelectedMonHoc == null) return;
            var mh = db.MonHocs.Find(SelectedMonHoc.MaMonHoc);
            if (mh != null)
            {
                mh.MaMonHoc = MaMon;
                mh.TenMonHoc = TenMH;
                mh.SoTC = SoTC;
                mh.TinhChat = TinhChat;
                db.SaveChanges();
                LoadData() ;
                MaMon = TenMH = TinhChat = string.Empty;
                SoTC = null;
            }
        }
        private void ThucThiXoa(object p)
        {
            if (SelectedMonHoc == null) return;
            var mh = db.MonHocs.Find(SelectedMonHoc.MaMonHoc);
            if (mh != null)
            {
                db.MonHocs.Remove(mh);
                db.SaveChanges();
                LoadData();
                MaMon = TenMH = TinhChat = string.Empty;
                SoTC = null;
            }
        }
        private void ThucThiLuu(object p)
        {
            MessageBox.Show("Lưu thành công!");
            db.SaveChanges();
            LoadData();
            MaMon = TenMH = TinhChat = string.Empty;
            SoTC = null;
        }
        private void ThucThiHuy(object p)
        {
            MessageBox.Show("Hủy thành công!");
            db.Dispose();
            db = new QuanLySinhVienEntities();
            LoadData();
            MaMon = TenMH = TinhChat = string.Empty;
            SoTC = null;
        }
        private void LoadData()
        {
            DS_MonHoc = new ObservableCollection<MonHoc>(db.MonHocs.ToList());
            OnPropertyChanged(nameof(DS_MonHoc));
        }
    }
}