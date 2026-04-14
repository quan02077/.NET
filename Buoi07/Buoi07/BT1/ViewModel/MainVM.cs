using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Buoi07.BT1.Model;
using System.Windows.Input;
using System.Linq;
using System.IO.Packaging;
using Buoi07.BT1.ViewModel;
using System.Windows;

namespace Buoi07.BT1.ViewModel
{
    public class MainVM: BaseVM
    {
        private ObservableCollection<KhachHangModel> customers;
        public ObservableCollection<KhachHangModel> Customers
        {
            get => customers;
            set
            {
                customers = value;
                OnPropertyChanged("Customers");
            }
        }
        private string newName;
        public string NewName
        {
            get => newName;
            set
            {
                newName = value;
                OnPropertyChanged("NewName");
            }
        }
        private string sdt;
        public string SDT
        {
            get => sdt;
            set
            {
                sdt= value;
                OnPropertyChanged("SDT");
            }
        }
        private bool isSinhVien;
        public bool IsSinhVien
        {
            get => isSinhVien;
            set
            {
                isSinhVien= value;
                OnPropertyChanged("IsSinhVien");
            }
        }

        private string thongTinChiTiet;
        public string ThongTinChiTiet
        {
            get => thongTinChiTiet;
            set
            {
                thongTinChiTiet = value;
                OnPropertyChanged("ThongTinChiTiet");
            }
        }

        private bool ban01 = true;
        public bool Ban01 { get => ban01; set { ban01 = value; OnPropertyChanged("Ban01"); } }
        private bool ban02;
        public bool Ban02 { get => ban02; set { ban02 = value; OnPropertyChanged("Ban02"); } }
        private bool ban03;
        public bool Ban03 { get => ban03; set { ban03 = value; OnPropertyChanged("Ban03"); } }
        private bool ban04;
        public bool Ban04 { get => ban04; set { ban04 = value; OnPropertyChanged("Ban04"); } }

        private bool banhMiTrung;
        public bool BanhMiTrung { get => banhMiTrung; set { banhMiTrung = value; OnPropertyChanged("BanhMiTrung"); } }
        private bool banhMiCa;
        public bool BanhMiCa { get => banhMiCa; set { banhMiCa = value; OnPropertyChanged("BanhMiCa"); } }
        private bool miTomTrung;
        public bool MiTomTrung { get => miTomTrung; set { miTomTrung = value; OnPropertyChanged("MiTomTrung"); } }
        private bool miXaoBo;
        public bool MiXaoBo { get => miXaoBo; set { miXaoBo = value; OnPropertyChanged("MiXaoBo"); } }
        private bool miCay;
        public bool MiCay { get => miCay; set { miCay = value; OnPropertyChanged("MiCay"); } }

        private bool cafeDen;
        public bool CafeDen { get => cafeDen; set { cafeDen = value; OnPropertyChanged("CafeDen"); } }
        private bool cafeDa;
        public bool CafeDa { get => cafeDa; set { cafeDa = value; OnPropertyChanged("CafeDa"); } }
        private bool cafeSua;
        public bool CafeSua { get => cafeSua; set { cafeSua = value; OnPropertyChanged("CafeSua"); } }
        private bool cafeKem;
        public bool CafeKem { get => cafeKem; set { cafeKem = value; OnPropertyChanged("CafeKem"); } }
        private bool cafeSuaDa;
        public bool CafeSuaDa { get => cafeSuaDa; set { cafeSuaDa = value; OnPropertyChanged("CafeSuaDa"); } }

        public ICommand ChonCommand { get; set; }
        public ICommand NhapLaiCommand { get; set; }
        public ICommand ThanhToanCommand { get; set; }
        public ICommand ThoatCommand { get; set; }

        public MainVM()
        {
            Customers = new ObservableCollection<KhachHangModel>();

            ChonCommand = new RelayCommand(ExecuteChon, CanExecuteChon);
            NhapLaiCommand = new RelayCommand(ExecuteNhapLai);
            ThanhToanCommand = new RelayCommand(ExecuteThanhToan, CanExecuteThanhToan);
            ThoatCommand = new RelayCommand(ExecuteThoat);
        }

        private KhachHangModel hoaDonHienTai;

