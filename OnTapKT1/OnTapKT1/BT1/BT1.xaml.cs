using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OnTapKT1.BT1
{
    /// <summary>
    /// Interaction logic for BT1.xaml
    /// </summary>
    public partial class BT1 : Window
    {
        public BT1()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string MaSV = txtId.Text.Trim();
            string HoTen = txtName.Text.Trim();

            string GioiTinh = (radNam.IsChecked == true) ? "Nam" : "Nữ";

            List<string> SoThich = new List<string>();
            if(chkTheThao.IsChecked == true) SoThich.Add("Thể thao");
            if(chkAmNhac.IsChecked == true) SoThich.Add("Âm nhạc");
            if(chkDuLich.IsChecked == true) SoThich.Add("Du lịch");

            string SoThichStr = string.Join(", ", SoThich);

            string Lop = cmbLop.Text.Trim();

            List<string> MonHoc = new List<string>();
            foreach(ListBoxItem item in lstMonHoc.SelectedItems)
            {
                MonHoc.Add(item.Content.ToString());
            }
            string MonHocStr = string.Join(", ", MonHoc);

            string KetQua = $"Mã SV: {MaSV}\nHọ Tên: {HoTen}\nGiới Tính: {GioiTinh}\nSở Thích: {SoThichStr}\nLớp: {Lop}\nMôn Học: {MonHocStr}";
            MessageBox.Show(KetQua, "Thông Tin Sinh Viên");
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn thoát?", "Xác Nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
