using System;
using System.ComponentModel;


namespace ToDo.Models
{
    public class Task : INotifyPropertyChanged
    {
        private string _taskTitle;
        private bool _isDone;
        private string _taskNote;

        public Task(string title, Action<Task> deleteTask)
        {
            DeleteTask = new RelayCommand(_ => deleteTask(this), _ => true);
            TaskTitle = title;
        }

        public string TaskTitle
        {
            get => _taskTitle;
            set
            {
                _taskTitle = value;
                OnPropertyChanged("TaskTitle");
            }
        }

        public bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }

        public string TaskNote
        {
            get => _taskNote;
            set
            {
                _taskNote = value;
                OnPropertyChanged("TaskNote");
            }
        }

        public RelayCommand DeleteTask
        {
            get; private set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
