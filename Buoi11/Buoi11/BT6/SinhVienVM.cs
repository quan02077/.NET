using Buoi11.Model;
using Buoi11.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace Buoi11.BT6
{
    public class SinhVienVM: BaseVM, IDataErrorInfo
    {
        private QuanLySinhVienEntities1 db = new QuanLySinhVienEntities1();
        private ObservableCollection<SinhVien> _dsSinhVien;
        public ObservableCollection<SinhVien> DS_SinhVien
        {
            get => _dsSinhVien;
            set { _dsSinhVien = value; OnPropertyChanged(nameof(DS_SinhVien)); }
        }
        private ObservableCollection<Lop> _dsLop;
        public ObservableCollection<Lop> DS_Lop
        {
            get => _dsLop;
            set { _dsLop = value; OnPropertyChanged(nameof(DS_Lop)); }
        }
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
                    SelectedLop = selectedSinhVien.Lop;
                }
            }
        }
        private Lop selectedLop;
        public Lop SelectedLop
        {
            get => selectedLop;
            set { selectedLop = value; OnPropertyChanged(nameof(SelectedLop)); }
        }
        private string maSV;
        public string MaSV
        {
            get { return maSV; } 
            set { maSV = value; OnPropertyChanged(nameof(MaSV)); }
        }
        private string tenSV;
        public string TenSV
        {
            get { return tenSV; }
            set { tenSV = value; OnPropertyChanged(nameof(TenSV)); }
        }
        private string gioiTinh;
        public string GioiTinh
        {
            get => gioiTinh;
            set
            {
                gioiTinh = value;
                OnPropertyChanged(nameof(GioiTinh));
                OnPropertyChanged(nameof(IsNam));
                OnPropertyChanged(nameof(IsNu));
            }
        }
        public bool IsNam
        {
            get => GioiTinh == "Nam";
            set { if (value) GioiTinh = "Nam"; }
        }

        public bool IsNu
        {
            get => GioiTinh == "Nữ";
            set { if (value) GioiTinh = "Nữ"; }
        }
        private DateTime? ngaySinh;
        public DateTime? NgaySinh
        {
            get => ngaySinh;
            set { ngaySinh = value; OnPropertyChanged(nameof(NgaySinh)); }
        }
        private bool _isMaSVEnabled = false;
        public bool IsMaSVEnabled
        {
            get => _isMaSVEnabled;
            set { _isMaSVEnabled = value; OnPropertyChanged(nameof(IsMaSVEnabled)); }
        }
        private bool isAdding = false;
        private bool isEditing = false;

        public ICommand ThemCmd { get; set; }
        public ICommand SuaCmd { get; set; }
        public ICommand LuuCmd { get; set; }

        public SinhVienVM()
        {
            LoadData();
            ThemCmd = new RelayCommand<object>(ThucThiThem, DieuKienThem);
            SuaCmd = new RelayCommand<object>(ThucThiSua, DieuKienSua);
            LuuCmd = new RelayCommand<object>(ThucThiLuu, DieuKienLuu);
        }

        #region Validation (IDataErrorInfo)
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(MaSV))
                {
                    if (string.IsNullOrEmpty(MaSV))
                        return "Mã sinh viên không được để trống";
                    if (MaSV.Length > 10)
                        return "Mã sinh viên không được vượt quá 10 ký tự";
                }
                if (columnName == nameof(TenSV))
                {
                    if (string.IsNullOrEmpty(TenSV))
                        return "Tên sinh viên không được để trống";
                    if (TenSV.Length > 50)
                        return "Tên sinh viên không được vượt quá 50 ký tự";
                }
                if (columnName == nameof(GioiTinh))
                {
                    if (string.IsNullOrEmpty(GioiTinh))
                        return "Giới tính không được để trống";
                }
                if (columnName == nameof(NgaySinh))
                {
                    if (NgaySinh == null)
                        return "Ngày sinh không được để trống";
                    if (NgaySinh.Value.AddYears(18) > DateTime.Now)
                        return "Sinh viên phải từ 18 tuổi trở lên";
                }    
                if (columnName == nameof(SelectedLop))
                {
                    if (SelectedLop == null)
                        return "Lớp không được để trống";
                }    
                return null;
            }
        }
        #endregion
        private bool DieuKienThem(object obj)
        {
            return !isAdding && !isEditing;
        }
        private bool DieuKienSua(object obj)
        {
            return SelectedLop != null && !isAdding && !isEditing;
        }
        private bool DieuKienLuu(object obj)
        {
            return isAdding || isEditing;
        }
        private void ThucThiThem(object p)
        {
            isAdding = true;
            MaSV = "";
            TenSV = "";
            GioiTinh = "";
            NgaySinh = null;
            SelectedLop = null;
            IsMaSVEnabled = true;
        }
        private void ThucThiSua(object p)
        {
            isEditing = true;
            IsMaSVEnabled = false; 
        }
        private void ThucThiLuu(object p)
        {
            if (isAdding)
            {
                if (SelectedLop == null)
                {
                    MessageBox.Show("Vui lòng chọn Lớp cho sinh viên!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; 
                }
                if (db.SinhViens.Any(sv => sv.MaSinhVien == MaSV))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }    
                SinhVien svmoi = new SinhVien
                {
                    MaSinhVien = MaSV,
                    HoTen = TenSV,
                    GioiTinh = GioiTinh,
                    NgaySinh = NgaySinh,
                    MaLop = SelectedLop.MaLop
                };
                db.SinhViens.Add(svmoi);
                db.SaveChanges();
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var sv = db.SinhViens.Find(SelectedSinhVien.MaSinhVien);
                if (sv != null)
                {
                    sv.HoTen = TenSV;
                    sv.GioiTinh = GioiTinh;
                    sv.NgaySinh = NgaySinh;
                    sv.MaLop = SelectedLop.MaLop;
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            isAdding = false;
            isEditing = false;
            IsMaSVEnabled = false;
            LoadData();
        }
        private void ThucThiHuy(object p)
        {
            isAdding = false;
            isEditing = false;
            IsMaSVEnabled = false;
            if (SelectedSinhVien != null)
            {
                MaSV = SelectedSinhVien.MaSinhVien;
                TenSV = SelectedSinhVien.HoTen;
                GioiTinh = SelectedSinhVien.GioiTinh;
                NgaySinh = SelectedSinhVien.NgaySinh;
                SelectedLop = SelectedSinhVien.Lop;
            }
            else
            {
                MaSV = "";
                TenSV = "";
                GioiTinh = "";
                NgaySinh = null;
                SelectedLop = null;
            }    
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
