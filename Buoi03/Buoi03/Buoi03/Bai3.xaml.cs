using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    /// Interaction logic for Bai3.xaml
    /// </summary>
    public partial class Bai3 : Window
    {
        public Bai3()
        {
            InitializeComponent();
        }
        private void btn_ThemMon (object sender, RoutedEventArgs e)
        {
            string tenKhach = txtTenKhachHang.Text.Trim();
            string sDT = txtSoDienThoai.Text.Trim();
            string chonBan = "";
            if (cmbChonBan.SelectedIndex >= 0)
                chonBan = cmbChonBan.Text;
            string chonMonAn = "";
            if (cmbChonMonAn.SelectedIndex >= 0)
                chonMonAn = cmbChonMonAn.Text;
            string info = $"Khách hàng: {tenKhach} \n SĐT: {sDT} \n Bàn: {chonBan}";
            show_Info.Text = info;
            string chonMon = $"{chonMonAn} \n";
            show_FoodList.Items.Add(chonMon);

        }
        private void btn_XoaMon (object sender, RoutedEventArgs e)
        {
            if (show_FoodList.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn món ăn cần xóa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            show_FoodList.Items.Remove(show_FoodList.SelectedItem);
        }
        private void btn_DatMon (object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text) || string.IsNullOrWhiteSpace(txtSoDienThoai.Text) || cmbChonBan.SelectedIndex < 0 || cmbChonMonAn.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin và chọn món ăn trước khi đặt món.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MessageBox.Show("Đặt món thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
