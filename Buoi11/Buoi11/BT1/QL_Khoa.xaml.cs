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

namespace Buoi11.BT1
{
    /// <summary>
    /// Interaction logic for QL_Khoa.xaml
    /// </summary>
    public partial class QL_Khoa : Window
    {
        public QL_Khoa()
        {
            InitializeComponent();
            this.DataContext = new KhoaVM();
        }
    }
}
