using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Buoi06.BT2.Model
{
    public class Department
    {
        public string Name { get; set; }
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public Department(string name)
        {
            Name = name;
        }
    }
}
