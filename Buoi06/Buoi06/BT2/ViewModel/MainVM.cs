using Buoi06.BT2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace Buoi06.BT2.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public MainVM()
        {
            Departments = new ObservableCollection<Department>();

            Departments.Add(new Department("Giám đốc"));
            Departments.Add(new Department("Tổ chức tài chính"));
            Departments.Add(new Department("Kế hoạch"));
            Departments.Add(new Department("Kế Toán"));
            AddEmployeeCommand = new RelayCommand(AddEmployeeExecute, CanAddEmployeeExecute);
            AddDepartmentCommand = new RelayCommand(AddDepartmentExecute, CanAddDepartmentExecute);
            RemoveDepartmentCommand = new RelayCommand(RemoveDepartmentExecute, CanRemoveDepartmentExecute);
        }
        private ObservableCollection<Department> _departments;
        public ObservableCollection<Department> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged("Departments");
            }
        }
        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");
            }
        }
        private string _newDepartmentName;
        public string NewDepartmentName
        {
            get { return _newDepartmentName; }
            set
            {
                _newDepartmentName = value;
                OnPropertyChanged("NewDepartmentName");
            }
        }
        private string _employeeId;
        public string EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                OnPropertyChanged("EmployeeId");
            }
        }
        private string _employeeName;
        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                _employeeName = value;
                OnPropertyChanged("EmployeeName");
            }
        }
        private string _employeeAddress;
        public string EmployeeAddress
        {
            get { return _employeeAddress; }
            set
            {
                _employeeAddress = value;
                OnPropertyChanged("EmployeeAddress");
            }
        }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand AddDepartmentCommand { get; set; }
        public ICommand RemoveDepartmentCommand { get; set; }
        private void AddEmployeeExecute(object parameter)
        {
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban trước khi thêm nhân viên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool isDuplicate = SelectedDepartment.Employees.Any(e =>
            e.EmployeeId.Equals(EmployeeId,
            StringComparison.OrdinalIgnoreCase));
            if (isDuplicate)
            {
                MessageBox.Show("Mã nhân viên đã tồn tại trong phòng ban!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Employee emp = new Employee(EmployeeId, EmployeeName, EmployeeAddress);
            SelectedDepartment.Employees.Add(emp);
            EmployeeId = string.Empty;
            EmployeeName = string.Empty;
            EmployeeAddress = string.Empty;
        }
        private bool CanAddEmployeeExecute(object parameter)
        {
            return SelectedDepartment != null &&
            !string.IsNullOrWhiteSpace(EmployeeId) &&
            !string.IsNullOrWhiteSpace(EmployeeName);

        }
        private void AddDepartmentExecute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewDepartmentName))
                return;
            bool isDuplicate = Departments.Any(d => d.Name.Equals(NewDepartmentName.Trim(),
            StringComparison.OrdinalIgnoreCase));
            if (isDuplicate)
            {
                MessageBox.Show("Phòng ban đã tồn tại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Department dep = new Department(NewDepartmentName.Trim());
            Departments.Add(dep);
            NewDepartmentName = string.Empty;
        }
        private bool CanAddDepartmentExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewDepartmentName);
        }
        private void RemoveDepartmentExecute(object parameter)
        {
            if (SelectedDepartment == null)
                return;
            MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn xóa phòng ban \"{SelectedDepartment.Name}\"?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Departments.Remove(SelectedDepartment);
            }
        }
        private bool CanRemoveDepartmentExecute(object parameter)
        {
            return SelectedDepartment != null;
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