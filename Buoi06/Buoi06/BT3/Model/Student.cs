using System;
using System.Collections.Generic;
using System.Text;

namespace Buoi06.BT3.Model
{
    public class Student
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public Student(string id, string fullName, string address)
        {
            StudentId = id;
            FullName = fullName;
            Address = address;
        }
    }
}
