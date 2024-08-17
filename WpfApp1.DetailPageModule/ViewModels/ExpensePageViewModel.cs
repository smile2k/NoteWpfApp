using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.DetailPageModule.Models;
using WpfApp1.Service;

namespace WpfApp1.DetailPageModule.ViewModels
{
    public class ExpensePageViewModel : BindableBase
    {
        #region Fields

        private IDataHandlerService _dataHandler;
        private IRegionManager _regionManager;

        ExpenseItem expenseItemSelected;

        #endregion

        #region Observables Properties
        private ObservableCollection<ExpenseItem> _expenseList;
        public ObservableCollection<ExpenseItem> ExpenseList
        {
            get { return _expenseList; }
            set 
            { 
                _expenseList = value;

                RaisePropertyChanged(nameof(_expenseList));
                //SetProperty(ref _expenseList, value); 
            }
        }

        private bool _showPopup;
        public bool ShowPopup
        {
            get { return _showPopup; }
            set { SetProperty(ref _showPopup, value); }
        }

        private Brush _selectedColor;
        public Brush SelectedColor
        {
            get => _selectedColor;
            set => SetProperty(ref _selectedColor, value);
        }

        private ObservableCollection<ColorItem> _colorList;
        public ObservableCollection<ColorItem> ColorList
        {
            get => _colorList;
            set => SetProperty(ref _colorList, value);
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    UpdateDay(_selectedDate);
                    RaisePropertyChanged(nameof(_selectedDate));
                }
            }
        }

        private ObservableCollection<PieChartItem> _pieChartItemList;
        public ObservableCollection<PieChartItem> PieChartItemList
        {
            get => _pieChartItemList;
            set => SetProperty(ref _pieChartItemList, value);
        }
        #endregion

        public ExpensePageViewModel(IDataHandlerService dataHandler, IRegionManager regionManager) 
        {
            _dataHandler = dataHandler;
            _regionManager = regionManager;
            expenseItemSelected = new ExpenseItem();
            PieChartItemList = new ObservableCollection<PieChartItem>();

            LoadExpenseTable();

            // Command
            this.DeleteRowCommand = new DelegateCommand<object>(DeleteRow);
            this.AddRowCommand = new DelegateCommand(AddRow);
            this.SaveExpenseTableCommand = new DelegateCommand(SaveExpenseTable);
            this.OpenDetailRowCommand = new DelegateCommand<object>(OpenDetailRow);
            this.ChangeBackgroundColorCommand = new DelegateCommand<object>(ChangeBackgroundColor);
        }

        #region Commands
        public ICommand DeleteRowCommand { get; set; }
        public ICommand AddRowCommand { get; set; }
        public ICommand SaveExpenseTableCommand { get; set; }
        public ICommand OpenDetailRowCommand { get; set; }
        public ICommand ChangeBackgroundColorCommand { get; set; }

        #endregion

        #region Methods
        private void DeleteRow(object obj)
        {
            var taskNeedToDelete = (ExpenseItem)obj;
            ExpenseList.Remove(taskNeedToDelete);
        }

        private void AddRow()
        {
            var day = DateTime.Now.Day.ToString()+"/"+ DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            ExpenseList.Add(new ExpenseItem { Id = Guid.NewGuid(), Name = "", Datetime = day, Amount = 0, ExpenseType = "" }); ;
        }

        private void SaveExpenseTable()
        {
            try
            {
                string modelFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model");
                if (!Directory.Exists(modelFolderPath))
                {
                    Directory.CreateDirectory(modelFolderPath);
                }

                string filePath = Path.Combine(modelFolderPath, "ExpenseList.json");
                string json = JsonConvert.SerializeObject(ExpenseList, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, json);
                MessageBox.Show("Save done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Save Fail!", "Save Infor", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool LoadExpenseTable()
        {
            try
            {
                string filePath = GetFilePath();
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    ExpenseList = JsonConvert.DeserializeObject<ObservableCollection<ExpenseItem>>(json);
                }
                else
                {
                    ExpenseList = new ObservableCollection<ExpenseItem>();
                }
                return true;
            }
            catch
            {
                ExpenseList = new ObservableCollection<ExpenseItem>();
                return false;
            }
        }

        private void OpenDetailRow(object obj)
        {
            try
            {
                UpdatePieChart();
                var detailTask = (ExpenseItem)obj;
                expenseItemSelected = detailTask;

                //ShowPopup = true;
            }
            catch
            {
                ShowPopup = false;
            }
        }

        private void ChangeBackgroundColor(object obj)
        {
            if (obj is Brush brush)
            {
                SelectedColor = brush;
            }
        }

        private void UpdatePieChart()
        {
            PieChartItemList.Clear();
            PieChartItemList.Add(new PieChartItem { Title = "Mango", Value = 10 });
            PieChartItemList.Add(new PieChartItem { Title = "Banana", Value = 36 });
        }

        #endregion

        #region FUNCTIONs SUPPORT
        private string GetModelFolderPath()
        {
            // Path to Model
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model");
        }

        private string GetFilePath()
        {
            // Path to tasklist.json
            return Path.Combine(GetModelFolderPath(), "ExpenseList.json");
        }

        private void UpdateDay(DateTime? dateTime)
        {
            try
            {
                var day = dateTime?.Day.ToString() + "/" + dateTime?.Month.ToString() + "/" + dateTime?.Year.ToString();
                var item = ExpenseList.FirstOrDefault(e => e.Id == expenseItemSelected.Id);
                if (item != null)
                {
                    item.Datetime = day;                    
                }
            }
            catch
            {
                int x = 1;
            }
        }

        #endregion
    }
}
