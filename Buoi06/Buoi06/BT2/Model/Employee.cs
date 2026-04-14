using System;
using System.Collections.Generic;
using System.Text;

namespace Buoi06.BT2.Model
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public Employee(string id, string fullName, string address)
        {
            EmployeeId = id;
            FullName = fullName;
            Address = address;
        }
    }
}
