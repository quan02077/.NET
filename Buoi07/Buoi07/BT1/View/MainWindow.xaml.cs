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

namespace Buoi07.BT1.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel.MainVM _viewModelChung;

        public MainWindow()
        {
            InitializeComponent();
            _viewModelChung = new ViewModel.MainVM();
            this.DataContext = _viewModelChung;
            MnuLapHoaDon_Click(null, null);
        }

        private void MnuLapHoaDon_Click(object sender, RoutedEventArgs e)
        {
            UCLapHoaDon viewLapHoaDon = new UCLapHoaDon();
            MainContent.Content = viewLapHoaDon;
        }

        private void MnuThongKe_Click(object sender, RoutedEventArgs e)
        {
            UCThongKeHoaDon viewThongKe = new UCThongKeHoaDon();
            MainContent.Content = viewThongKe;
        }
    }
}

