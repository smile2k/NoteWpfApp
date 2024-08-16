using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MainWindow.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        #endregion

        public MainWindowViewModel()
        {
        }

        #region Observable Properties

        #endregion

    }
}
