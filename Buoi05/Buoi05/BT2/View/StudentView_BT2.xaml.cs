using Buoi05.BT1.ViewModel;
using Buoi05.BT2.ViewModel;
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

namespace Buoi05.BT2.View
{
    /// <summary>
    /// Interaction logic for StudentView_BT2.xaml
    /// </summary>
    public partial class StudentView_BT2 : Window
    {
        StudentVM_BT2 vm;
        public StudentView_BT2()
        {
            InitializeComponent();
            vm = new StudentVM_BT2();
            this.DataContext = vm;
        }
        private void BtnAdd_Click(object sender,RoutedEventArgs e)

        {
            try
            {
                string name = txtName.Text;
                int age = int.Parse(txtAge.Text);
                vm.AddStudent(name, age);
                txtName.Clear();
                txtAge.Clear();
            }
            catch
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
            }
        }
        private void BtnDel_Click(object sender,RoutedEventArgs e)

        {
            try
            {
                vm.DeleteStudent();
            }
            catch
            {
                MessageBox.Show("Không thể xóa!");
            }
        }
    }
}
