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

namespace Buoi11.BT4
{
    public class DiemVM : BaseVM
    {
        private QuanLySinhVienEntities1 db = new QuanLySinhVienEntities1();
        public ObservableCollection<MonHoc> DS_MonHoc { get; set; }
        public List<string> DS_NamHoc { get; set; } = new List<string> { "2023-2024", "2024-2025" };
        public List<int> DS_HocKy { get; set; } = new List<int> { 1, 2, 3 };

        private ObservableCollection<KetQua> dsKetQua;
        public ObservableCollection<KetQua> DS_KetQua
        {
            get => dsKetQua;
            set { dsKetQua = value; OnPropertyChanged(nameof(DS_KetQua)); }
        }

        private string selectedMaMon;
        public string SelectedMaMon
        {
            get => selectedMaMon;
            set { selectedMaMon = value; OnPropertyChanged(nameof(SelectedMaMon)); }
        }

        private string selectedNamHoc;
        public string SelectedNamHoc
        {
            get => selectedNamHoc;
            set { selectedNamHoc = value; OnPropertyChanged(nameof(SelectedNamHoc)); }
        }

        private int? selectedHocKy;
        public int? SelectedHocKy
        {
            get => selectedHocKy;
            set { selectedHocKy = value; OnPropertyChanged(nameof(SelectedHocKy)); }
        }

        public ICommand TaiDanhSachCmd { get; set; }
        public ICommand LuuDiemCmd { get; set; }

        public DiemVM()
        {
            LoadData();
            TaiDanhSachCmd = new RelayCommand<object>(ThucThiTaiDanhSach, DieuKienTaiDanhSach);
            LuuDiemCmd = new RelayCommand<object>(ThucThiLuuDiem, DieuKienLuuDiem);
        }

        private void LoadData()
        {
            DS_MonHoc = new ObservableCollection<MonHoc>(db.MonHocs.ToList());
            OnPropertyChanged(nameof(DS_MonHoc));
        }

        private bool DieuKienTaiDanhSach(object p)
        {
            return !string.IsNullOrEmpty(SelectedMaMon) &&
                   !string.IsNullOrEmpty(SelectedNamHoc) &&
                   SelectedHocKy != null;
        }

        private void ThucThiTaiDanhSach(object p)
        {
            var dsSinhVien = db.SinhViens.ToList();
            var danhSachDiem = new ObservableCollection<KetQua>();

            foreach (var sv in dsSinhVien)
            {
                var kq = db.KetQuas.FirstOrDefault(k => k.MaSinhVien == sv.MaSinhVien &&
                                                        k.MaMonHoc == SelectedMaMon &&
                                                        k.NamHoc == SelectedNamHoc &&
                                                        k.HocKy == SelectedHocKy);

                if (kq == null)
                {
                    kq = new KetQua
                    {
                        MaSinhVien = sv.MaSinhVien,
                        SinhVien = sv,
                        MaMonHoc = SelectedMaMon,
                        NamHoc = SelectedNamHoc,
                        HocKy = SelectedHocKy.Value
                    };
                    db.KetQuas.Add(kq);
                }
                danhSachDiem.Add(kq);
            }

            DS_KetQua = danhSachDiem;
        }

        private bool DieuKienLuuDiem(object p)
        {
            return DS_KetQua != null && DS_KetQua.Count > 0;
        }

        private void ThucThiLuuDiem(object p)
        {
            try
            {
                db.SaveChanges();
                MessageBox.Show("Lưu điểm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }
    }
}
