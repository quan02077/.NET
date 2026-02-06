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
    /// Interaction logic for Bai3.xaml
    /// </summary>
    public partial class Bai3 : Window
    {
        public Bai3()
        {
            InitializeComponent();
        }

        private void btn_SinhVien(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UC_SinhVien();
        }

        private void btn_LopHoc(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UC_LopHoc();
        }

        private void btn_Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
