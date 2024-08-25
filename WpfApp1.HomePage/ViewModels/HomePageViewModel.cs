using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Service;


namespace WpfApp1.HomePageModule.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        #region Fields

        private IDataHandlerService _dataHandler;
        private IRegionManager _regionManager;
        #endregion

        public HomePageViewModel(IDataHandlerService dataHandler, IRegionManager regionManager) 
        {
            _dataHandler = dataHandler;
            _regionManager = regionManager;

            this.OpenHomePageCommand = new DelegateCommand(OpenHomePage);
            this.OpenMenuPageCommand = new DelegateCommand(OpenMenuPage);
            this.OpenToDoPageCommand = new DelegateCommand(OpenToDoPage);
            this.OpenExpensePageCommand = new DelegateCommand(OpenExpensePage);
            this.CloseMenuCommand = new DelegateCommand(CloseMenu);
        }

        #region Observable Properties

        private bool _homePageIsChecked = true;
        public bool HomePageIsChecked
        {
            get => _homePageIsChecked;
            set => SetProperty(ref _homePageIsChecked, value);
        }

        private bool _menuPageIsChecked = false;
        public bool MenuPageIsChecked
        {
            get => _menuPageIsChecked;
            set => SetProperty(ref _menuPageIsChecked, value);
        }

        private bool _todoPageIsChecked = false;
        public bool TodoPageIsChecked
        {
            get => _todoPageIsChecked;
            set => SetProperty(ref _todoPageIsChecked, value);
        }

        private bool _expensePageIsChecked = false;
        public bool ExpensePageIsChecked
        {
            get => _expensePageIsChecked;
            set => SetProperty(ref _expensePageIsChecked, value);
        }

        #endregion



        #region Commands
        public ICommand OpenHomePageCommand { get; }
        public ICommand OpenMenuPageCommand { get; }
        public ICommand OpenToDoPageCommand { get; }
        public ICommand OpenExpensePageCommand { get; }
        public ICommand CloseMenuCommand { get; }
        #endregion



        #region Methods
        private void OpenHomePage()
        {
            HomePageIsChecked = true;
            MenuPageIsChecked = false;
            TodoPageIsChecked = false;
            ExpensePageIsChecked = false;
            _regionManager.RequestNavigate("DetailPageRegion", "DetailPageView");
        }

        private void OpenMenuPage()
        {
            if (MenuPageIsChecked)
            {
                HomePageIsChecked = false;
                TodoPageIsChecked = false;
                ExpensePageIsChecked = false;
                _regionManager.RequestNavigate("MenuRegion", "MenuPageView");
            }
            
        }

        private void OpenToDoPage()
        {
            HomePageIsChecked = false;
            MenuPageIsChecked = false;
            TodoPageIsChecked = true;
            ExpensePageIsChecked = false;
            _regionManager.RequestNavigate("DetailPageRegion", "ToDoPageView");
        }

        private void OpenExpensePage()
        {
            HomePageIsChecked = false;
            MenuPageIsChecked = false;
            TodoPageIsChecked = false;
            ExpensePageIsChecked = true;
            _regionManager.RequestNavigate("DetailPageRegion", "ExpensePageView");
        }

        private void CloseMenu()
        {
            MenuPageIsChecked = false;
        }
        #endregion
    }

}
