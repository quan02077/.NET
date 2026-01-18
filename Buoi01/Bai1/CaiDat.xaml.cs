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
    /// Interaction logic for CaiDat.xaml
    /// </summary>
    public partial class CaiDat : UserControl
    {
        public CaiDat()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtName.Text.Trim();
            string age = txtAge.Text.Trim();
            string note = txtNote.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(age) || string.IsNullOrWhiteSpace(note))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu!", "Cảnh cáo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string resultText = $"Họ tên: {fullName} | Tuổi: {age} | Ghi chú: {note}";
            txtShow.Text += resultText + "\n";

            txtName.Clear();
            txtAge.Clear();
            txtNote.Clear();
            txtName.Focus();
        }
    }
}
