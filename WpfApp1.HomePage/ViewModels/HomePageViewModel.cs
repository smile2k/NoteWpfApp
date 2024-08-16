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
            this.OpenSettingPageCommand = new DelegateCommand(OpenSettingPage);
            this.OpenToDoPageCommand = new DelegateCommand(OpenToDoPage);
            this.OpenExpensePageCommand = new DelegateCommand(OpenExpensePage);
        }

        #region Observable Properties

        #endregion



        #region Commands
        public ICommand OpenHomePageCommand { get; }
        public ICommand OpenSettingPageCommand { get; }
        public ICommand OpenToDoPageCommand { get; }
        public ICommand OpenExpensePageCommand { get; }
        #endregion



        #region Methods
        private void OpenHomePage()
        {
            int x = 1;
            _regionManager.RequestNavigate("DetailPageRegion", "DetailPageView");
        }

        private void OpenSettingPage()
        {
            int x = 1;
        }

        private void OpenToDoPage()
        {
            int x = 1;
            _regionManager.RequestNavigate("DetailPageRegion", "ToDoPageView");
        }

        private void OpenExpensePage()
        {
            int x = 1;
            _regionManager.RequestNavigate("DetailPageRegion", "ExpensePageView");
        }
        #endregion
    }

}
