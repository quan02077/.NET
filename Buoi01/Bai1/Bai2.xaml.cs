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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtName.Text.Trim();
            if(string.IsNullOrEmpty(fullName) || fullName == "Nhap ten cua ban")
            {
                txtGreeting.Text = "Vui long nhap ho va ten!";
            }
            else
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                string formattedName = textInfo.ToTitleCase(fullName.ToLower());

                txtGreeting.Text = $"Xin chao, {formattedName}";
            }
        }
    }
}
