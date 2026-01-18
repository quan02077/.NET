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

namespace Bai1
{
    /// <summary>
    /// Interaction logic for Bai6.xaml
    /// </summary>
    public partial class Bai6 : Window
    {
        public Bai6()
        {
            InitializeComponent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu!", "Cảnh cáo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hoTen = txtName.Text.Trim();
            string tuoi = txtAge.Text.Trim();
            string ghiChu = txtNote.Text.Trim();
            txtShow.Text = $"Họ và tên: {hoTen}\nTuổi: {tuoi}\nGhi chú: {ghiChu}";
        }
    }
}
