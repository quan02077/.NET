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
    /// Interaction logic for BTVN1.xaml
    /// </summary>
    public partial class BTVN1 : Window
    {
        public BTVN1()
        {
            InitializeComponent();
        }

        private void Button_Click_cd(object sender, RoutedEventArgs e)
        {
            CaiDat.Content = new CaiDat();
        }
    }
}
