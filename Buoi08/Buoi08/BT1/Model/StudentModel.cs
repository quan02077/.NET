using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi08.BT1.Model
{
    public class StudentModel
    {
        public string Lop { get; set; }
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public bool GioiTinh { get; set; }
        public string ThanhPho { get; set; }
        public string DiaChi { get; set; }
        public StudentModel(string TenLop, string MSV, string HoVaTen, bool Gender, string City, string Address)
        {
            Lop = TenLop;
            MaSV = MSV;
            HoTen = HoVaTen;
            GioiTinh = Gender;
            ThanhPho = City;
            DiaChi = Address;
        }
    }
}
