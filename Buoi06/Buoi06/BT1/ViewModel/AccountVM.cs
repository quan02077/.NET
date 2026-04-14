using Buoi06.BT1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Printing;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Windows;
using System.Linq;

namespace Buoi06.BT1.ViewModel
{
    public class AccountVM : BaseVM
    {
        private ObservableCollection<AccountModel> accounts;
        public ObservableCollection<AccountModel> Accounts
        {
            get => accounts;
            set
            {
                accounts = value;
                OnPropertyChanged("Accounts");
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
        private AccountModel selectedAccount;
        public AccountModel SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;
                OnPropertyChanged("SelectedAccount");
            }
        }
        private string selectedCity;
        public string SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }
        private string newAccountNumber;
        public string NewAccountNumber
        {
            get => newAccountNumber;
            set
            {
                newAccountNumber = value;
                OnPropertyChanged("NewAccountNumber");
            }
        }
        private string newCustomerName;
        public string NewCustomerName
        {
            get => newCustomerName;
            set
            {
                newCustomerName = value;
                OnPropertyChanged("NewCustomerName");
            }
        }
        private string newAddress;
        public string NewAddress
        {
            get => newAddress;
            set
            {
                newAddress = value;
                OnPropertyChanged("NewAddress");
            }
        }
        private decimal newBalance;
        public decimal NewBalance
        {
            get => newBalance;
            set
            {
                newBalance = value;
                OnPropertyChanged("NewBalance");
            }
        }
        private bool isAdding;
        public bool IsAdding
        {
            get => isAdding;
            set
            {
                isAdding = value;
                OnPropertyChanged("IsAdding");
                OnPropertyChanged("AddButtonText");
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public string AddButtonText
        {
            get
            {
                if (IsAdding)
                {
                    return "Hủy";
                }
                else
                {
                    return "Thêm";
                }
            }
        }
        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                OnPropertyChanged("IsEditing");
                OnPropertyChanged("EditButtonText");
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public string EditButtonText
        {
            get
            {
                if (IsEditing)
                {
                    return "Hủy";
                }
                else
                {
                    return "Sửa";
                }
            }
        }
        public RelayCommand AddCommand {  get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public AccountVM()
        {
            Accounts = new ObservableCollection<AccountModel>();
            Cities = new ObservableCollection<string>();
            Cities.Add("HCM");
            Cities.Add("HN");
            Cities.Add("Đà Nẵng");
            Cities.Add("Cần Thơ");
            

            LoadSampleData();
            AddCommand = new RelayCommand(AddOrCancel, null);
            EditCommand = new RelayCommand(EditOrCancel,CanEditOrDelete);
            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete, CanEditOrDelete);
        }
        private void LoadSampleData()
        {
            AccountModel acc = new AccountModel();
            acc.STT = 1;
            acc.AccountNumber = "001";
            acc.CustomerName = "Nguyễn Văn A";
            acc.Address = "Q1";
            acc.City = "HCM";
            acc.Balance = 1000000;
            Accounts.Add(acc);
        }
        private void AddOrCancel(object obj)
        {
            if (!IsAdding)
            {
                ClearInput();
                IsAdding = true;
                IsEditing = false;
            }
            else
            {
                ClearInput();
                IsAdding = false;
            }
        }
        private void EditOrCancel(object obj)
        {
            if (!IsEditing)
            {
                if (SelectedAccount == null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần sửa.");
                    return;
                }

                NewAccountNumber = SelectedAccount.AccountNumber;
                NewCustomerName = SelectedAccount.CustomerName;
                NewAddress = SelectedAccount.Address;   
                SelectedCity = SelectedAccount.City;
                NewBalance = SelectedAccount.Balance;
                IsEditing = true;
                IsAdding = false;
            }
            else
            {
                ClearInput();
                IsEditing = false;
            }
        }
        private void Save(object obj)
        {
            if (IsAdding)
            {
                if (!ValidateInput()) return;
                AccountModel acc = new AccountModel();
                acc.STT = Accounts.Count + 1;
                acc.AccountNumber = NewAccountNumber;
                acc.CustomerName = NewCustomerName;
                acc.Address = NewAddress;
                acc.City = SelectedCity;
                acc.Balance = NewBalance;
                Accounts.Add(acc);
                UpdateSTT();
                OnPropertyChanged("TotalBalance");
                ClearInput();
                IsAdding = false;
            }
            else if (IsEditing)
            {
                if (SelectedAccount == null)
                {
                    MessageBox.Show("Không có tài khoản nào để sửa.");
                    return;
                }
                if (!ValidateInput()) return;
                SelectedAccount.AccountNumber = NewAccountNumber;
                SelectedAccount.CustomerName = NewCustomerName;
                SelectedAccount.Address = NewAddress;
                SelectedAccount.City = SelectedCity;
                SelectedAccount.Balance = NewBalance;
                OnPropertyChanged("TotalBalance");
                ClearInput();

                IsEditing = false;
            }
        }
        private void Delete(object obj)
        {
            if (SelectedAccount == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa.");
                return;
            }
            Accounts.Remove(SelectedAccount);
            UpdateSTT();
            OnPropertyChanged("TotalBalance");
            ClearInput();
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(NewAccountNumber))
            {
                MessageBox.Show("Số tài khoản không được rỗng.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewCustomerName))
            {
                MessageBox.Show("Tên khách hàng không được rỗng.");
                return false;
            }

            if (NewBalance < 0)
            {
                MessageBox.Show("Số tiền không được âm.");
                return false;
            }

            bool duplicate = false;

            if (IsAdding)
            {
                duplicate = Accounts.Any(a => a.AccountNumber == NewAccountNumber);
            }
            else if (IsEditing && SelectedAccount != null)
            {
                if (NewAccountNumber != SelectedAccount.AccountNumber)
                {
                    duplicate = Accounts.Any(a => a.AccountNumber == NewAccountNumber);
                }
            }

            if (duplicate)
            {
                MessageBox.Show("Trùng số tài khoản.");
                return false;
            }

            return true;
        }
        private bool CanSave(object obj)
        {
            return IsAdding || IsEditing;
        }
        private bool CanEditOrDelete(object obj)
        {
            return SelectedAccount != null;
        }
        private void ClearInput()
        {
            NewAccountNumber = "";
            NewCustomerName = "";
            NewAddress = "";
            SelectedCity = null;
            NewBalance = 0;
        }
        private void UpdateSTT()
        {
            int i = 1;
            foreach (var acc in Accounts)
            {
                acc.STT = i;
                i++;
            }
        }
        public decimal TotalBalance
        {
            get
            {
                decimal total = 0;
                foreach (AccountModel acc in Accounts)
                {
                    total += acc.Balance;
                }
                return total;
            }
        }
    }
}
