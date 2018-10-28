using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ToDo.Models;

using Newtonsoft.Json;
using System.Windows.Input;
using System.Collections;
using static ToDo.Data.DataLayer;
using System.IO;

namespace ToDo.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private TaskList selectedTaskList;
        private RelayCommand _addTaskList;
        private RelayCommand _addTask;
        private string _newTaskListName;
        private string _newTaskName;
        public ObservableCollection<TaskList> TaskLists { get; set; }

        public ApplicationViewModel()
        {
            //string s = "SELECT title, id FROM tasks_list";            
            //ArrayList a = MainWindow.db.select(s);
            
            TaskLists = new ObservableCollection<TaskList>();

            //var taskLists = MainWindow.db.GetTaskLists();
            string jsonString;
            using (var reader = new StreamReader("mock.json"))
                jsonString = reader.ReadToEnd();
            var taskLists = JsonConvert.DeserializeObject<DbTaskList[]>(jsonString);

            foreach(var taskList in taskLists)
            {
                var taskListVm = new TaskList(taskList.Name, tL => TaskLists.Remove(tL));
                taskListVm.Id = taskList.Id;
                TaskLists.Add(taskListVm);
                foreach(var task in taskList.Tasks)
                {
                    var taskVm = new Task(task.Title, t => SelectedTaskList.Tasks.Remove(t));
                    taskVm.IsDone = task.IsDone;
                    taskVm.TaskNote = task.Description;
                    taskVm.Id = task.Id;
                    taskListVm.Tasks.Add(taskVm);
                }
            }
            //for (int i = 0; i < a.Count; i++)
            //    TaskLists.Add(new TaskList(a[i].[0].ToString(), int.Parse(a[i].[1])));
            // чтение из базы
        }

        public TaskList SelectedTaskList
        {
            get { return selectedTaskList; }
            set
            {
                selectedTaskList = value;
// selectedTaskList.TaskListTitle
                OnPropertyChanged("SelectedTaskList");
            }
        }

        public string NewTaskListName
        {
            get => _newTaskListName;
            set
            {
                _newTaskListName = value;
                OnPropertyChanged("NewTaskListName");
            }
        }

        public RelayCommand AddTaskList
        {
            get
            {
                return _addTaskList ??
                    (_addTaskList = new RelayCommand(obj =>
                   {
                       if (NewTaskListName != "")
                       {
                           TaskList taskList = new TaskList(NewTaskListName, tL => TaskLists.Remove(tL));
                           TaskLists.Add(taskList);
                           SelectedTaskList = taskList;
                           // string s = "INSERT INTO tasks_list(title) VALUES ('" + NewTaskListName.ToString() + "')" + " RETURNING ID";
                           // SelectedTaskList.Id = MainWindow.db.insert(s);
                           NewTaskListName = "";
                       }
                   }));
            }
        }

        public RelayCommand AddTask
        {
            get
            {
                return _addTask ?? (_addTask = new RelayCommand(obj =>
                {
                    Task task = new Task(NewTaskName, t => SelectedTaskList.Tasks.Remove(t));
                    SelectedTaskList.Tasks.Add(task);
                    // string s = "INSERT INTO task(title, id_list) VALUES ('" + NewTaskName.ToString() + "','" + SelectedTaskList.Id + "') RETURNING ID";
                    // task.Id = MainWindow.db.insert(s); 
                    NewTaskName = "";    
                }));
            }
        }

        public string NewTaskName
        {
            get => _newTaskName;
            set
            {
                _newTaskName = value;
                OnPropertyChanged("NewTaskName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}