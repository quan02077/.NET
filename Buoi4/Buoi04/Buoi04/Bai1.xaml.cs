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

namespace Buoi04
{
    /// <summary>
    /// Interaction logic for Bai1.xaml
    /// </summary>
    public partial class Bai1 : Window
    {
        public Bai1()
        {
            InitializeComponent();
        }
        private void NhanVien_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UC_NhanVien();
        }

        private void PhongBan_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UC_PhongBan();
        }

        private void Thoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
