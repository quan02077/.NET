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
using System.Globalization;

namespace Bai1
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
        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            string fullName = ins_Name.Text.Trim();
            string age = ins_Age.Text.Trim();
            show_Name.Text = $"Họ tên: {fullName}";
            show_Age.Text = $"Tuổi: {age}";
        }
    }
}
