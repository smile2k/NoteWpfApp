using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Based.Common;

namespace WpfApp1.DetailPageModule.Models
{
    public class ExpenseItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Datetime { get; set; }
        public string Amount { get; set; }
        public string ExpenseType { get; set; }
    }
}
