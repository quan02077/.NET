using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Buoi07.BT3.ViewModel
{
    public class MainViewModel : BaseVM
    {
        private string _tenKhachHang;
        public string TenKhachHang
        {
            get { return _tenKhachHang; }
            set { _tenKhachHang = value; OnPropertyChanged(); }
        }

        private string _soDienThoai;
        public string SoDienThoai
        {
            get { return _soDienThoai; }
            set { _soDienThoai = value; OnPropertyChanged(); }
        }

        private string _noiDungGopY;
        public string NoiDungGopY
        {
            get { return _noiDungGopY; }
            set { _noiDungGopY = value; OnPropertyChanged(); }
        }

        private int _currentViewIndex = 0;
        public int CurrentViewIndex
        {
            get { return _currentViewIndex; }
            set { _currentViewIndex = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> DanhSachPhanHoi { get; set; }

        public ICommand ThamGiaGopYCommand { get; set; }
        public ICommand FormPhanHoiCommand { get; set; }
        public ICommand ThoatCommand { get; set; }
        public ICommand GuiGopYCommand { get; set; }
        public ICommand QuayLaiCommand { get; set; }

        public MainViewModel()
        {
            DanhSachPhanHoi = new ObservableCollection<string>
            {
                "Câu 1: Chất lượng dịch vụ - Đáp án: ...",
                "Câu 2: Chất lượng sản phẩm - Đáp án: Hài lòng",
                "Câu 3: Chất lượng phục vụ - Đáp án: Bình thường",
                "Câu 4: Chất lượng bảo hành - Đáp án: ...",
                "Câu 5: Chất lượng dịch vụ - Đáp án: Ý kiến khác"
            };

            ThamGiaGopYCommand = new RelayCommand(p => CurrentViewIndex = 1, p => !string.IsNullOrEmpty(TenKhachHang));
            FormPhanHoiCommand = new RelayCommand(p => CurrentViewIndex = 2, p => !string.IsNullOrEmpty(TenKhachHang));
            QuayLaiCommand = new RelayCommand(p => CurrentViewIndex = 0);

            GuiGopYCommand = new RelayCommand(p =>
            {
                MessageBox.Show($"Cảm ơn {TenKhachHang} đã gửi góp ý!", "Thông báo thành công");
                NoiDungGopY = string.Empty;
                CurrentViewIndex = 0;
            });

            ThoatCommand = new RelayCommand(p => Application.Current.Shutdown());
        }
    }
}