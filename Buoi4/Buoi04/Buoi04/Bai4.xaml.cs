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
    /// Interaction logic for Bai4.xaml
    /// </summary>
    public partial class Bai4 : Window
    {
        public Bai4()
        {
            InitializeComponent();
        }

        void Add_PB(string tenPB)
        {
            TreeViewItem pb = new TreeViewItem
            {
                Header = tenPB
            };
            tv_PhongBan.Items.Add(pb);
        }
        void Add_NV(TreeViewItem pb, string hoTen, string maNV, string diaChi, string dienThoai)
        {
            TreeViewItem nv = new TreeViewItem
            {
                Header = $"{hoTen} ({maNV})"
            };

            TreeViewItem dc_sdt = new TreeViewItem
            {
                Header = $"{diaChi} - {dienThoai}"
            };

            nv.Items.Add(dc_sdt);
            pb.Items.Add(nv);
            pb.IsExpanded = true;
        }

    private void btn_ThemPB (object sender, RoutedEventArgs e)
        {
            string tenPB = txt_PhongBan.Text.Trim();
            if(tenPB == "")
            {
                MessageBox.Show("Chưa nhập tên phòng ban");
                return;
            }
            foreach (TreeViewItem item in tv_PhongBan.Items)
            {
                if(item.Header.ToString() == tenPB)
                {
                    MessageBox.Show("Phòng ban đã tồn tại!");
                    return;
                }    
            }
            Add_PB(tenPB);
        }

        private void btn_XoaPB (object  sender, RoutedEventArgs e)
        {
            TreeViewItem pb = tv_PhongBan.SelectedItem as TreeViewItem;

            if(pb == null)
            {
                MessageBox.Show("Chưa chọn phòng ban");
                return;
            }
            if (pb.Items.Count > 0)
            {
                MessageBox.Show("Phòng ban còn nhân viên, không thể xóa");
                return;
            }
            tv_PhongBan.Items.Remove(pb);
        }

        TreeViewItem nvDangChon;

        private void tvPhongBan_SelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = tv_PhongBan.SelectedItem as TreeViewItem;
            if (item == null) return;

            if (item.Parent is TreeView)
            {
                show_PhongBan.Text = item.Header.ToString();

                txt_HoTen.Clear();
                txt_MaSo.Clear();
                txt_DiaChi.Clear();
                txt_DienThoai.Clear();

                nvDangChon = null;
            }
            else if (item.Parent is TreeViewItem && item.Items.Count == 1)
            {
                nvDangChon = item;

                string[] nv = item.Header.ToString()
                                 .Replace(")", "")
                                 .Split('(');

                txt_HoTen.Text = nv[0].Trim();
                txt_MaSo.Text = nv[1].Trim();

                string[] tt = (item.Items[0] as TreeViewItem).Header.ToString().Split('-');

                txt_DiaChi.Text = tt[0].Trim();
                txt_DienThoai.Text = tt[1].Trim();
            }
        }


        private void btn_Them(object sender, RoutedEventArgs e)
        {
            TreeViewItem pb = tv_PhongBan.SelectedItem as TreeViewItem;

            if (pb == null || pb.Parent is TreeViewItem)
            {
                MessageBox.Show("Vui lòng chọn phòng ban!");
                return;
            }

            Add_NV(pb,
                txt_HoTen.Text,
                txt_MaSo.Text,
                txt_DiaChi.Text,
                txt_DienThoai.Text);
        }

        private void btn_Sua(object sender, RoutedEventArgs e)
        {
            if (nvDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên");
                return;
            }

            nvDangChon.Header = $"{txt_HoTen.Text} ({txt_MaSo.Text})";
            (nvDangChon.Items[0] as TreeViewItem).Header = $"{txt_DiaChi.Text} - {txt_DienThoai.Text}";


            MessageBox.Show("Sửa nhân viên thành công");
        }
        private void btn_Xoa(object sender, RoutedEventArgs e)
        {
            if (nvDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên");
                return;
            }

            TreeViewItem pb = nvDangChon.Parent as TreeViewItem;
            pb.Items.Remove(nvDangChon);
            nvDangChon = null;

            txt_HoTen.Clear();
            txt_MaSo.Clear();
            txt_DiaChi.Clear();
            txt_DienThoai.Clear();
        }

        private void btn_Thoat (object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
