using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ToDo.Models;

namespace ToDo.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private TaskList selectedTaskList;

        public ApplicationViewModel()
        {
            TaskLists = new ObservableCollection<TaskList>
            {
                new TaskList("Tasklist 1"),
                new TaskList("Tasklist 2"),
                new TaskList("Tasklist 3"),
            };
        }

        public ObservableCollection<TaskList> TaskLists { get; set; }

        public TaskList SelectedTaskList
        {
            get { return selectedTaskList; }
            set
            {
                selectedTaskList = value;
                OnPropertyChanged("SelectedTaskList");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}