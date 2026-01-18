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
using System.Xml.Linq;

namespace Bai1
{
    /// <summary>
    /// Interaction logic for Bai4.xaml
    /// </summary>
    public partial class Bai4 : Window
    {
        private string[,] people = new string[5, 2];
        private int count = 0;
        public Bai4()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fullName = ins_Name.Text.Trim();
            string age = ins_Age.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(age))
            {
                result.Text = "Vui lòng nhập tên và tuổi!";
                return;
            }
            if (!int.TryParse(age, out int ageNum))
            {
                result.Text = "Tuổi phải là số!";
                return;
            }
            if (count >= people.GetLength(0))
            {
                result.Text = "Mảng đầy!";
                return;
            }

            people[count, 0] = fullName;
            people[count, 1] = age;
            count++;

            result.Text = "Danh sách người:\n";
            for (int i = 0; i < count; i++)
            {
                result.Text += $"{i + 1}. {people[i,0]} - {people[i, 1]} tuổi\n";
            }

            ins_Name.Clear();
            ins_Age.Clear();
            ins_Name.Focus();
        }
    }
}
