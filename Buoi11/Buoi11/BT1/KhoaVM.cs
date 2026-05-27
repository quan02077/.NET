using Buoi11.Model;
using Buoi11.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Buoi11.BT1
{
    public class KhoaVM:BaseVM  
    {
        private QuanLySinhVienEntities1 db = new QuanLySinhVienEntities1();
        public ObservableCollection<Khoa> DS_Khoa { get; set; }
        private Khoa selectedKhoa;
        public Khoa SelectedKhoa
        {
            get => selectedKhoa;
            set
            {
                selectedKhoa = value;
                OnPropertyChanged(nameof(SelectedKhoa));
                if (selectedKhoa != null)
                {
                    NewKhoa.MaKhoa = selectedKhoa.MaKhoa;
                    NewKhoa.TenKhoa = selectedKhoa.TenKhoa;
                    NewKhoa.IsEdit = true;
                }
            }
        }
        public KhoaInputViewModel NewKhoa { get; set; } = new KhoaInputViewModel();
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
            return true;
        }
        private bool DieuKienSua(object obj)
        {
           return SelectedKhoa != null;
        }
        private bool DieuKienXoa(object obj)
        {
            return SelectedKhoa != null;
        }
        private void ThucThiThem(object obj)
        {
            if (!NewKhoa.IsValid)
            {
                MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool isDuplicate = db.Khoas.Any(k => k.MaKhoa == NewKhoa.MaKhoa);
            if (isDuplicate)
            {
                MessageBox.Show("Mã khoa đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Khoa khoaMoi = new Khoa()
            {
                MaKhoa = NewKhoa.MaKhoa,
                TenKhoa = NewKhoa.TenKhoa
            };
            db.Khoas.Add(khoaMoi);
            db.SaveChanges(); 
            DS_Khoa.Add(khoaMoi); 

            MessageBox.Show("Thêm khoa thành công!", "Thông báo");
        }
        private void ThucThiSua(object obj)
        {
            if(SelectedKhoa == null)
            {
                MessageBox.Show("Vui lòng chọn khoa để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!NewKhoa.IsValid)
            {
                MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var khoa = db.Khoas.Find(SelectedKhoa.MaKhoa);

            if (khoa != null)
            {
                SelectedKhoa.TenKhoa = NewKhoa.TenKhoa;
                db.SaveChanges();
                LoadData();
                MessageBox.Show("Cập nhật thành công!");
            }
        }
        private void ThucThiXoa(object obj)
        {
            if (SelectedKhoa == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn khoa để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var khoa = db.Khoas.Find(SelectedKhoa.MaKhoa);

            if (khoa != null)
            {
                db.Khoas.Remove(khoa);
                db.SaveChanges();
                DS_Khoa.Remove(SelectedKhoa);
                MessageBox.Show("Xóa thành công!");
            }
        }
        void LoadData()
        {
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoas.ToList());
            OnPropertyChanged(nameof(DS_Khoa));
        }
    }
}
