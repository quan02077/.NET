using KTL2.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KTL2.ViewModels
{
    public class Bai2VM : BaseVM
    {
        private QL_Karaoke_KT2Entities db = new QL_Karaoke_KT2Entities();

        private ObservableCollection<LOAIPHONG> ds_Tang;
        public ObservableCollection<LOAIPHONG> Ds_Tang
        {
            get => ds_Tang;
            set
            {
                ds_Tang = value;
                OnPropertyChanged(nameof(Ds_Tang));
            }
        }

        private ObservableCollection<PHONG> ds_PhongTimKiem;
        public ObservableCollection<PHONG> Ds_PhongTimKiem
        {
            get => ds_PhongTimKiem;
            set
            {
                ds_PhongTimKiem = value;
                OnPropertyChanged(nameof(Ds_PhongTimKiem));
            }
        }

        private string selectedMaNhom;
        public string SelectedMaNhom
        {
            get => selectedMaNhom;
            set
            {
                selectedMaNhom = value;
                OnPropertyChanged(nameof(SelectedMaNhom));
            }
        }

        private int sucChua;
        public int SucChua
        {
            get => sucChua;
            set
            {
                sucChua = value;
                OnPropertyChanged(nameof(SucChua));
            }
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
                    TenPhong = selectedPhong.TenPhong;
                    SucChuaPhong = selectedPhong.SucChua.ToString();
                    GiaPhong = selectedPhong.GiaPhong.ToString();

                    if (selectedPhong.KieuPhong == 1)
                    {
                        KieuPhong = "Phòng quạt";
                    }
                    else
                    {
                        KieuPhong = "Phòng máy lạnh";
                    }

                    TinhTrang = KiemTraTinhTrangPhong(selectedPhong.MaPhong);
                }
            }
        }

        private string tenPhong;
        public string TenPhong
        {
            get => tenPhong;
            set
            {
                tenPhong = value;
                OnPropertyChanged(nameof(TenPhong));
            }
        }

        private string sucChuaPhong;
        public string SucChuaPhong
        {
            get => sucChuaPhong;
            set
            {
                sucChuaPhong = value;
                OnPropertyChanged(nameof(SucChuaPhong));
            }
        }

        private string giaPhong;
        public string GiaPhong
        {
            get => giaPhong;
            set
            {
                giaPhong = value;
                OnPropertyChanged(nameof(GiaPhong));
            }
        }

        private string kieuPhong;
        public string KieuPhong
        {
            get => kieuPhong;
            set
            {
                kieuPhong = value;
                OnPropertyChanged(nameof(KieuPhong));
            }
        }

        private string tinhTrang;
        public string TinhTrang
        {
            get => tinhTrang;
            set
            {
                tinhTrang = value;
                OnPropertyChanged(nameof(TinhTrang));
            }
        }

        public ICommand TimKiemCommand { get; set; }

        public Bai2VM()
        {
            TimKiemCommand = new RelayCommand<object>(TimKiem, CanTimKiem);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Ds_Tang = new ObservableCollection<LOAIPHONG>(db.LOAIPHONGs.ToList());
                Ds_PhongTimKiem = new ObservableCollection<PHONG>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private bool CanTimKiem(object p)
        {
            return true;
        }

        private void TimKiem(object p)
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedMaNhom))
                {
                    MessageBox.Show("Vui lòng chọn tầng!");
                    return;
                }

                if (SucChua <= 0)
                {
                    MessageBox.Show("Vui lòng nhập sức chứa!");
                    return;
                }

                var ketQua = db.PHONGs
                    .Where(p1 => p1.MaNhom == SelectedMaNhom && p1.SucChua >= SucChua)
                    .ToList();

                Ds_PhongTimKiem = new ObservableCollection<PHONG>(ketQua);

                if (ketQua.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy phòng phù hợp!");
                    XoaThongTinChiTiet();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private string KiemTraTinhTrangPhong(string maPhong)
        {
            DateTime hienTai = DateTime.Now;

            var datPhong = db.DATPHONGs.FirstOrDefault(dp =>
                dp.MaPh == maPhong &&
                dp.NgayDat <= hienTai &&
                dp.NgayTra >= hienTai
            );

            if (datPhong != null)
            {
                return "Khách đang nhận phòng";
            }

            return "Phòng trống";
        }

        private void XoaThongTinChiTiet()
        {
            TenPhong = "";
            SucChuaPhong = "";
            GiaPhong = "";
            KieuPhong = "";
            TinhTrang = "";
        }
    }
}