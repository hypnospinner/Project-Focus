using System;
using System.ComponentModel;

namespace ToDo.Models
{
    public class SubTask : INotifyPropertyChanged
    {
        private string _subTaskTitle;
        private bool _isDone;

        public SubTask(string title, Action<SubTask> deleteSubTask)
        {
            DeleteSubTask = new RelayCommand(_ => deleteSubTask(this), _ => true);
            SubTaskTitle = title;
        }

        public RelayCommand DeleteSubTask
        {
            get; private set;
        }
        public string SubTaskTitle
        {
            get => _subTaskTitle;
            set
            {
                _subTaskTitle = value;
                OnPropertyChanged("SubTaskTitle");
            }
        }
        public bool IsDone
        {
            get => _isDone;
            set
            {
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
