using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi08.BT1.Model
{
    public class ClassModel
    {
        public string Name { get; set; }
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();
        public ClassModel(string name)
        {
            Name = name;
        }
    }
}
