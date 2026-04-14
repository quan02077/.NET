using Buoi06.BT2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Buoi06.BT3.Model
{
    public class Class
    {
        public string Name { get; set; }
        public ObservableCollection<Student> Student { get; set; } = new ObservableCollection<Student>();
        public Class(string name)
        {
            Name = name;
        }
    }
}
