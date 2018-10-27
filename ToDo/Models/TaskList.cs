using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDo.Models
{
    class TaskList : INotifyPropertyChanged
    {
        private Task _currentTask;

        private ObservableCollection<Task> _tasks;

        internal Task CurrentTask
        {
            get => _currentTask;
            set
            {
                _currentTask = value;
                OnPropertyChange("CurrentTask");
            }
        }
        internal ObservableCollection<Task> Tasks { get; set; }

        public TaskList()
        {
            _tasks = new ObservableCollection<Task>
            {
                new Task { TaskDescription = "Task_1", TaskNote = "do somthing, hard code!!!", IsDone = false },
                new Task { TaskDescription = "Task_1", TaskNote = "do somthing, hard code!!!", IsDone = false },
                new Task { TaskDescription = "Task_1", TaskNote = "do somthing, hard code!!!", IsDone = false },
                new Task { TaskDescription = "Task_1", TaskNote = "do somthing, hard code!!!", IsDone = false },
                new Task { TaskDescription = "Task_1", TaskNote = "do somthing, hard code!!!", IsDone = false },
                new Task { TaskDescription = "Task_1", TaskNote = "do somthing, hard code!!!", IsDone = false }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
