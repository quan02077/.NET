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

namespace Buoi04
{
    /// <summary>
    /// Interaction logic for Bai2.xaml
    /// </summary>
    public partial class Bai2 : Window
    {
        public Bai2()
        {
            InitializeComponent();
            ADDPhongBan("Giám đốc", "BGĐ");
            ADDPhongBan("Kế hoạch", "PKH");
            ADDPhongBan("Kế toán", "PKT");
        }

        void ADDPhongBan(string tenPB, string maPB)
        {
            TreeViewItem pb = new TreeViewItem
            {
                Header = $"{tenPB} - {maPB}"
            };
            txtPhongBan.Items.Add(pb);
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            ADDPhongBan(txtTenPhong.Text, txtMaPhong.Text);
        }
        
        private void tvPhongBan_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(txtPhongBan.SelectedItem != null)
            {
                TreeViewItem item = txtPhongBan.SelectedItem as TreeViewItem;
                string[] pb = (item.Header.ToString()).Split('-');
                txtTenPhongBan.Text = pb[0];
                txtChucVu.Text = pb[1];
            }
        }

        private void QUIT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
