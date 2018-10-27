//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;

//namespace ToDo.Models
//{
//    class ApplicationViewModel : INotifyPropertyChanged
//    {
//        //private TaskList _currentTaskList;
//        //private ObservableCollection<TaskList> _taskLists;

//        //public ApplicationViewModel()
//        //{
//        //    TaskLists = new ObservableCollection<TaskList>
//        //    {
//        //        new TaskList() { TaskListName = "TaskList_1" },
//        //        new TaskList() { TaskListName = "TaskList_2" },
//        //        new TaskList() { TaskListName = "TaskList_3" },
//        //        new TaskList() { TaskListName = "TaskList_4" },
//        //        new TaskList() { TaskListName = "TaskList_5" },
//        //        new TaskList() { TaskListName = "TaskList_6" }
//        //    };
//        //}

//        //internal TaskList CurrentTaskList
//        //{
//        //    get => _currentTaskList;
//        //    set
//        //    {
//        //        _currentTaskList = value;
//        //        OnPropertyChaged("CurrentTaskList");
//        //    }
//        //}

//        //internal ObservableCollection<TaskList> TaskLists
//        //{
//        //    get => _taskLists;
//        //    set => _taskLists = value;
//        //}

//        //public event PropertyChangedEventHandler PropertyChanged;
//        //public void OnPropertyChaged([CallerMemberName]string propertyName = "")
//        //{
//        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        //}

//        private TaskList selectedTaskList;

//        public ObservableCollection<TaskList> TaskLists { get; set; }
//        public TaskList SelectedTaskList
//        {
//            get { return selectedTaskList; }
//            set
//            {
//                selectedTaskList = value;
//                OnPropertyChanged("SelectedPhone");
//            }
//        }

//        public ApplicationViewModel()
//        {
//            TaskLists = new ObservableCollection<TaskList>
//            {
//            new TaskList { TaskListTitle="Task list 1" },
//            new TaskList { TaskListTitle="Task list 2" },
//            new TaskList { TaskListTitle="Task list 2" },
//            new TaskList { TaskListTitle="Task list 2" }
//            };
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ToDo.Models;

namespace ToDo.ModelsView
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Phone selectedPhone;

        public ObservableCollection<Phone> Phones { get; set; }
        public Phone SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public ApplicationViewModel()
        {
            Phones = new ObservableCollection<Phone>
{
new Phone { Title="iPhone 7", Company="Apple", Price=56000 },
new Phone {Title="Galaxy S7 Edge", Company="Samsung", Price =60000 },
new Phone {Title="Elite x3", Company="HP", Price=56000 },
new Phone {Title="Mi5S", Company="Xiaomi", Price=35000 }
};
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
