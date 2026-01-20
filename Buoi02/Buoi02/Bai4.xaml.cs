using System;
using System.Windows;
using System.Windows.Controls;

namespace Buoi02
{
    public partial class Bai4 : Window
    {
        public Bai4()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (rbBac1.IsChecked == true)
            {
                txtC.Visibility = Visibility.Collapsed;
            }
        }


        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            if (txtC == null) return;
            if (rbBac1.IsChecked == true)
            {
                txtC.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtC.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double a, b, c;

            // Kiểm tra a, b
            if (!double.TryParse(txtA.Text, out a) ||
                !double.TryParse(txtB.Text, out b))
            {
                MessageBox.Show("Vui lòng nhập đúng số cho a và b");
                return;
            }

            if (rbBac1.IsChecked == true)
            {
                if (a == 0)
                {
                    if (b == 0)
                        txtShow.Text = "Phương trình có vô số nghiệm";
                    else
                        txtShow.Text = "Phương trình vô nghiệm";
                }
                else
                {
                    double x = -b / a;
                    txtShow.Text = $"Nghiệm x = {x}";
                }
            }

            else
            {
                if (!double.TryParse(txtC.Text, out c))
                {
                    MessageBox.Show("Vui lòng nhập đúng số cho c");
                    return;
                }

                if (a == 0)
                {
                    txtShow.Text = "Không phải phương trình bậc hai";
                    return;
                }

                double delta = b * b - 4 * a * c;

                if (delta < 0)
                {
                    txtShow.Text = "Phương trình vô nghiệm";
                }
                else if (delta == 0)
                {
                    double x = -b / (2 * a);
                    txtShow.Text = $"Phương trình có nghiệm kép x = {x}";
                }
                else
                {
                    double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                    txtShow.Text = $"x1 = {x1}, x2 = {x2}";
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
