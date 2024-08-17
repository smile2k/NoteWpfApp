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
using System.Xml;
using WpfApp1.DetailPageModule.Models;
using WpfApp1.Service;

namespace WpfApp1.DetailPageModule.ViewModels
{
    public class ToDoPageViewModel : BindableBase
    {
        #region Fields

        private IDataHandlerService _dataHandler;
        private IRegionManager _regionManager;


        #endregion


        #region Observables Properties
        private ObservableCollection<TaskToDo> _taskList;
        public ObservableCollection<TaskToDo> TaskList
        {
            get { return _taskList; }
            set { SetProperty(ref _taskList, value); }
        }

        private bool _showPopup;
        public bool ShowPopup
        {
            get { return _showPopup; }
            set { SetProperty(ref _showPopup, value); }
        }

        #endregion


        public ToDoPageViewModel(IDataHandlerService dataHandler, IRegionManager regionManager) 
        {
            _dataHandler = dataHandler;
            _regionManager = regionManager;

            LoadTaskTable();
            //TaskList = new ObservableCollection<TaskToDo>
            //{
            //    new TaskToDo { Id = 1, IsCompleted = true, Name = "A", Datetime = "2022" },
            //    new TaskToDo { Id = 2, IsCompleted = false, Name = "B", Datetime = "2023" },
            //    new TaskToDo { Id = 3, IsCompleted = false, Name = "C", Datetime = "2024" },
            //};

            // Command
            this.DeleteRowCommand = new DelegateCommand<object>(DeleteRow);
            this.AddRowCommand = new DelegateCommand(AddRow);
            this.SaveTaskTableCommand = new DelegateCommand(SaveTaskTable);
            this.OpenDetailRowCommand = new DelegateCommand<object>(OpenDetailRow);
        }

        #region Commands

        public ICommand DeleteRowCommand { get; set; }
        public ICommand AddRowCommand { get; set; }
        public ICommand SaveTaskTableCommand { get; set; }
        public ICommand OpenDetailRowCommand { get; set; }
        #endregion


        #region Methods

        private void DeleteRow(object taskToDo)
        {
            var taskNeedToDelete = (TaskToDo)taskToDo;
            TaskList.Remove(taskNeedToDelete);
        }

        private void AddRow()
        {
            TaskList.Add(new TaskToDo { Id = Guid.NewGuid(), IsCompleted = false, Name = "", Datetime = DateTime.Now.ToString() }); ;
        }
        #endregion

        private void SaveTaskTable()
        {
            try
            {
                string modelFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model");
                if (!Directory.Exists(modelFolderPath))
                {
                    Directory.CreateDirectory(modelFolderPath);
                }

                string filePath = Path.Combine(modelFolderPath, "TaskList.json");
                string json = JsonConvert.SerializeObject(TaskList, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, json);
                MessageBox.Show("Save done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save Fail!", "Save Infor", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool LoadTaskTable()
        {
            string filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                TaskList = JsonConvert.DeserializeObject<ObservableCollection<TaskToDo>>(json);
            }
            else
            {
                TaskList = new ObservableCollection<TaskToDo>();
            }
            return true;
        }

        private void OpenDetailRow(object obj)
        {
            var detailTask = (TaskToDo)obj;
            ShowPopup = true;
        }

        #region FUNCTIONs SUPPORT
        private string GetModelFolderPath()
        {
            // Path to Model
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model");
        }

        private string GetFilePath()
        {
            // Path to tasklist.json
            return Path.Combine(GetModelFolderPath(), "tasklist.json");
        }

        #endregion
    }
}
