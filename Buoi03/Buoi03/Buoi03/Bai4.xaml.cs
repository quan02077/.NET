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

namespace Buoi03
{
    /// <summary>
    /// Interaction logic for Bai4.xaml
    /// </summary>
    public partial class Bai4 : Window
    {
        public Bai4()
        {
            InitializeComponent();
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string ngaySinh = "";
            if (txtDate.SelectedDate != null)
                ngaySinh = txtDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            string gioiTinh = "";
            if (txtNam.IsChecked == true)
                gioiTinh = "Nam";
            else
                gioiTinh = "Nữ";
            string quocTich = "";
            if (txtQuocTich.SelectedIndex >= 0)
                quocTich = txtQuocTich.Text;
            string ngheNghiep = txtNghe.Text.Trim();
            var soThich = new StringBuilder();
            if (txtDocSach.IsChecked == true)
                soThich.Append("Đọc sách, ");
            if (txtNgheNhac.IsChecked == true)
                soThich.Append("Nghe nhạc, ");
            if (txtTheThao.IsChecked == true)
                soThich.Append("Thể thao, ");
            if (txtDuLich.IsChecked == true)
                soThich.Append("Du lịch, ");
            if (txtChoiGame.IsChecked == true)
                soThich.Append("Chơi game, ");
            string soThichStr = "";
            if (soThich.Length > 0)
                soThichStr = soThich.ToString().TrimEnd(' ', ',');
            var kyNang = txtKyNang.SelectedItems.Cast<ListBoxItem>().Select(item => item.Content.ToString());
            string kyNangText;
            if (kyNang.Any())
                kyNangText = string.Join(", ", kyNang);
            else
                kyNangText = "Không có";

            string info = $"Họ tên: {hoTen} \n Ngày sinh: {ngaySinh} \n Giới tính: {gioiTinh} \n Quốc tịch: {quocTich} \n Nghề nghiệp: {ngheNghiep} \n Sở thích: {soThichStr} \n Kỹ năng: {kyNangText}";
            
            MessageBox.Show(info, "Thông tin cá nhân", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
