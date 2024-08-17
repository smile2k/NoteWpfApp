using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DetailPageModule.Models
{
    public class ExpenseItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Datetime { get; set; }
        public double Amount { get; set; }
        public string ExpenseType { get; set; }
    }
}
