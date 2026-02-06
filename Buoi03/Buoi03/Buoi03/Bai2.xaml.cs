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
    /// Interaction logic for Bai2.xaml
    /// </summary>
    public partial class Bai2 : Window
    {
        public Bai2()
        {
            InitializeComponent();
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (!Validateinputs())
                return;
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = "";
            if (txtNam.IsChecked == true)
                gioiTinh = "Nam";
            else
                gioiTinh = "Nữ";
            string ngaySinh = "";
            if (txtDate.SelectedDate != null)
                ngaySinh = txtDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            string queQuan = "";
            if (txtQueQuan.SelectedIndex >= 0)
                queQuan = txtQueQuan.Text;
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
            string soThichStr = "";
            if (soThich.Length > 0)
                soThichStr = soThich.ToString().TrimEnd(' ', ',');
            var kyNang = txtKyNang.SelectedItems.Cast<ListBoxItem>().Select(item => item.Content.ToString());
            string kyNangText;
            if (kyNang.Any())
                kyNangText = string.Join(", ", kyNang);
            else
                kyNangText = "Không có";
            string ghiChu = txtGhiChu.Text.Trim();

            lblHoTen.Text = hoTen;
            lblGioiTinh.Text = gioiTinh;
            lblNgaySinh.Text = ngaySinh;
            lblQuocTich.Text = queQuan;
            lblNgheNghiep.Text = ngheNghiep;
            lblSoThich.Text = soThichStr;
            lblKyNang.Text = kyNangText;
            lblGhiChu.Text = ghiChu;

            TabControl.SelectedIndex = 1;
        }

        private bool Validateinputs()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtHoTen.Focus();
                return false;
            }
            if (txtNam.IsChecked == false && txtNu.IsChecked == false)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtDate.Focus();
                return false;
            }
            if (txtQueQuan.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn quê quán.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtQueQuan.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNghe.Text))
            {
                MessageBox.Show("Vui lòng nhập nghề nghiệp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNghe.Focus();
                return false;
            }
            if (txtDocSach.IsChecked == false && txtNgheNhac.IsChecked == false &&
                txtTheThao.IsChecked == false && txtDuLich.IsChecked == false)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sở thích.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtKyNang.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một kỹ năng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtKyNang.Focus();
                return false;
            }
            return true;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
