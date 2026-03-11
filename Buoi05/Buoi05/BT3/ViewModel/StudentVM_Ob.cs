using Buoi05.BT3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Buoi05.BT3.ViewModel
{
    public class StudentVM_Ob:BaseVM
    {
        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get
            {
                return students;
            }
            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }
        private ICollectionView studentsView;
        public ICollectionView StudentsView
        {
            get
            {
                return studentsView;
            }
            set
            {
                studentsView = value;
                OnPropertyChanged("StudentsView");
            }
        }
        private Student selectionStudent;
        public Student SelectionStudent
        {
            get
            {
                return selectionStudent;
            }
            set
            {
                selectionStudent = value;
                OnPropertyChanged("SelectionStudent");
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
        public StudentVM_Ob()
        {
            Students = new ObservableCollection<Student>
            {
                new Student { Name = "An", Age = 20 },
                new Student { Name = "Bình", Age = 18 },
                new Student { Name = "Chi", Age = 19 }
            };
            StudentsView = CollectionViewSource.GetDefaultView(Students);
            StudentsView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
            StudentsView.Filter = FilterStudents;
        }
        private bool isAscending = true;
        public void ToggleSortByAge()
        {
            if(StudentsView == null)
            {
                return;
            }
            StudentsView.SortDescriptions.Clear();
            if (isAscending)
            {
                StudentsView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
            }
            else
            {
                StudentsView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Descending));
            }
            isAscending = !isAscending;
        }
        private bool FilterStudents(object obj)
        {
            if(string.IsNullOrWhiteSpace(FilterText))
                return true;
            Student student = obj as Student;
            if (student == null)
                return false;
            return student.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
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
                Age = NewAge
            });
            NewName = string.Empty;
            NewAge = 0;
        }
        public void DeleteStudent()
        {
            if (SelectionStudent == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa!");
                return;
            }
            Students.Remove(SelectionStudent);
        }
    }
}
