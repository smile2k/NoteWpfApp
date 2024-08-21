using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using System.ComponentModel;
using Prism.Mvvm;
using WpfApp1.Service;


namespace WpfApp1.MainWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            // Return the main window of the application.
            return Container.Resolve<MainWindow.Views.MainWindowView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Service.
            containerRegistry.RegisterSingleton<IDataHandlerService, DataHandlerService>();

            // Register View for Navigation.
            containerRegistry.RegisterForNavigation<WpfApp1.DetailPageModule.Views.DetailPageView>();
            containerRegistry.RegisterForNavigation<WpfApp1.DetailPageModule.Views.ToDoPageView>();
            containerRegistry.RegisterForNavigation<WpfApp1.DetailPageModule.Views.ExpensePageView>();
            containerRegistry.RegisterForNavigation<WpfApp1.DetailPageModule.Views.MenuPageView>();
            containerRegistry.RegisterForNavigation<MenuOption.Views.SettingView>();
            containerRegistry.RegisterForNavigation<MenuOption.Views.InformationView>();
            containerRegistry.RegisterForNavigation<MenuOption.Views.NothingView>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<MainWindow.Views.MainWindowView, MainWindow.ViewModels.MainWindowViewModel>();
            ViewModelLocationProvider.Register<HomePageModule.Views.HomePageView, HomePageModule.ViewModels.HomePageViewModel>();
            ViewModelLocationProvider.Register<DetailPageModule.Views.DetailPageView, DetailPageModule.ViewModels.DetailPageViewModel>();
            ViewModelLocationProvider.Register<DetailPageModule.Views.ToDoPageView, DetailPageModule.ViewModels.ToDoPageViewModel>();
            ViewModelLocationProvider.Register<DetailPageModule.Views.ExpensePageView, DetailPageModule.ViewModels.ExpensePageViewModel>();
            ViewModelLocationProvider.Register<DetailPageModule.Views.MenuPageView, DetailPageModule.ViewModels.MenuPageViewModel>();
            ViewModelLocationProvider.Register<MenuOption.Views.SettingView, MenuOption.ViewModels.SettingViewModel>();
            ViewModelLocationProvider.Register<MenuOption.Views.InformationView, MenuOption.ViewModels.InformationViewModel>();
            ViewModelLocationProvider.Register<MenuOption.Views.NothingView, MenuOption.ViewModels.NothingViewModel>();
        }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //    // Any additional startup logic
        //}
    }
}
