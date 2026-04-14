using Buoi06.BT1.ViewModel;
using Buoi06.BT3.Model;
using Buoi06.BT4.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Buoi06.BT4.ViewModel
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
                if (selectedAccount != null)
                {
                    NewAccountNumber = selectedAccount.AccountId;
                    NewCustomerName = selectedAccount.AccountName;
                    NewAddress = selectedAccount.Address;
                    SelectedCity = selectedAccount.City;
                    NewBalance = selectedAccount.Balance;
                }
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
        private decimal totalBalance;
        public decimal TotalBalance
        {
            get => totalBalance;
            set
            {
                totalBalance = value;
                OnPropertyChanged("TotalBalance");
            }
        }
        public RelayCommand AddCommand { get; set; }
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
            AddCommand = new RelayCommand(AddAccountCommand, CanAddAccountCommand);
            EditCommand = new RelayCommand(EditAccountCommand, CanEditAccountCommand);
            SaveCommand = new RelayCommand(SaveAccountCommand, CanSaveAccountCommand);
            DeleteCommand = new RelayCommand(DelAccountCommand, CanDelAccountCommand);
        }
        private void LoadSampleData()
        {
            AccountModel acc = new AccountModel();
            acc.STT = 1;
            acc.AccountId = "001";
            acc.AccountName = "Nguyễn Văn A";
            acc.Address = "Q1";
            acc.City = "HCM";
            acc.Balance = 1000000;
            Accounts.Add(acc);
            CalculateTotalBalance();
        }
        
        private void AddAccountCommand(object parameter)
        {
            if(string.IsNullOrWhiteSpace(NewAccountNumber) && string.IsNullOrWhiteSpace(NewCustomerName))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ stk và tên khách hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool isDuplicated = Accounts.Any(x => x.AccountId == NewAccountNumber);
            if(isDuplicated)
            {
                MessageBox.Show("Số tài khoản này đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int nextSTT = 1;
            if (Accounts.Count > 0)
            {
                nextSTT = Accounts.Max(x => x.STT) + 1;
            }
            AccountModel acc = new AccountModel();
            acc.AccountId = NewAccountNumber;
            acc.AccountName = NewCustomerName;
            acc.Address = NewAddress;
            acc.City = SelectedCity;
            acc.Balance = NewBalance;
            Accounts.Add(acc);
            NewAccountNumber = string.Empty;
            NewCustomerName = string.Empty;
            NewAddress = string.Empty;
            SelectedCity = null;
            NewBalance = 0;
            CalculateTotalBalance();
        }

        private bool CanAddAccountCommand(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewAccountNumber) && !string.IsNullOrWhiteSpace(NewCustomerName);
        }

        private bool CanDelAccountCommand(object parameter)
        {
            return SelectedAccount != null;
        }

        private void DelAccountCommand(object parameter)
        {
            if (SelectedAccount == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản bạn muốn xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Accounts.Remove(SelectedAccount);
                for (int i = 0; i < Accounts.Count; i++)
                {
                    Accounts[i].STT = i + 1;
                }
                NewAccountNumber = "";
                NewCustomerName = "";
                NewAddress = "";
                SelectedCity = null;
                NewBalance = 0;
                CalculateTotalBalance();
            }
        }

        private bool CanEditAccountCommand(object parameter)
        {
            return SelectedAccount != null;
        }

        private void EditAccountCommand(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewAccountNumber) || string.IsNullOrWhiteSpace(NewCustomerName))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ STK và tên khách hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (SelectedAccount == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản bạn muốn sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool isDuplicate = Accounts.Any(x => x.AccountId == NewAccountNumber && x.AccountId != SelectedAccount.AccountId);
            if(isDuplicate)
            {
                MessageBox.Show("Số tài khoản này đã tồn tại ở một khách hàng khác!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int index = Accounts.IndexOf(SelectedAccount);
            if(index >= 0)
            {
                AccountModel updateacc = new AccountModel()
                {
                    STT = SelectedAccount.STT,
                    AccountId = NewAccountNumber,
                    AccountName = NewCustomerName,
                    Address = NewAddress,
                    City = SelectedCity,
                    Balance = newBalance,
                };
                Accounts[index] = updateacc;
                SelectedAccount = updateacc;
                CalculateTotalBalance();
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
        }

        private bool CanSaveAccountCommand(object parameter)
        {
            return Accounts != null && Accounts.Count > 0;
        }

        private void SaveAccountCommand(object parameter)
        {
            MessageBox.Show($"Đã lưu thành công {Accounts.Count} tài khoản vào hệ thống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CalculateTotalBalance()
        {
            if (Accounts != null)
            {
                TotalBalance = Accounts.Sum(x => x.Balance);
            }
        }
    }
}
