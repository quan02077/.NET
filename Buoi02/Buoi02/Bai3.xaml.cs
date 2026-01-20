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
    /// Interaction logic for Bai3.xaml
    /// </summary>
    public partial class Bai3 : Window
    {
        public Bai3()
        {
            InitializeComponent();
        }
        private void Seat_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Background == Brushes.Gold)
            {
                MessageBox.Show("Ghế này đã được bán!");
                return;
            }

            if (btn.Background == Brushes.LightGray)
                btn.Background = Brushes.DeepSkyBlue;
            else
                btn.Background = Brushes.LightGray;
        }

        private void BtnChon_Click(object sender, RoutedEventArgs e)
        {
            int total = 0;

            foreach (Button btn in SeatGrid.Children)
            {
                if (btn.Background == Brushes.DeepSkyBlue)
                {
                    btn.Background = Brushes.Gold;
                    total += int.Parse(btn.Tag.ToString());
                }
            }

            txtTotal.Text = total.ToString();
        }

        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in SeatGrid.Children)
            {
                if (btn.Background == Brushes.DeepSkyBlue)
                    btn.Background = Brushes.LightGray;
            }

            txtTotal.Text = "0";
        }

        private void BtnThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