        public void ExecuteChon(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(SDT))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên và số điện thoại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!BanhMiTrung && !BanhMiCa && !MiTomTrung && !MiXaoBo && !MiCay &&
                !CafeDen && !CafeDa && !CafeSua && !CafeKem && !CafeSuaDa)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một món ăn hoặc nước uống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string viTri = Ban01 ? "Bàn 01" : Ban02 ? "Bàn 02" : Ban03 ? "Bàn 03" : "Bàn 04";

            List<string> listThucAn = new List<string>();
            decimal tienAn = 0;
            if (BanhMiTrung) { listThucAn.Add("Bánh mỳ trứng"); tienAn += 15000; }
            if (BanhMiCa) { listThucAn.Add("Bánh mỳ cá"); tienAn += 15000; }
            if (MiTomTrung) { listThucAn.Add("Mỳ tôm trứng"); tienAn += 20000; }
            if (MiXaoBo) { listThucAn.Add("Mỳ xào bò"); tienAn += 30000; }
            if (MiCay) { listThucAn.Add("Mỳ cay"); tienAn += 50000; }

            List<string> listNuoc = new List<string>();
            decimal tienNuoc = 0;
            if (CafeDen) { listNuoc.Add("Cafe đen"); tienNuoc += 20000; }
            if (CafeDa) { listNuoc.Add("Cafe đá"); tienNuoc += 25000; }
            if (CafeSua) { listNuoc.Add("Cafe sữa"); tienNuoc += 25000; }
            if (CafeKem) { listNuoc.Add("Cafe kem"); tienNuoc += 35000; }
            if (CafeSuaDa) { listNuoc.Add("Cafe sữa đá"); tienNuoc += 30000; }

            decimal tongTien = tienAn + tienNuoc;
            if (IsSinhVien) tongTien = tongTien * 0.8m; 

            int nextSTT = Customers.Count > 0 ? Customers.Max(x => x.STT) + 1 : 1;
            hoaDonHienTai = new KhachHangModel
            {
                STT = nextSTT,
                HoTen = NewName,
                SĐT = SDT,
                LaSinhVien = IsSinhVien,
                ViTriBan = viTri,
                ThucAn = string.Join(", ", listThucAn),
                NuocUong = string.Join(", ", listNuoc),
                TongTien = tongTien
            };

            ThongTinChiTiet = $"Khách hàng: {NewName}\n" +
                              $"Sinh viên: {(IsSinhVien ? "Có" : "Không")}\n" +
                              $"Vị trí: {viTri}\n" +
                              $"Nước uống:\n- {string.Join("\n- ", listNuoc)}\n" +
                              $"Thức ăn:\n- {string.Join("\n- ", listThucAn)}\n" +
                              $"Tổng tiền: {tongTien:N0}đ";
        }
        private bool CanExecuteChon(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewName) && !string.IsNullOrWhiteSpace(SDT);
        }
        public void ExecuteThanhToan(object parameter)
        {
            if (hoaDonHienTai != null)
            {
                Customers.Add(hoaDonHienTai);

                hoaDonHienTai = null;
                ThongTinChiTiet = string.Empty;
                ExecuteNhapLai(null);
                MessageBox.Show("Thanh toán thành công!", "Thông báo");
                OnPropertyChanged("TongKhachHang");
                OnPropertyChanged("TongTienThanhToan");
            }
        }

        private bool CanExecuteThanhToan(object parameter)
        {
            return hoaDonHienTai != null;
        }

        public void ExecuteNhapLai(object parameter)
        {
            NewName = string.Empty;
            SDT = string.Empty;
            IsSinhVien = false;
            Ban01 = true;
            BanhMiTrung = BanhMiCa = MiTomTrung = MiXaoBo = MiCay = false;
            CafeDen = CafeDa = CafeSua = CafeKem = CafeSuaDa = false;

            hoaDonHienTai = null;
            ThongTinChiTiet = string.Empty;
        }

        public void ExecuteThoat(object parameter)
        {
            Application.Current.MainWindow.Close();
        }
        public int TongKhachHang => Customers.Count;
        public decimal TongTienThanhToan => Customers.Sum(x => x.TongTien); 
    }
}
