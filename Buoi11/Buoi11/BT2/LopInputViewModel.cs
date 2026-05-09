using Buoi11.VM;
using System.ComponentModel;

namespace Buoi11.BT2
{
    public class LopInputViewModel : BaseVM, IDataErrorInfo
    {
        private string _MaLop;
        public string MaLop
        {
            get => _MaLop;
            set
            {
                _MaLop = value;
                OnPropertyChanged(nameof(MaLop));
            }
        }

        private string _MaKhoa;
        public string MaKhoa
        {
            get => _MaKhoa;
            set
            {
                _MaKhoa = value;
                OnPropertyChanged(nameof(MaKhoa));
            }
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(MaLop))
                {
                    if (string.IsNullOrWhiteSpace(MaLop))
                        return "Mã lớp không được để trống";
                }
                if (columnName == nameof(MaKhoa))
                {
                    if (string.IsNullOrWhiteSpace(MaKhoa))
                        return "Vui lòng chọn Mã khoa";
                }
                return null;
            }
        }
        public bool IsValid => string.IsNullOrEmpty(this[nameof(MaLop)]) && string.IsNullOrEmpty(this[nameof(MaKhoa)]);
    }
}