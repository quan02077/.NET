using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buoi05.BT1.Model;

namespace Buoi05.BT1.ViewModel
{
    public class StudentViewModel
    {
        public List<Student> Students { get; set; }

        public StudentViewModel()
        {
            Students = new List<Student>
            {
                new Student { Name = "Quan", Age = 20 },
                new Student { Name = "An", Age = 18 }
            };
        }
    }
}
