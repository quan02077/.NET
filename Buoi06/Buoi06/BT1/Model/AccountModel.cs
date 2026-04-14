using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Buoi06.BT1.Model
{
    public class AccountModel : INotifyPropertyChanged
    {
        private int _stt;
        private string _accountNumber;
        private string _customerName;
        private string _address;
        private string _city;
        private decimal _balance;
        public int STT
        {
            get
            {
                return _stt;
            }
            set
            {
                if (_stt != value)
                {
                    _stt = value;
                    OnPropertyChanged("STT");
                }
            }
        }
        public string AccountNumber
        {
            get
            {
                return _accountNumber;
            }
            set
            {
                if (_accountNumber != value)
                {
                    _accountNumber = value;
                    OnPropertyChanged("AccountNumber");
                }
            }
        }

        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged("CustomerName");
                }
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }
        public decimal Balance
        {
            get
            {
                return _balance;
            
}
            set
            {
                if (_balance != value)
                {
                    _balance = value;
                    OnPropertyChanged("Balance");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new

                PropertyChangedEventArgs(propertyName));

            }
        }
    }
}
