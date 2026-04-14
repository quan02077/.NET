using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Buoi06.BT3.Model;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows;

namespace Buoi06.BT3.ViewModel
{
    public class MainVM: INotifyPropertyChanged
    {
        public MainVM()
        {
            Classes = new ObservableCollection<Class>();

            Classes.Add(new Class("05DHTH01"));
            Classes.Add(new Class("05DHTH02"));
            Classes.Add(new Class("05DHTH03"));
            Classes.Add(new Class("05DHTH04"));

            AddStudentCommand = new RelayCommand(AddStudentExecute, CanAddStudentExecute);
            AddClassCommand = new RelayCommand(AddClassExecute, CanAddClassExecute);
            DelStudentCommand = new RelayCommand(DelStudentExecute, CanDelStudentExecute);
        }

        private ObservableCollection<Class> classes;
        public ObservableCollection<Class> Classes
        {
            get => classes;
            set
            {
                classes = value;
                OnPropertyChanged("Classes");
            }
        }

        private Class selectedClass;
        public Class SelectedClass
        {
            get => selectedClass;
            set
            {
                selectedClass = value;
                OnPropertyChanged("SelectedClass");
            }
        }

        private string newClass;
        public string NewClass
        {
            get=> newClass;
            set
            {
                newClass = value;
                OnPropertyChanged("NewClass");
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

        private string newStudentId;
        public string NewStudentId
        {
            get => newStudentId;
            set
            {
                newStudentId = value;
                OnPropertyChanged("NewStudentId");
            }
        }

        private string newStudentName;
        public string NewStudentName
        {
            get => newStudentName;
            set
            {
                newStudentName = value;
                OnPropertyChanged("NewStudentName");
            }
        }

        private string newStudentAddress;
        public string NewStudentAddress
        {
            get=>newStudentAddress;
            set
            {
                newStudentAddress = value;
                OnPropertyChanged("NewStudentAddress");
            }
        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand DelStudentCommand { get; set; }
        public ICommand AddClassCommand { get; set; }

        private void AddStudentExecute (object parameter)
        {
            if(SelectedClass == null)
            {
                MessageBox.Show("Vui lòng chọn lớp trước khi thêm sinh viên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool isDuplicated = SelectedClass.Student.Any(e  => e.StudentId == newStudentId);
            if (isDuplicated)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại trong lớp học này!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Student st = new Student(NewStudentId, NewStudentName, NewStudentAddress);
            SelectedClass.Student.Add(st);
            NewStudentId = string.Empty;
            NewStudentName = string.Empty;
            NewStudentAddress = string.Empty;
        }

        private bool CanAddStudentExecute(object parameter)
        {
            return SelectedClass != null && !string.IsNullOrWhiteSpace(NewStudentId) && !string.IsNullOrWhiteSpace(NewStudentName);
        }

        private void AddClassExecute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewClass))
            {
                MessageBox.Show("Vui lòng điền tên lớp trước khi thêm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool isDuplicate = Classes.Any(e => e.Name == NewClass);
            if(isDuplicate)
            {
                MessageBox.Show("Lớp này đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Class cl = new Class(NewClass);
            Classes.Add(cl);
            NewClass = string.Empty;
        }
        private bool CanAddClassExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewClass);
        }

        private void DelStudentExecute(object parameter)
        {
            if (SelectedStudent == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên trước khi xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này!", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {

            
                foreach (var lop in Classes)
                {
                    if (lop.Student.Contains(SelectedStudent))
                    {
                        lop.Student.Remove(SelectedStudent);
                        break; 
                    }
                }
            }
        }
        private bool CanDelStudentExecute(object parameter)
        {
            return SelectedStudent != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
