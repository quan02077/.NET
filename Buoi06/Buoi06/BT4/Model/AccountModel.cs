using System;
using System.Collections.Generic;
using System.Text;

namespace Buoi06.BT4.Model
{
    public class AccountModel
    {
        public int STT {  get; set; }
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Balance { get; set; }
    }
}
