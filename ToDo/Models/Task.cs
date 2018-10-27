using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDo.Models
{
    class Task : INotifyPropertyChanged
    {
        // properties
        private string _taskDescription;        // name of task        
        private string _taskNote;               // some notes for task
        private bool _isDone;                   // weather wask is done or note

        // fields
        public string TaskDescription
        {
            get => _taskDescription;
            set
            {
                _taskDescription = value;
                OnPropertyChange("Task Description");
            }
        }
        public string TaskNote
        {
            get => _taskNote;
            set
            {
                _taskNote = value;
                OnPropertyChange("TaskNote");
            }
        }
        public bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                OnPropertyChange("IsDone");
            }
        }

        // methods
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
