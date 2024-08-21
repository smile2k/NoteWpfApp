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
        }

        #region Observable Properties

        private bool _homePageIsChecked = true;
        public bool HomePageIsChecked
        {
            get => _homePageIsChecked;
            set => SetProperty(ref _homePageIsChecked, value);
        }

        private bool _settingPageIsChecked = false;
        public bool SettingPageIsChecked
        {
            get => _settingPageIsChecked;
            set => SetProperty(ref _settingPageIsChecked, value);
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
        #endregion



        #region Methods
        private void OpenHomePage()
        {
            HomePageIsChecked = true;
            SettingPageIsChecked = false;
            TodoPageIsChecked = false;
            ExpensePageIsChecked = false;
            _regionManager.RequestNavigate("DetailPageRegion", "DetailPageView");
        }

        private void OpenMenuPage()
        {
            HomePageIsChecked = false;
            SettingPageIsChecked = true;
            TodoPageIsChecked = false;
            ExpensePageIsChecked = false;
            _regionManager.RequestNavigate("DetailPageRegion", "MenuPageView");
        }

        private void OpenToDoPage()
        {
            HomePageIsChecked = false;
            SettingPageIsChecked = false;
            TodoPageIsChecked = true;
            ExpensePageIsChecked = false;
            _regionManager.RequestNavigate("DetailPageRegion", "ToDoPageView");
        }

        private void OpenExpensePage()
        {
            HomePageIsChecked = false;
            SettingPageIsChecked = false;
            TodoPageIsChecked = false;
            ExpensePageIsChecked = true;
            _regionManager.RequestNavigate("DetailPageRegion", "ExpensePageView");
        }
        #endregion
    }

}
