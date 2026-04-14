using Buoi08.BT1.ViewModel;
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
using Buoi08.BT1.Model;

namespace Buoi08.BT1.View
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
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is MainVM vm)
            {

                if (e.NewValue is Model.ClassModel dep)
                {
                    vm.SelectionClass = dep;
                    vm.SelectionClass = null;
                }
                else if (e.NewValue is StudentModel st)
                {
                    vm.SelectionStudent = st;
                }
            }
        }
    }
}
