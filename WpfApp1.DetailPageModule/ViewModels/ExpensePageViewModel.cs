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
using LiveCharts;
using LiveCharts.Wpf;
using WpfApp1.Based.Common;

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

        private ObservableCollection<string> _expenseOptions;
        public ObservableCollection<string> ExpenseOptions
        {
            get => _expenseOptions;
            set => SetProperty(ref _expenseOptions, value);
        }

        private string _selectedExpenseOption;
        public string SelectedExpenseOption
        {
            get => _selectedExpenseOption;
            set
            {
                _selectedExpenseOption = value;
                RaisePropertyChanged();
            }
        }

        //private string _expenseType;
        //public string ExpenseType
        //{
        //    get => _expenseType;
        //    set
        //    {
        //        _expenseType = value;
        //        RaisePropertyChanged();
        //    }
        //}

        public SeriesCollection PieSeries { get; set; }
        #endregion

        public ExpensePageViewModel(IDataHandlerService dataHandler, IRegionManager regionManager) 
        {
            _dataHandler = dataHandler;
            _regionManager = regionManager;
            expenseItemSelected = new ExpenseItem();
            PieSeries = new SeriesCollection();
            ExpenseOptions = new ObservableCollection<string> 
            {
                ExpenseTypeEnum.General.ToString(),
                ExpenseTypeEnum.Food.ToString(),
                ExpenseTypeEnum.Transport.ToString(),
                ExpenseTypeEnum.Gift.ToString(),
                ExpenseTypeEnum.Rent.ToString(),
                ExpenseTypeEnum.Healthcare.ToString(),
            };
            SelectedExpenseOption = ExpenseOptions.FirstOrDefault();

            LoadExpenseTable();
            UpdatePieSeries();

            // Command
            this.DeleteRowCommand = new DelegateCommand<object>(DeleteRow);
            this.AddRowCommand = new DelegateCommand(AddRow);
            this.SaveExpenseTableCommand = new DelegateCommand(SaveExpenseTable);
            this.OpenDetailRowCommand = new DelegateCommand<object>(OpenDetailRow);
            this.ChangeBackgroundColorCommand = new DelegateCommand<object>(ChangeBackgroundColor);
            this.LostFocusAmountColumnCommand = new DelegateCommand<object>(LostFocusAmountColumn);
            this.LostFocusExpenseTypeColumnCommand = new DelegateCommand<object>(LostFocusExpenseTypeColumn);
        }

        #region Commands
        public ICommand DeleteRowCommand { get; set; }
        public ICommand AddRowCommand { get; set; }
        public ICommand SaveExpenseTableCommand { get; set; }
        public ICommand OpenDetailRowCommand { get; set; }
        public ICommand ChangeBackgroundColorCommand { get; set; }
        public ICommand LostFocusAmountColumnCommand { get; set; }
        public ICommand LostFocusExpenseTypeColumnCommand { get; set; }

        #endregion

        #region Methods
        private void DeleteRow(object obj)
        {
            var taskNeedToDelete = obj as ExpenseItem;
            ExpenseList.Remove(taskNeedToDelete);
        }

        private void AddRow()
        {
            var day = DateTime.Now.Day.ToString()+"/"+ DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            ExpenseList.Add(new ExpenseItem { Id = Guid.NewGuid(), Name = "", Datetime = day, Amount = "0", ExpenseType = ExpenseTypeEnum.General.ToString() }); ;
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
                //UpdatePieSeries();
                var detailTask = obj as ExpenseItem;
                expenseItemSelected = detailTask;

                //ShowPopup = true;
            }
            catch
            {
                ShowPopup = false;
            }
        }

        private void LostFocusAmountColumn(object rowSelected)
        {
            try
            {
                var expenseItem = rowSelected as ExpenseItem;
                expenseItemSelected = expenseItem;

                bool convertToDouble = double.TryParse(expenseItem.Amount, out var money);
                if (convertToDouble)
                {
                    if (money < 0)
                    {
                        var row = ExpenseList.Where(p => p.Id == expenseItemSelected.Id).FirstOrDefault();
                        row.Amount = "0";
                    }
                }
                else
                {
                    MessageBox.Show("Enter numbers only!", "Update Chart", MessageBoxButton.OK, MessageBoxImage.Warning);
                    var row = ExpenseList.Where(p => p.Id == expenseItemSelected.Id).FirstOrDefault();
                    row.Amount = "0";
                }

                UpdatePieSeries();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Update Chart Fail!", "Update Chart", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LostFocusExpenseTypeColumn(object rowSelected)
        {
            try
            {
                UpdatePieSeries();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Chart Fail!", "Update Chart", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ChangeBackgroundColor(object obj)
        {
            if (obj is Brush brush)
            {
                SelectedColor = brush;
            }
        }

        private void UpdatePieSeries()
        {
            try
            {
                PieSeries.Clear();
                Random random = new Random();

                var expensesByType = ExpenseList.GroupBy(e => e.ExpenseType)
                                         .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var data in expensesByType)
                {
                    Color randomColor = Color.FromRgb(
                    (byte)random.Next(0, 256),
                    (byte)random.Next(0, 256),
                    (byte)random.Next(0, 256));

                    List<ExpenseItem> expenses = data.Value;
                    double totalAmount = 0;
                    foreach (var expense in expenses)
                    {
                        totalAmount += double.Parse(expense.Amount);
                    }

                    PieSeries.Add(new PieSeries
                    {
                        Title = data.Key.ToString(),
                        Values = new ChartValues<double> { totalAmount },
                        DataLabels = true,
                        Fill = new SolidColorBrush(randomColor)
                    });
                }
            }
            catch
            {
                
            }
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
