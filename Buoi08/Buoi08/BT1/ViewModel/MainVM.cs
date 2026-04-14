using Buoi08.BT1.Model;
using Buoi08.BT1.Model;
using Buoi08.BT1.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Buoi08.BT1.ViewModel
{
    public class MainVM: BaseVM
    {
        private ObservableCollection<ClassModel> classes;
        public ObservableCollection<ClassModel> Classes
        {
            get => classes;
            set
            {
                classes = value;
                OnPropertyChanged("Classes");
            }
        }

        private ObservableCollection<ClassModel> students;
        public ObservableCollection<ClassModel> Students
        {
            get => students;
            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }

        private ClassModel selectionClass;
        public ClassModel SelectionClass
        {
            get => selectionClass;
            set
            {
                selectionClass = value;
                OnPropertyChanged("SelectionClass");
            }
        }
        private StudentModel selectionStudent;
        public StudentModel SelectionStudent
        {
            get => selectionStudent;
            set
            {
                selectionStudent = value;
                OnPropertyChanged("SelectionStudent");
                if (selectionStudent != null)
                {
                    NewId = selectionStudent.MaSV;         
                    IsNam = selectionStudent.GioiTinh;      
                    IsNu = !selectionStudent.GioiTinh;      
                    SelectionCity = selectionStudent.ThanhPho; 
                    DiaChi = selectionStudent.DiaChi;         
                }
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
        private string selectionCity;
        public string SelectionCity
        {
            get => selectionCity;
            set
            {
                selectionCity = value;
                OnPropertyChanged("SelectionCity");
            }
        }
        private string newLop;
        public string NewLop
        {
            get=> newLop;
            set
            {
                newLop = value;
                OnPropertyChanged("NewLop");
            }
        }
        private string newId;
        public string NewId
        {
            get => newId;
            set
            {
                newId = value;  
                OnPropertyChanged("NewId");
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
        private bool isNam;
        public bool IsNam
        {
            get => isNam;
            set
            {
                isNam = value;
                OnPropertyChanged("IsNam");
            }
        }
        private bool isNu;
        public bool IsNu
        {
            get => isNu;
            set
            {
                isNu = value;
                OnPropertyChanged("IsNu");
            }
        }
        private string diaChi;
        public string DiaChi
        {
            get => diaChi;
            set
            {
                diaChi = value;
                OnPropertyChanged("DiaChi");
            }
        }


        private string tuKhoaTimKiem;
        public string TuKhoaTimKiem
        {
            get => tuKhoaTimKiem;
            set
            {
                tuKhoaTimKiem = value;
                OnPropertyChanged("TuKhoaTimKiem");
                ViewSinhVien?.Refresh(); 
            }
        }

        private ClassModel lopLocDangChon;
        public ClassModel LopLocDangChon
        {
            get => lopLocDangChon;
            set
            {
                lopLocDangChon = value;
                OnPropertyChanged("LopLocDangChon");
                ViewSinhVien?.Refresh();
            }
        }

        private ICollectionView viewSinhVien;
        public ICollectionView ViewSinhVien
        {
            get => viewSinhVien;
            set
            {
                viewSinhVien = value;
                OnPropertyChanged("ViewSinhVien");
            }
        }

        public ICommand AddStudentCommand {  get; set; }
        public ICommand UpdateStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand AddClassCommand { get; set; }

        public ICommand XuatFileCommand { get; set; }

        public MainVM()
        {
            Classes = new ObservableCollection<ClassModel>
            {
                new ClassModel("05DHTH1"),
                new ClassModel("05DHTH2")
            };
            Cities = new ObservableCollection<string>();
            Cities.Add("Hà Nội");
            Cities.Add("TP.HCM");
            Cities.Add("Đà Nẵng");
            Cities.Add("Cần Thơ");
            
            AddStudentCommand = new RelayCommand(ExecuteAddStudent, CanExecuteAddStudent);
            UpdateStudentCommand = new RelayCommand(ExecuteUpdateStudent, CanExecuteUpdateStudent);
            DeleteStudentCommand = new RelayCommand(ExecuteDeleteStudent, CanExecuteDeleteStudent);
            AddClassCommand = new RelayCommand(ExecuteAddClass, CanExecuteAddClass);
            XuatFileCommand = new RelayCommand(ExecuteXuatFile);
            IsNu = true;
            IsNam = false;
            CapNhatViewSinhVien();
        }

        private void ExecuteXuatFile(object obj)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV (*.csv)|*.csv";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(dialog.FileName, false, new UTF8Encoding(true)))
                    {
                        writer.WriteLine("Mã SV,Họ Tên,Giới Tính,Thành Phố,Địa Chỉ,Tên Lớp");

                        foreach (StudentModel sv in ViewSinhVien)
                        {
                            string gioiTinh = sv.GioiTinh ? "Nam" : "Nữ";
                            string tenSua = sv.HoTen != null ? sv.HoTen.Replace(",", "") : "";
                            string diaChiSua = sv.DiaChi != null ? sv.DiaChi.Replace(",", " -") : "";

                            string dongDuLieu = $"{sv.MaSV},{tenSua},{gioiTinh},{sv.ThanhPho},{diaChiSua},{sv.Lop}";
                            writer.WriteLine(dongDuLieu);
                        }
                    }
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu file: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void CapNhatViewSinhVien()
        {
            if (Classes == null) return;

            var tatCaSinhVien = Classes.SelectMany(c => c.Students).ToList();

            ViewSinhVien = CollectionViewSource.GetDefaultView(tatCaSinhVien);
            ViewSinhVien.Filter = LocSinhVien;
        }

        private bool LocSinhVien(object obj)
        {
                if (obj is StudentModel sv)
                {
                    bool dungLop = LopLocDangChon == null || LopLocDangChon.Name == "All" || sv.Lop == LopLocDangChon.Name;

                    bool dungTen = string.IsNullOrWhiteSpace(TuKhoaTimKiem) || sv.HoTen.ToLower().Contains(TuKhoaTimKiem.ToLower());

                    return dungLop && dungTen;
                }
            return false;
        }
        private void ExecuteAddStudent(object parameter)
        {
             if(SelectionClass == null)
            {
                MessageBox.Show("Vui lòng chọn lớp trước khi thêm sinh viên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool isDuplicated = SelectionClass.Students.Any(e => e.MaSV == NewId);
            if (isDuplicated)
            {
                MessageBox.Show("Mã sinh viên này đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            StudentModel st = new StudentModel(NewLop, NewId, NewName, IsNam, SelectionCity, DiaChi);
            SelectionClass.Students.Add(st);
            NewId = string.Empty;
            NewLop = string.Empty;
            NewName = string.Empty;
            IsNam = true;
            IsNu = false;
            SelectionCity = null;
            DiaChi = string.Empty;
            CapNhatViewSinhVien();
        }
        private bool CanExecuteAddStudent(object parameter)
        {
            return SelectionClass != null && !string.IsNullOrWhiteSpace(NewId) && !string.IsNullOrWhiteSpace(NewName) && selectionCity != null && !string.IsNullOrWhiteSpace(DiaChi);
        }

        private void ExecuteUpdateStudent(object parameter)
        {
            if (SelectionStudent == null || SelectionClass == null)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên trong TreeView để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            StudentModel svCapNhat = new StudentModel(SelectionClass.Name, NewId, NewName, IsNam, SelectionCity, DiaChi);

            int index = SelectionClass.Students.IndexOf(SelectionStudent);
            if (index != -1)
            {
                SelectionClass.Students[index] = svCapNhat;
            }

            MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            NewId = string.Empty;
            NewName = string.Empty;
            IsNam = false;
            IsNu = true;
            SelectionCity = null;
            DiaChi = string.Empty;
            SelectionStudent = null;
            CapNhatViewSinhVien();
        }
        private bool CanExecuteUpdateStudent(object parameter)
        {
            return SelectionStudent != null && SelectionClass != null && !string.IsNullOrWhiteSpace(NewId) && !string.IsNullOrWhiteSpace(NewName) && selectionCity != null && !string.IsNullOrWhiteSpace(DiaChi);
        }
        private void ExecuteDeleteStudent(object parameter)
        {
            if (SelectionStudent == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên trước khi xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này!", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SelectionClass.Students.Remove(SelectionStudent);
            }
            CapNhatViewSinhVien();
        }
        private bool CanExecuteDeleteStudent(object parameter)
        {
            return SelectionStudent != null;
        }
        private void ExecuteAddClass(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewLop))
            {
                MessageBox.Show("Vui lòng nhập tên lớp cần thêm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string tenLopMoi = NewLop.Trim();
            bool isDuplicated = Classes.Any(c => c.Name.ToLower() == tenLopMoi.ToLower());

            if (isDuplicated)
            {
                MessageBox.Show("Tên lớp này đã tồn tại! Vui lòng nhập tên khác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ClassModel lopHocMoi = new ClassModel(tenLopMoi);
            Classes.Add(lopHocMoi);
            MessageBox.Show("Thêm lớp mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            NewLop = string.Empty;
        }
        private bool CanExecuteAddClass(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewLop);
        }
    }
}
