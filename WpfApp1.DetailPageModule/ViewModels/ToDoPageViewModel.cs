using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        #endregion


        public ToDoPageViewModel(IDataHandlerService dataHandler, IRegionManager regionManager) 
        {
            _dataHandler = dataHandler;
            _regionManager = regionManager;

            TaskList = new ObservableCollection<TaskToDo>
            {
                new TaskToDo { Id = 1, IsCompleted = true, Name = "A", Datetime = "2022" },
                new TaskToDo { Id = 2, IsCompleted = false, Name = "B", Datetime = "2023" },
                new TaskToDo { Id = 3, IsCompleted = false, Name = "C", Datetime = "2024" },
            };

            // Command
            this.DeleteRowCommand = new DelegateCommand<object>(DeleteRow);
            this.AddRowCommand = new DelegateCommand(AddRow);
        }

        #region Commands

        public ICommand DeleteRowCommand { get; set; }
        public ICommand AddRowCommand { get; set; }
        #endregion


        #region Methods

        private void DeleteRow(object taskToDo)
        {
            var taskNeedToDelete = (TaskToDo)taskToDo;
            TaskList.Remove(taskNeedToDelete);
        }

        private void AddRow()
        {
            TaskList.Add(new TaskToDo { Id = 0, IsCompleted = false, Name = "", Datetime = DateTime.Now.ToString() }); ;
        }
        #endregion
    }
}
