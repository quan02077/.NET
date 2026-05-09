using Buoi11.VM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi11.BT1
{
    public class KhoaInputViewModel : BaseVM, IDataErrorInfo
    {
        public bool IsEdit { get; set; } = false;
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
        private string _TenKhoa;
        public string TenKhoa
        {
            get => _TenKhoa;
            set
            {
                _TenKhoa = value;
                OnPropertyChanged(nameof(TenKhoa));
            }
        }
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(MaKhoa))
                {
                    if (string.IsNullOrWhiteSpace(MaKhoa))
                        return "Mã khoa không được để trống";
                    if (MaKhoa.Length > 5)
                        return "Mã khoa tối đa 5 ký tự";
                }
                if (columnName == nameof(TenKhoa))
                {
                    if (string.IsNullOrWhiteSpace(TenKhoa))
                        return "Tên khoa không được để trống";
                    if (TenKhoa.Length > 50)
                        return "Tên khoa tối đa 50 ký tự";
                }
                return null;
            }
        }
        public bool IsValid => string.IsNullOrEmpty(this[nameof(MaKhoa)]) && string.IsNullOrEmpty(this[nameof(TenKhoa)]);
    }
}
