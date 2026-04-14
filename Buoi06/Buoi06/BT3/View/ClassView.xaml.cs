using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Buoi06.BT3.Model;     
using Buoi06.BT3.ViewModel; 

namespace Buoi06.BT3.View
{
    /// <summary>
    /// Interaction logic for ClassView.xaml
    /// </summary>
    public partial class ClassView : UserControl
    {
        public ClassView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is MainVM vm)
            {
              
                if (e.NewValue is Model.Class dep)
                {
                    vm.SelectedClass = dep;
                    vm.SelectedStudent = null; 
                }
                else if (e.NewValue is Student st)
                {
                    vm.SelectedStudent = st; 
                }
            }
        }
    }
}