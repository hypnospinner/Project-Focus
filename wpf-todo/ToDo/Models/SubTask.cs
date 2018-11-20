using System;
using System.ComponentModel;

namespace ToDo.Models
{
    public class SubTask : INotifyPropertyChanged
    {
        private string _subTaskTitle;
        private bool _isDone;

        public SubTask(string title, Action<SubTask> deleteSubTask, Action<SubTask> updateTask)
        {
            DeleteSubTask = new RelayCommand(_ => deleteSubTask(this), _ => true);
            UpdateSubTask = new RelayCommand(_ => updateTask(this), _ => true);
            SubTaskTitle = title;
        }

        public RelayCommand DeleteSubTask
        {
            get; private set;
        }

        public RelayCommand UpdateSubTask
        {
            get; private set;
        }

        public int Id
        {
            get; set;
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
