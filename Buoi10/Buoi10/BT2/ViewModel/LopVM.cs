using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buoi10.BT1.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace Buoi10.BT2.ViewModel
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

        private string _selectedMaKhoa;
        public string SelectedMaKhoa
        {
            get => _selectedMaKhoa;
            set { _selectedMaKhoa = value; OnPropertyChanged(nameof(SelectedMaKhoa)); }
        }

        private Lop _selectedLop;
        public Lop SelectedLop
        {
            get => _selectedLop;
            set
            {
                _selectedLop = value;
                OnPropertyChanged(nameof(SelectedLop));
                if (_selectedLop != null)
                {
                    MaLop = _selectedLop.MaLop;
                    SelectedMaKhoa = _selectedLop.MaKhoa;
                }
            }
        }
        private bool _isMaLopEnabled = true;
        public bool IsMaLopEnabled
        {
            get => _isMaLopEnabled;
            set { _isMaLopEnabled = value; OnPropertyChanged(nameof(IsMaLopEnabled)); }
        }

        public ICommand ThemCmd { get; set; }
        public ICommand SuaCmd { get; set; }
        public ICommand XoaCmd { get; set; }
        public ICommand LuuCmd { get; set; }
        public ICommand HuyCmd { get; set; }

        private bool isAdding = false;
        private bool isEditing = false;

        public LopVM()
        {
            LoadData();

            ThemCmd = new RelayCommand<object>(ThucThiThem, DieuKienThem);
            SuaCmd = new RelayCommand<object>(ThucThiSua, DieuKienSua);
            XoaCmd = new RelayCommand<object>(ThucThiXoa, DieuKienXoa);
            LuuCmd = new RelayCommand<object>(ThucThiLuu, DieuKienLuu);
            HuyCmd = new RelayCommand<object>(ThucThiHuy, DieuKienHuy);
        }

        private void LoadData()
        {
            DS_Lop = new ObservableCollection<Lop>(db.Lops.ToList());
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoas.ToList());
            OnPropertyChanged(nameof(DS_Lop));
            OnPropertyChanged(nameof(DS_Khoa));
        }

        private bool DieuKienThem(object p) { return !isAdding && !isEditing; }
        private void ThucThiThem(object p)
        {
            MaLop = string.Empty;
            SelectedMaKhoa = null;
            SelectedLop = null;

            isAdding = true;        
            IsMaLopEnabled = true;  
        }

        private bool DieuKienSua(object p) { return SelectedLop != null && !isAdding && !isEditing; }
        private void ThucThiSua(object p)
        {
            isEditing = true;       
            IsMaLopEnabled = false;  
        }

        private bool DieuKienLuu(object p) { return isAdding || isEditing; }
        private void ThucThiLuu(object p)
        {
            if (string.IsNullOrWhiteSpace(MaLop) || string.IsNullOrWhiteSpace(SelectedMaKhoa))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }

            if (isAdding)
            {
                var checkTonTai = db.Lops.Find(MaLop);
                if (checkTonTai != null)
                {
                    MessageBox.Show("Mã lớp đã tồn tại!");
                    return;
                }
                var lopMoi = new Lop { MaLop = MaLop, MaKhoa = SelectedMaKhoa };
                db.Lops.Add(lopMoi);
                MessageBox.Show("Thêm mới thành công!");
            }
            else if (isEditing)
            {
                var lopSua = db.Lops.Find(MaLop);
                if (lopSua != null)
                {
                    lopSua.MaKhoa = SelectedMaKhoa;
                    MessageBox.Show("Cập nhật thành công!");
                }
            }

            db.SaveChanges();
            LoadData();

            isAdding = false;
            isEditing = false;
            IsMaLopEnabled = true;
        }
        private bool DieuKienHuy(object p) { return isAdding || isEditing; }
        private void ThucThiHuy(object p)
        {
            isAdding = false;
            isEditing = false;
            IsMaLopEnabled = true;
            MaLop = string.Empty;
            SelectedMaKhoa = null;
            SelectedLop = null;
        }
        private bool DieuKienXoa(object p) { return SelectedLop != null && !isAdding && !isEditing; }
        private void ThucThiXoa(object p)
        {
            MessageBoxResult result = MessageBox.Show("Em có chắc chắn muốn xóa lớp này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var lopXoa = db.Lops.Find(SelectedLop.MaLop);
                if (lopXoa != null)
                {
                    db.Lops.Remove(lopXoa);
                    db.SaveChanges();
                    LoadData();
                    ThucThiHuy(null);
                }
            }
        }
    }
}