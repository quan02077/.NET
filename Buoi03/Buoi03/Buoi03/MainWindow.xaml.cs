using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Buoi03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void show(object sender, RoutedEventArgs e)
        {
            string strMessage, strHoten, strTitle,strGioiTinh, strNgoaiNgu = "";
            strHoten = txtHo.Text + " " + txtTen.Text + " ";
            if (nam.IsChecked == true)
                strTitle = "Mr. ";
            else
                strTitle = "Ms. ";
            strMessage = "Xin chao " + strTitle + strHoten + "\n";
            
            if (anh.IsChecked == true)
                strNgoaiNgu = "tiếng Anh";
            if (trung.IsChecked == true)
            {
                if (strNgoaiNgu != "")
                    strNgoaiNgu += ", tiếng Trung";
                else
                    strNgoaiNgu = "tiếng Trung";
            }
            
            strMessage += "ngoại ngữ: " + strNgoaiNgu + " ";

            if (cBox.SelectedIndex >= 0)
            {
                strMessage +="quê quán: " + cBox.Text;
            }

            MessageBox.Show(strMessage, "Thông báo");
        }

        private void huy(object sender, RoutedEventArgs e)
        {
            txtHo.Text = "";
            txtTen.Text = "";
            nam.IsChecked = true;
            nu.IsChecked = false;
            anh.IsChecked = false;
            trung.IsChecked = false;
            cBox.SelectedIndex = 0;
        }
    }
}