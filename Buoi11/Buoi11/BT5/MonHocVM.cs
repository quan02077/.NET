using Buoi11.Model;
using Buoi11.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Buoi11.BT5
{
    public class MonHocVM : BaseVM, IDataErrorInfo
    {
        private QuanLySinhVienEntities1 db = new QuanLySinhVienEntities1();
        private ObservableCollection<MonHoc> _dsMonHoc;
        public ObservableCollection<MonHoc> DS_MonHoc
        {
            get => _dsMonHoc;
            set { _dsMonHoc = value; OnPropertyChanged(nameof(DS_MonHoc)); }
        }

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

        #region Validation (IDataErrorInfo)
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(MaMon))
                {
                    if (string.IsNullOrWhiteSpace(MaMon))
                        return "Mã môn không được để trống";
                    if (MaMon.Length > 20)
                        return "Mã môn không vượt quá 20 ký tự";
                }
                if (columnName == nameof(TenMH))
                {
                    if (string.IsNullOrWhiteSpace(TenMH))
                        return "Tên môn học không được để trống";
                    if (TenMH.Length > 100) 
                        return "Tên môn học không vượt quá 100 ký tự";
                }
                if (columnName == nameof(SoTC))
                {
                    if (!SoTC.HasValue)
                        return "Số tín chỉ không được để trống";
                    if (SoTC.Value <= 0)
                        return "Số tín chỉ phải là số nguyên dương";
                    if (SoTC.Value > 10) 
                        return "Giá trị số tín chỉ không hợp lệ (Vượt quá quy định)";
                }
                return null;
            }
        }
        #endregion

        private bool DieuKienThem(object p)
        {
            return !string.IsNullOrWhiteSpace(MaMon)
                && !string.IsNullOrWhiteSpace(TenMH)
                && !string.IsNullOrWhiteSpace(TinhChat)
                && SoTC.HasValue && SoTC.Value > 0 && SoTC.Value <= 10;
        }

        private bool DieuKienSuaXoa(object p) { return SelectedMonHoc != null; }
        private bool DieuKienLuu(object p) { return true; }
        private bool DieuKienHuy(object p) { return true; }

        private void ThucThiThem(object p)
        {
            if (db.MonHocs.Any(m => m.MaMonHoc == MaMon))
            {
                MessageBox.Show("Mã môn học đã tồn tại trong hệ thống! Vui lòng nhập mã khác.", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }
            if (db.MonHocs.Any(m => m.TenMonHoc == TenMH))
            {
                MessageBox.Show("Tên môn học đã tồn tại (Tên là duy nhất)! Vui lòng đặt tên khác.", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var newMH = new MonHoc
            {
                MaMonHoc = MaMon,
                TenMonHoc = TenMH,
                SoTC = SoTC,
                TinhChat = TinhChat,
            };

            db.MonHocs.Add(newMH);
            db.SaveChanges();
            MessageBox.Show("Thêm môn học thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            LoadData();
            ClearForm();
        }

        private void ThucThiSua(object p)
        {
            if (SelectedMonHoc == null) return;
            if (db.MonHocs.Any(m => m.TenMonHoc == TenMH && m.MaMonHoc != SelectedMonHoc.MaMonHoc))
            {
                MessageBox.Show("Tên môn học này đã được sử dụng cho một môn khác! Vui lòng nhập tên khác.", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var mh = db.MonHocs.Find(SelectedMonHoc.MaMonHoc);
            if (mh != null)
            {
                mh.TenMonHoc = TenMH;
                mh.SoTC = SoTC;
                mh.TinhChat = TinhChat;

                db.SaveChanges();
                MessageBox.Show("Cập nhật môn học thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadData();
                ClearForm();
            }
        }

        private void ThucThiXoa(object p)
        {
            if (SelectedMonHoc == null) return;

            var rs = MessageBox.Show("Bạn có chắc chắn muốn xóa môn này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rs != MessageBoxResult.Yes) return;

            var mh = db.MonHocs.Find(SelectedMonHoc.MaMonHoc);
            if (mh != null)
            {
                db.MonHocs.Remove(mh);
                db.SaveChanges();
                LoadData();
                ClearForm();
            }
        }

        private void ThucThiLuu(object p)
        {
            MessageBox.Show("Lưu thành công!");
            db.SaveChanges();
            LoadData();
            ClearForm();
        }

        private void ThucThiHuy(object p)
        {
            MessageBox.Show("Hủy thành công!");
            db = new QuanLySinhVienEntities1();
            LoadData();
            ClearForm();
        }

        private void LoadData()
        {
            DS_MonHoc = new ObservableCollection<MonHoc>(db.MonHocs.ToList());
        }
        private void ClearForm()
        {
            MaMon = string.Empty;
            TenMH = string.Empty;
            TinhChat = string.Empty;
            SoTC = null;
        }
    }
}