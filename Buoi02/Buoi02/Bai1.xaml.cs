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

namespace Buoi02
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

        private void textName(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                lblError1.Text = $"{tb.Tag} không được bỏ trống!";
                tb.BorderBrush = Brushes.Red;
            }
            else
            {
                lblError1.Text = "";
                tb.BorderBrush = Brushes.Gray;
            }
        }

        private void textAge(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!tb.Text.All(Char.IsDigit))
            {
                lblError2.Text = $"{tb.Tag} phải là số!";
                tb.BorderBrush = Brushes.Red;
            }
            else
            {
                int a = int.Parse(tb.Text);
                if (a > DateTime.Now.Year)
                {
                    lblError2.Text = $"{tb.Tag} phải nhỏ hơn năm hiện tại!";
                    tb.BorderBrush = Brushes.Red;
                }
                else
                {
                    lblError2.Text = "";
                    tb.BorderBrush = Brushes.Gray;
                }
            }
        }
        private void btn_Show(object sender, RoutedEventArgs e)
        {
            int age = DateTime.Now.Year - int.Parse(txtAge.Text);
            string s = $"Họ tên: {txtName.Text} - Tuổi: {age}";
            MessageBox.Show(s, "Thông báo", MessageBoxButton.OK);
        }

        private void btn_Del(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtAge.Clear();
            txtName.Focus();
        }

        private void btn_Quit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult kq = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (kq == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
