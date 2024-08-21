using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.MenuOption.Views;

namespace WpfApp1.DetailPageModule.ViewModels
{
    public class MenuPageViewModel : BindableBase
    {
        #region Fields

        private IRegionManager _regionManager;

        #endregion

        public MenuPageViewModel(IRegionManager regionManager) 
        {
            _regionManager = regionManager;

            ToggleShowSettingCommand = new DelegateCommand(ToggleShowSetting);
            ToggleShowInformationCommand = new DelegateCommand(ToggleShowInformation);
        }

        #region Observable Properties

        private bool _isShowSetting = false;
        public bool IsShowSetting
        {
            get => _isShowSetting;
            set => SetProperty(ref _isShowSetting, value);
        }

        private bool _isShowInformation = false;
        public bool IsShowInformation
        {
            get => _isShowInformation;
            set => SetProperty(ref _isShowInformation, value);
        }

        #endregion

        #region Command

        public ICommand ToggleShowSettingCommand { get; }
        public ICommand ToggleShowInformationCommand { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Show setting
        /// </summary>
        private void ToggleShowSetting()
        {
            if (IsShowSetting)
            {
                IsShowInformation = false;
                _regionManager.RequestNavigate("MenuContentRegion", nameof(SettingView));
            }
            else
            {
                _regionManager.RequestNavigate("MenuContentRegion", nameof(NothingView));
            }
        }

        /// <summary>
        /// Show information
        /// </summary>
        private void ToggleShowInformation()
        {
            if (IsShowInformation)
            {
                IsShowSetting = false;
                _regionManager.RequestNavigate("MenuContentRegion", nameof(InformationView));
            }
            else
            {
                _regionManager.RequestNavigate("MenuContentRegion", nameof(NothingView));
            }
        }

        #endregion
    }
}
