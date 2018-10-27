using System.ComponentModel;

namespace ToDo.Models
{
    public class Task : INotifyPropertyChanged
    {
        private string _taskTitle;
        private bool _isDone;

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
