using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buoi05.BT2.Model;

namespace Buoi05.BT2.ViewModel
{
    public class StudentVM_BT2: BaseVM_BT2
    {
        private List<Student_BT2> _student;
        public List<Student_BT2> Students
        {
            get
            {
                return _student;
            }
            set
            {
                _student = value;
                OnPropertyChanged("Students");
            }
        }

        private Student_BT2 _selectedStudent;
        public Student_BT2 SelectedStudent
        {
            get
            {
                return _selectedStudent;
            }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }
        public StudentVM_BT2()
        {
            Students = new List<Student_BT2>
            {
                new Student_BT2 {Name = "Quan", Age = 20},
                new Student_BT2 {Name = "An", Age = 18}
            };
        }
        public void AddStudent(string name, int age)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception("Tên sinh viên không được để trống");
                }
                if(age <= 0)
                {
                    throw new Exception("Tuổi không được nhỏ hơn hoặc bằng 0");
                }
                List<Student_BT2> newList = new List<Student_BT2>(Students);
                newList.Add(new Student_BT2 { Name = name, Age = age });
                Students = newList;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi khi thêm sinh viên: " +  ex.Message);
            }
        }
        public void DeleteStudent()
        {
            try
            {
                if(SelectedStudent == null)
                {
                    throw new Exception("Vui lòng chọn sinh viên cần xóa");
                }
                List<Student_BT2> newList = new List<Student_BT2>(Students);
                newList.Remove(SelectedStudent);
                Students = newList;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
            }
        }
    }
}
