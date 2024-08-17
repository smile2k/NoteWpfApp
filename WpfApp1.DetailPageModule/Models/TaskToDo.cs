using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DetailPageModule.Models
{
    public class TaskToDo
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
        public string Name { get; set; }
        public string Datetime { get; set; }
    }
}
