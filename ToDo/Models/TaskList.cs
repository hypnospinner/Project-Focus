using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ToDo.Models
{
    class TaskList : INotifyPropertyChanged
    {
        private ObservableCollection<Task> _tasks;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
