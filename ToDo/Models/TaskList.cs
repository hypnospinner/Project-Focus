using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;

namespace ToDo.Models
{
    public class TaskList : INotifyPropertyChanged
    {
        private Task _selectedTask;
        private string _taskListTitle;
        private ObservableCollection<Task> _tasks;

        public TaskList(string title, Action<TaskList> deleteTaskList)
        {
            DeleteTaskList = new RelayCommand(_ => deleteTaskList(this), _ => true);
            TaskListTitle = title;
            Tasks = new ObservableCollection<Task>();
        }

        public string TaskListTitle
        {
            get { return _taskListTitle; }
            set
            {
                _taskListTitle = value;
                OnPropertyChanged("TaskListTitle");
                Console.WriteLine(value);
            }
        }

        public ObservableCollection<Task> Tasks { get => _tasks; set => _tasks = value; }

        public Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        public RelayCommand DeleteTaskList
        {
            get; private set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
