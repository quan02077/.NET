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

namespace KT1.B1
{
    /// <summary>
    /// Interaction logic for B1.xaml
    /// </summary>
    public partial class B1 : Window
    {
        public B1()
        {
            InitializeComponent();
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            string hoTen = txtName.Text.Trim();
            string gioiTinh = (radNam.IsChecked == true) ? "Nam" : "Nữ";
            string ngaySinhStr = dpNgaySinh.SelectedDate?.ToString("dd/MM/yyyy");
            List<string> dsSoThich = new List<string>();
            if(chkDocSach.IsChecked == true) dsSoThich.Add("Đọc sách");
            if(chkDuLich.IsChecked == true) dsSoThich.Add("Du lịch");
            if(chkNgheNhac.IsChecked == true) dsSoThich.Add("Nghe nhạc");
            if(chkTheThao.IsChecked == true) dsSoThich.Add("Thể thao");
            string soThich = string.Join(", ", dsSoThich);
            List<string> dsKyNang = new List<string>();
            foreach(ListBoxItem item in lstKyNang.SelectedItems)
            {
                dsKyNang.Add(item.Content.ToString());
            }
            string kyNang = string.Join(", ", dsKyNang);
            string ngheNghiep = txtNgheNghiep.Text.Trim();
            string quocTich = cmbQuocTich.Text.Trim();
            string thongTin = $"Họ tên: {hoTen}\nGiới tính: {gioiTinh}\nNgày sinh: {ngaySinhStr}\nSở thích: {soThich}\nKỹ năng: {kyNang}\nNghề nghiệp: {ngheNghiep}\nQuốc tịch: {quocTich}";
            MessageBox.Show(thongTin, "Thông tin cá nhân", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
