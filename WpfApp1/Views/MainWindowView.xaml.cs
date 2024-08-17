using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.MainWindow.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        IRegionManager _regionManager;
        public MainWindowView(IRegionManager regionManager)
        {
            InitializeComponent();
            _regionManager = regionManager;

            _regionManager.RegisterViewWithRegion("HomePageRegion", typeof(WpfApp1.HomePageModule.Views.HomePageView));
            _regionManager.RegisterViewWithRegion("DetailPageRegion", typeof(WpfApp1.DetailPageModule.Views.DetailPageView));
            _regionManager.RegisterViewWithRegion("DetailPageRegion", typeof(WpfApp1.DetailPageModule.Views.ToDoPageView));
            _regionManager.RegisterViewWithRegion("DetailPageRegion", typeof(WpfApp1.DetailPageModule.Views.ExpensePageView));
            _regionManager.RegisterViewWithRegion("DetailPageRegion", typeof(WpfApp1.DetailPageModule.Views.SettingView));
        }
    }
}
