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
using Buoi05.BT4.VM;

namespace Buoi05.BT4.View
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Window
    {
        private StudentVM vm;
        public StudentView()
        {
            InitializeComponent();
            vm = this.DataContext as StudentVM;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            vm.AddStudent();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            vm.DeleteStudent();
        }
    }
}
