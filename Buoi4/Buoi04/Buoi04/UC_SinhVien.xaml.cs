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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Buoi04
{
    /// <summary>
    /// Interaction logic for UC_SinhVien.xaml
    /// </summary>
    public partial class UC_SinhVien : UserControl
    {
        public UC_SinhVien()
        {
            InitializeComponent();
        }
        private void btn_QUITSV_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if(window is Bai3 main)
            {
                main.MainContent.Content = null;
            }
        }
    }
}
