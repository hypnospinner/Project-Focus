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

        public TaskList(string title)
        {
            TaskListTitle = title;
            Tasks = new ObservableCollection<Task>
            {
            //    new Task("task 1 title of " + _taskListTitle),
            //    new Task("task 2 title of " + _taskListTitle),
            //    new Task("task 3 title of " + _taskListTitle)
            };
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
