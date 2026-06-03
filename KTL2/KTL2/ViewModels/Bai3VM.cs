using KTL2.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KTL2.ViewModels
{
    public class Bai3VM : BaseVM
    {
        private QL_Karaoke_KT2Entities db = new QL_Karaoke_KT2Entities();

        public ObservableCollection<PHONG> Ds_Phong { get; set; }
        public ObservableCollection<KHACHHANG> Ds_KhachHang { get; set; }
        public ObservableCollection<PHUTHU> Ds_PhuThu { get; set; }

        private ObservableCollection<ChiTietPhuThuDisplay> ds_ChiTietPhuThu;
        public ObservableCollection<ChiTietPhuThuDisplay> Ds_ChiTietPhuThu
        {
            get => ds_ChiTietPhuThu;
            set
            {
                ds_ChiTietPhuThu = value;
                OnPropertyChanged(nameof(Ds_ChiTietPhuThu));
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
                    GiaPhong = selectedPhong.GiaPhong.ToString();
                    SucChua = selectedPhong.SucChua.ToString();
                    TinhTongTien();
                }
            }
        }

        private KHACHHANG selectedKhachHang;
        public KHACHHANG SelectedKhachHang
        {
            get => selectedKhachHang;
            set
            {
                selectedKhachHang = value;
                OnPropertyChanged(nameof(SelectedKhachHang));

                if (selectedKhachHang != null)
                {
                    SoDienThoai = selectedKhachHang.SoDT;
                }
            }
        }

        private PHUTHU selectedPhuThu;
        public PHUTHU SelectedPhuThu
        {
            get => selectedPhuThu;
            set
            {
                selectedPhuThu = value;
                OnPropertyChanged(nameof(SelectedPhuThu));

                if (selectedPhuThu != null)
                {
                    GiaPhuThu = selectedPhuThu.GiaPT.ToString();
                }
            }
        }

        private DateTime? ngayDat = DateTime.Now;
        public DateTime? NgayDat
        {
            get => ngayDat;
            set
            {
                ngayDat = value;
                OnPropertyChanged(nameof(NgayDat));
            }
        }

        private string gioVao;
        public string GioVao
        {
            get => gioVao;
            set
            {
                gioVao = value;
                OnPropertyChanged(nameof(GioVao));
                TinhTongTien();
            }
        }

        private string gioRa;
        public string GioRa
        {
            get => gioRa;
            set
            {
                gioRa = value;
                OnPropertyChanged(nameof(GioRa));
                TinhTongTien();
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

        private string sucChua;
        public string SucChua
        {
            get => sucChua;
            set
            {
                sucChua = value;
                OnPropertyChanged(nameof(SucChua));
            }
        }

        private string soDienThoai;
        public string SoDienThoai
        {
            get => soDienThoai;
            set
            {
                soDienThoai = value;
                OnPropertyChanged(nameof(SoDienThoai));
            }
        }

        private int soLuong = 1;
        public int SoLuong
        {
            get => soLuong;
            set
            {
                soLuong = value;
                OnPropertyChanged(nameof(SoLuong));
            }
        }

        private string giaPhuThu;
        public string GiaPhuThu
        {
            get => giaPhuThu;
            set
            {
                giaPhuThu = value;
                OnPropertyChanged(nameof(GiaPhuThu));
            }
        }

        private string tongTien;
        public string TongTien
        {
            get => tongTien;
            set
            {
                tongTien = value;
                OnPropertyChanged(nameof(TongTien));
            }
        }

        public ICommand ThemPhuThuCommand { get; set; }
        public ICommand DatPhongCommand { get; set; }

        public Bai3VM()
        {
            ThemPhuThuCommand = new RelayCommand<object>(ThemPhuThu, CanThemPhuThu);
            DatPhongCommand = new RelayCommand<object>(DatPhong, CanDatPhong);

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Ds_Phong = new ObservableCollection<PHONG>(db.PHONGs.ToList());
                OnPropertyChanged(nameof(Ds_Phong));

                Ds_KhachHang = new ObservableCollection<KHACHHANG>(db.KHACHHANGs.ToList());
                OnPropertyChanged(nameof(Ds_KhachHang));

                Ds_PhuThu = new ObservableCollection<PHUTHU>(db.PHUTHUs.ToList());
                OnPropertyChanged(nameof(Ds_PhuThu));

                Ds_ChiTietPhuThu = new ObservableCollection<ChiTietPhuThuDisplay>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private bool CanThemPhuThu(object p)
        {
            return true;
        }

        private void ThemPhuThu(object p)
        {
            if (SelectedPhuThu == null)
            {
                MessageBox.Show("Vui lòng chọn phụ thu!");
                return;
            }

            if (SoLuong <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!");
                return;
            }

            var check = Ds_ChiTietPhuThu.FirstOrDefault(x => x.MaPT == SelectedPhuThu.MaPhuThu);

            if (check != null)
            {
                check.SL += SoLuong;
                check.ThanhTien = check.SL * check.GiaPT;

                Ds_ChiTietPhuThu = new ObservableCollection<ChiTietPhuThuDisplay>(Ds_ChiTietPhuThu);
            }
            else
            {
                ChiTietPhuThuDisplay ct = new ChiTietPhuThuDisplay()
                {
                    MaPT = SelectedPhuThu.MaPhuThu,
                    TenPhuThu = SelectedPhuThu.TenPhuThu,
                    GiaPT = Convert.ToDouble(SelectedPhuThu.GiaPT),
                    SL = SoLuong,
                    ThanhTien = Convert.ToDouble(SelectedPhuThu.GiaPT) * SoLuong
                };

                Ds_ChiTietPhuThu.Add(ct);
            }

            TinhTongTien();
        }

        private bool CanDatPhong(object p)
        {
            return true;
        }

        private void DatPhong(object p)
        {
            try
            {
                if (SelectedPhong == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng!");
                    return;
                }

                if (SelectedKhachHang == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!");
                    return;
                }

                if (NgayDat == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày đặt!");
                    return;
                }

                if (!TimeSpan.TryParse(GioVao, out TimeSpan timeVao))
                {
                    MessageBox.Show("Giờ vào không hợp lệ! Ví dụ đúng: 13:00");
                    return;
                }

                if (!TimeSpan.TryParse(GioRa, out TimeSpan timeRa))
                {
                    MessageBox.Show("Giờ ra không hợp lệ! Ví dụ đúng: 15:00");
                    return;
                }

                DateTime ngay = NgayDat.Value.Date;
                DateTime ngayGioVao = ngay.Add(timeVao);
                DateTime ngayGioRa = ngay.Add(timeRa);

                if (ngayGioRa <= ngayGioVao)
                {
                    MessageBox.Show("Giờ ra phải lớn hơn giờ vào!");
                    return;
                }

                DATPHONG dp = new DATPHONG()
                {
                    MaPh = SelectedPhong.MaPhong,
                    MaKH = SelectedKhachHang.MaKhachHang,
                    NgayDat = ngayGioVao,
                    NgayTra = ngayGioRa
                };

                db.DATPHONGs.Add(dp);
                db.SaveChanges();

                foreach (var item in Ds_ChiTietPhuThu)
                {
                    CHITIETDATPHONG ct = new CHITIETDATPHONG()
                    {
                        MaCT = TaoMaChiTiet(),
                        MaDP = dp.MaDatPhong,
                        MaPT = item.MaPT,
                        SL = item.SL
                    };

                    db.CHITIETDATPHONGs.Add(ct);
                    db.SaveChanges();
                }

                MessageBox.Show("Đặt phòng thành công!");

                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đặt phòng: " + ex.Message);
            }
        }

        private void TinhTongTien()
        {
            double tienPhong = 0;
            double tienPhuThu = 0;

            if (SelectedPhong != null &&
                TimeSpan.TryParse(GioVao, out TimeSpan timeVao) &&
                TimeSpan.TryParse(GioRa, out TimeSpan timeRa))
            {
                double soGio = (timeRa - timeVao).TotalHours;

                if (soGio > 0)
                {
                    tienPhong = soGio * Convert.ToDouble(SelectedPhong.GiaPhong);
                }
            }

            if (Ds_ChiTietPhuThu != null)
            {
                tienPhuThu = Ds_ChiTietPhuThu.Sum(x => x.ThanhTien);
            }

            double tong = tienPhong + tienPhuThu;
            TongTien = tong.ToString("N0");
        }

        private string TaoMaChiTiet()
        {
            int soLuongCT = db.CHITIETDATPHONGs.Count() + 1;
            return "CT" + soLuongCT.ToString("00");
        }

        private void LamMoi()
        {
            SelectedPhong = null;
            SelectedKhachHang = null;
            SelectedPhuThu = null;

            GiaPhong = "";
            SucChua = "";
            SoDienThoai = "";
            GiaPhuThu = "";
            GioVao = "";
            GioRa = "";
            SoLuong = 1;
            TongTien = "";

            Ds_ChiTietPhuThu.Clear();
        }
    }

    public class ChiTietPhuThuDisplay
    {
        public string MaPT { get; set; }
        public string TenPhuThu { get; set; }
        public double GiaPT { get; set; }
        public int SL { get; set; }
        public double ThanhTien { get; set; }
    }
}