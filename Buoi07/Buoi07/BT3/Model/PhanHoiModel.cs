using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi07.BT3.Model
{
    public class PhanHoiModel
    {
        public string NoiDungCauHoi { get; set; }
        public string DapAn { get; set; }

        public override string ToString()
        {
            return $"{NoiDungCauHoi} - Đáp án: {DapAn}";
        }
    }
}
