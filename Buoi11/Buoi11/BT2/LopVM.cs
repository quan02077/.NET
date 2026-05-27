using Buoi11.Model;
using Buoi11.VM;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Buoi11.BT2
{
    public class LopVM : BaseVM
    {
        private QuanLySinhVienEntities1 db = new QuanLySinhVienEntities1();
        public ObservableCollection<Lop> DS_Lop { get; set; }
        public ObservableCollection<Khoa> DS_Khoa { get; set; }

        public LopInputViewModel NewLop { get; set; } = new LopInputViewModel();

        private Lop selectedLop;
        public Lop SelectedLop
        {
            get => selectedLop;
            set
            {
                selectedLop = value;
                OnPropertyChanged(nameof(SelectedLop));
                if (selectedLop != null && !isAdding && !isEditing)
                {
                    NewLop.MaLop = selectedLop.MaLop;
                    NewLop.MaKhoa = selectedLop.MaKhoa;
                }
            }
        }

        private bool _isMaLopEnabled = false;
        public bool IsMaLopEnabled
        {
            get => _isMaLopEnabled;
            set { _isMaLopEnabled = value; OnPropertyChanged(nameof(IsMaLopEnabled)); }
        }

        private bool isAdding = false;
        private bool isEditing = false;

        public ICommand ThemCmd { get; set; }
        public ICommand SuaCmd { get; set; }
        public ICommand XoaCmd { get; set; }
        public ICommand LuuCmd { get; set; }
        public ICommand HuyCmd { get; set; }

        public LopVM()
        {
            LoadData();

            ThemCmd = new RelayCommand<object>(ThucThiThem, DieuKienThem);
            SuaCmd = new RelayCommand<object>(ThucThiSua, DieuKienSua);
            XoaCmd = new RelayCommand<object>(ThucThiXoa, DieuKienXoa);
            LuuCmd = new RelayCommand<object>(ThucThiLuu, DieuKienLuu);
            HuyCmd = new RelayCommand<object>(ThucThiHuy, DieuKienHuy);
        }

        private bool DieuKienThem(object obj)
        {
            return !isAdding && !isEditing;
        }

        private bool DieuKienSua(object obj)
        {
            return SelectedLop != null && !isAdding && !isEditing;
        }

        private bool DieuKienXoa(object obj)
        {
            return SelectedLop != null && !isAdding && !isEditing;
        }

        private bool DieuKienLuu(object obj)
        {
            return isAdding || isEditing;
        }

        private bool DieuKienHuy(object obj)
        {
            return isAdding || isEditing;
        }

        void LoadData()
        {
            DS_Lop = new ObservableCollection<Lop>(db.Lops.ToList());
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoas.ToList());
            OnPropertyChanged(nameof(DS_Lop));
            OnPropertyChanged(nameof(DS_Khoa));
        }

        private void ThucThiThem(object obj)
        {
            isAdding = true;
            NewLop.MaLop = "";
            NewLop.MaKhoa = null;
            IsMaLopEnabled = true; 
            SelectedLop = null;
        }

        private void ThucThiSua(object obj)
        {
            isEditing = true;
            IsMaLopEnabled = false; 
        }

        private void ThucThiLuu(object obj)
        {
            if (!NewLop.IsValid)
            {
                MessageBox.Show("Dữ liệu không hợp lệ, vui lòng kiểm tra lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (isAdding)
            {
                if (db.Lops.Any(l => l.MaLop == NewLop.MaLop))
                {
                    MessageBox.Show("Mã lớp đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Lop lopMoi = new Lop { MaLop = NewLop.MaLop, MaKhoa = NewLop.MaKhoa };
                db.Lops.Add(lopMoi);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công!", "Thông báo");
            }
            else if (isEditing)
            {
                var lopSua = db.Lops.FirstOrDefault(l => l.MaLop == NewLop.MaLop);
                if (lopSua != null)
                {
                    lopSua.MaKhoa = NewLop.MaKhoa;
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                }
            }

            isAdding = false;
            isEditing = false;
            IsMaLopEnabled = false;
            LoadData();
        }

        private void ThucThiHuy(object obj)
        {
            isAdding = false;
            isEditing = false;
            IsMaLopEnabled = false;
            if (SelectedLop != null)
            {
                NewLop.MaLop = SelectedLop.MaLop;
                NewLop.MaKhoa = SelectedLop.MaKhoa;
            }
            else
            {
                NewLop.MaLop = "";
                NewLop.MaKhoa = null;
            }
        }

        private void ThucThiXoa(object obj)
        {
            var result = MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                db.Lops.Remove(SelectedLop);
                db.SaveChanges();
                LoadData();
                MessageBox.Show("Xóa thành công!", "Thông báo");
            }
        }
    }
}