using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ToDo.Models
{
    public class Task : INotifyPropertyChanged
    {
        private string _taskTitle;
        private bool _isDone;
        private string _taskNote;
        private RelayCommand _addSubTask;
        private string _newSubTaskName;
        private SubTask _selectedSubTask;
        private ObservableCollection<SubTask> _subTasks;

        public Task(string title, Action<Task> deleteTask)
        {
            DeleteTask = new RelayCommand(_ => deleteTask(this), _ => true);
            TaskTitle = title;
            SubTasks = new ObservableCollection<SubTask>();
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
        public RelayCommand AddSubTask
        {
            get {
                return _addSubTask ?? (_addSubTask = new RelayCommand(
                    obj => {
                        SubTask newSubTask = new SubTask(NewSubTaskName, t => SubTasks.Remove(t));
                        SubTasks.Add(newSubTask);
                        SelectedSubTask = newSubTask;
                    }));
            }
        }
        public string NewSubTaskName
        {
            get => _newSubTaskName;
            set
            {
                _newSubTaskName = value;
                OnPropertyChanged("NewSubTaskName");
            }
        }

        public SubTask SelectedSubTask
        {
            get => _selectedSubTask;
            set
            {
                _selectedSubTask = value;
                OnPropertyChanged("SelectedSubTask");
            }
        }

        public ObservableCollection<SubTask> SubTasks { get => _subTasks; set => _subTasks = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
