using Buoi05.BT4.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Buoi05.BT4.VM
{
    public class StudentVM: BaseVM
    {
        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get => students;
            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }
        private ICollectionView studentsView;
        public ICollectionView StudentsView
        {
            get => studentsView;
            set
            {
                studentsView = value;
                OnPropertyChanged("StudentsView");
            }
        }
        private Student selectedStudent;
        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }
        private string newName;
        public string NewName
        {
            get => newName;
            set
            {
                newName = value;
                OnPropertyChanged("NewName");
            }
        }
        private int newAge;
        public int NewAge
        {
            get => newAge;
            set
            {
                newAge = value;
                OnPropertyChanged("NewAge");
            }
        }
        private bool isMale;
        public bool IsMale
        {
            get => isMale;
            set
            {
                isMale = value;
                OnPropertyChanged("IsMale");
            }
        }
        private bool isFemale;
        public bool IsFemale
        {
            get => isFemale;
            set
            {
                isFemale = value;
                OnPropertyChanged("IsFemale");
            }
        }
        private ObservableCollection<string> cities;
        public ObservableCollection<string> Cities
        {
            get => cities;
            set
            {
                cities = value;
                OnPropertyChanged("Cities");
            }
        }
        private string selectedCity;
        public string SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }
        private int studentCount;
        public int StudentCount
        {
            get => studentCount;
            set
            {
                studentCount = value;
                OnPropertyChanged(nameof(StudentCount));
            }
        }
        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                OnPropertyChanged("FilterText");
                StudentsView.Refresh();
            }
        }

        public StudentVM()
        {
            Students = new ObservableCollection<Student>
            {
                new Student{ Name="An", Age=20, Gender="Nam", City="Hà Nội"},
                new Student{ Name="Bình", Age=19, Gender="Nữ", City="Đà Nẵng"},
                new Student{ Name="Châu", Age=21, Gender="Nữ", City="TP.HCM"}
            };
            Cities = new ObservableCollection<string>
            {
                "TP.HCM",
                "Hà Nội",
                "Đà Nẵng"
            };
            StudentsView = CollectionViewSource.GetDefaultView(Students);
            StudentsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            StudentsView.Filter = FilterStudents;
            StudentCount = Students.Count;
        }
        private bool FilterStudents(object obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText))
                return true;

            Student s = obj as Student;
            if (s == null)
                return false;

            return s.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        public void AddStudent()
        {
            if (string.IsNullOrWhiteSpace(NewName) || NewAge <= 0)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
                return;
            }
            Students.Add(new Student
            {
                Name = NewName,
                Age = NewAge,
                Gender = IsMale ? "Nam" : "Nữ",
                City = SelectedCity
            });
            StudentCount = Students.Count;
            NewName = string.Empty;
            OnPropertyChanged(nameof(NewName));

            NewAge = 0;
            OnPropertyChanged(nameof(NewAge));
        }
        public void DeleteStudent()
        {
            if (SelectedStudent == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa!");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không!", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) 
                Students.Remove(SelectedStudent);
                StudentCount = Students.Count;
        }
    }
}
