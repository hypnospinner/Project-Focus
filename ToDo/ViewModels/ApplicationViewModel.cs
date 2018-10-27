using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ToDo.Models;
using System.Windows.Input;

namespace ToDo.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private TaskList selectedTaskList;
        private RelayCommand _addTaskList;
        private RelayCommand _addTask;
        private string _newTaskListName;
        private string _newTaskName;
        public ObservableCollection<TaskList> TaskLists { get; set; }

        public ApplicationViewModel()
        {
            TaskLists = new ObservableCollection<TaskList>();
        }

        public TaskList SelectedTaskList
        {
            get { return selectedTaskList; }
            set
            {
                selectedTaskList = value;
                OnPropertyChanged("SelectedTaskList");
            }
        }

        public string NewTaskListName
        {
            get => _newTaskListName;
            set
            {
                _newTaskListName = value;
                OnPropertyChanged("NewTaskListName");
            }
        }

        public RelayCommand AddTaskList
        {
            get
            {
                return _addTaskList ??
                    (_addTaskList = new RelayCommand(obj =>
                   {
                       if (NewTaskListName != "")
                       {
                           TaskList taskList = new TaskList(NewTaskListName, tL => TaskLists.Remove(tL));
                           TaskLists.Add(taskList);
                           SelectedTaskList = taskList;
                           NewTaskListName = "";
                       }
                   }));
            }
        }

        public RelayCommand AddTask
        {
            get
            {
                return _addTask ?? (_addTask = new RelayCommand(obj =>
                {
                    Task task = new Task(NewTaskName, t => SelectedTaskList.Tasks.Remove(t));
                    SelectedTaskList?.Tasks.Add(task);
                    NewTaskName = "";
                }));
            }
        }

        public string NewTaskName
        {
            get => _newTaskName;
            set
            {
                _newTaskName = value;
                OnPropertyChanged("NewTaskName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}