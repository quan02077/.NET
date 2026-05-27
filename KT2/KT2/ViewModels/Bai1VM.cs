using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KT2.Models;
using KT2.ViewModels;    

namespace KT2.ViewModels
{
    public class Bai1VM: BaseVM
    {
        private QL_Karaoke_KT2Entities db = new QL_Karaoke_KT2Entities();
        public ObservableCollection<PHONG> ds_Phong {  get; set; }
        public ObservableCollection<LOAIPHONG> ds_Tang {  get; set; }
        private string maPhong;
        public string MaPhong
        {
            get
            {
                return maPhong;
            }
            set
            {
                maPhong = value;
                OnPropertyChanged(nameof(MaPhong));
            }
        }
        private string tenPhong;
        public string TenPhong
        {
            get
            {
                return tenPhong;
            }
            set
            {
                tenPhong = value;
                OnPropertyChanged(nameof(TenPhong));
            }
        }
        private float giaPhong;
        public float GiaPhong
        {
            get
            {
                return giaPhong;
            }
            set
            {
                giaPhong = value;
                OnPropertyChanged(nameof(GiaPhong));
            }
        }
        private int sucChua;
        public int SucChua
        {
            get
            {
                return sucChua;
            }
            set
            {
                sucChua = value;
                OnPropertyChanged(nameof(SucChua));
            }
        }
        private string selectedMaNhom;
        public string SelectedMaNhom
        { 
            get => selectedMaNhom; 
            set 
            { selectedMaNhom = value;
                OnPropertyChanged(nameof(SelectedMaNhom)); 
            } 
        }

        private int loaiPhong = 1; 
        public int LoaiPhong
        {
            get => loaiPhong;
            set
            {
                loaiPhong = value;
                OnPropertyChanged(nameof(LoaiPhong));
                OnPropertyChanged(nameof(IsPhongQuat));
                OnPropertyChanged(nameof(IsPhongMayLanh));
            }
        }

        public bool IsPhongQuat
        {
            get => LoaiPhong == 1;
            set { if (value) LoaiPhong = 1; }
        }
        public bool IsPhongMayLanh
        {
            get => LoaiPhong == 2;
            set { if (value) LoaiPhong = 2; }
        }

        private PHONG selectedPhong;
        public PHONG SelectedPhong
        {
            get => selectedPhong;
            set
            {
                selectedPhong = value;
                OnPropertyChanged(nameof(SelectedPhong));
                if (selectedPhong != null)
                {
                    MaPhong = selectedPhong.MaPhong;
                    TenPhong = selectedPhong.TenPhong;
                    GiaPhong = (float)selectedPhong.GiaPhong;
                    SucChua = (int)selectedPhong.SucChua;
                    SelectedMaNhom = selectedPhong.MaNhom;
                    LoaiPhong = (int)selectedPhong.KieuPhong;
                }
            }
        }
        private bool isAdd = false;
        public bool IsAdd
        {
            get
            {
                return isAdd;
            }
            set
            {
                isAdd = value;
                OnPropertyChanged(nameof(IsAdd));
            }
        }
        private bool isEdit = false;
        public bool IsEdit
        {
            get
            {
                return isEdit;
            }
            set
            {
                isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public Bai1VM()
        {
            LoadData();
            AddCommand = new RelayCommand<object>(Add, CanAdd);
            EditCommand = new RelayCommand<object>(Edit, CanEdit);
            SaveCommand = new RelayCommand<object>(Save, CanSave);
            ClearCommand = new RelayCommand<object>(Clear, CanClear);
        }

        private bool CanAdd(object p)
        {
            return true;
        }
        private void Add(object p)
        {
            IsAdd = true;
            IsEdit = false;
        }
        private bool CanEdit(object p)
        {
            return SelectedPhong != null;
        }

        private void Edit(object p)
        {
            IsAdd = false;
            IsEdit = true;
            MessageBox.Show("Hãy sửa thông tin trên form và bấm nút Lưu!", "Thông báo");
        }
        private bool CanSave(object p)
        {
            return IsAdd || IsEdit;
        }

        private void Save(object p)
        {
            if (string.IsNullOrEmpty(MaPhong) || string.IsNullOrEmpty(TenPhong) || string.IsNullOrEmpty(SelectedMaNhom))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin mã phòng, tên phòng và tầng!");
                return;
            }
            if (IsAdd)
            {
                var checkExist = db.PHONG.FirstOrDefault(x => x.MaPhong == MaPhong);
                if (checkExist != null)
                {
                    MessageBox.Show("Mã phòng đã tồn tại!");
                    return;
                }

                PHONG newPhong = new PHONG()
                {
                    MaPhong = this.MaPhong,
                    TenPhong = this.TenPhong,
                    GiaPhong = this.GiaPhong,
                    SucChua = this.SucChua,
                    MaNhom = this.SelectedMaNhom,
                    KieuPhong = this.LoaiPhong
                };

                db.PHONG.Add(newPhong);
                db.SaveChanges();
                MessageBox.Show("Thêm phòng mới thành công!");
            }
            else if (IsEdit)
            {
                var phongSua = db.PHONG.FirstOrDefault(x => x.MaPhong == SelectedPhong.MaPhong);
                if (phongSua != null)
                {
                    phongSua.TenPhong = this.TenPhong;
                    phongSua.GiaPhong = this.GiaPhong;
                    phongSua.SucChua = this.SucChua;
                    phongSua.MaNhom = this.SelectedMaNhom;
                    phongSua.KieuPhong = this.LoaiPhong;

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!");
                }
            }

            LoadData();
            IsAdd = false;
            IsEdit = false;
        }

        private bool CanClear(object p)
        {
            return SelectedPhong != null;
        }

        private void Clear(object p)
        {
            var phongXoa = db.PHONG.FirstOrDefault(x => x.MaPhong == SelectedPhong.MaPhong);
            if (phongXoa != null)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này?", "Xác nhận", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    db.PHONG.Remove(phongXoa);
                    db.SaveChanges();
                    LoadData();
                    Add(null); 
                }
            }
        }

        void LoadData()
        {
            ds_Phong = new ObservableCollection<PHONG>(db.PHONG.ToList());
            OnPropertyChanged(nameof(ds_Phong));
            ds_Tang = new ObservableCollection<LOAIPHONG>(db.LOAIPHONG.ToList());
            OnPropertyChanged(nameof(ds_Tang));
        }
    }
}
