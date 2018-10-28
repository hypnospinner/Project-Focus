using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ToDo.Models;

using Newtonsoft.Json;
using System.Windows.Input;
using System.Collections;
using static ToDo.Data.DataLayer;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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

        public IEnumerable<DbTaskList> Convert(IEnumerable<TaskList> source)
        {
            return source.Select(x =>
            {
                return new DbTaskList
                {
                    Id = x.Id,
                    Name = x.TaskListTitle,
                    Tasks = x.Tasks.Select(y => new DbTask
                    {
                        Description = y.TaskNote,
                        IsDone = y.IsDone,
                        Id = y.Id,
                        Title = y.TaskTitle,
                        SubTasks = y.SubTasks.Select(z => new DbSubTask
                        {
                            Id = z.Id,
                            Title = z.SubTaskTitle,
                            IsDone = z.IsDone
                        })
                    })
                };
            });
        }

        public ApplicationViewModel()
        {
            //string s = "SELECT title, id FROM tasks_list";            
            //ArrayList a = MainWindow.db.select(s);
            
            TaskLists = new ObservableCollection<TaskList>();

            var taskLists = MainWindow.db.GetMockTaskLists();

            foreach(var taskList in taskLists)
            {
                var taskListVm = new TaskList(taskList.Name, tL =>
                {
                    if(MessageBox.Show("THIS ACTION CANNPT BE UNDONE!", "DELETE", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
                    TaskLists.Remove(tL);
                    MainWindow.db.SaveMockTaskLists(Convert(TaskLists));
                });
                taskListVm.Id = taskList.Id;
                TaskLists.Add(taskListVm);
                foreach(var task in taskList.Tasks)
                {
                    var taskVm = new Task(task.Title, t =>
                    {
                        if (MessageBox.Show("THIS ACTION CANNPT BE UNDONE!", "DELETE", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
                        SelectedTaskList.Tasks.Remove(t);
                        MainWindow.db.SaveMockTaskLists(Convert(TaskLists));
                    }, _ => MainWindow.db.SaveMockTaskLists(Convert(TaskLists)));
                    taskVm.IsDone = task.IsDone;
                    taskVm.TaskNote = task.Description;
                    taskVm.Id = task.Id;
                    taskListVm.Tasks.Add(taskVm);
                    foreach(var subTask in task.SubTasks ?? new DbSubTask[] { })
                    {
                        var subTaskVm = new SubTask(subTask.Title, t =>
                        {
                            SelectedTaskList.SelectedTask.SubTasks.Remove(t);
                            MainWindow.db.SaveMockTaskLists(Convert(TaskLists));
                        }, _ => MainWindow.db.SaveMockTaskLists(Convert(TaskLists)));
                        subTaskVm.Id = subTask.Id;
                        subTaskVm.IsDone = subTask.IsDone;
                        taskVm.SubTasks.Add(subTaskVm);
                    }
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
                           taskList.Id = TaskLists.Max(x => x.Id) + 1;
                           MainWindow.db.SaveMockTaskLists(Convert(TaskLists));
                           // string s = "INSERT INTO tasks_list(title) VALUES ('" + NewTaskListName.ToString() + "')" + " RETURNING ID";
                           // SelectedTaskList.Id = MainWindow.db.insert(s);
                           NewTaskListName = "";
                       }
                   }, _ => !string.IsNullOrWhiteSpace(NewTaskListName)));
            }
        }

        public RelayCommand AddTask
        {
            get
            {
                return _addTask ?? (_addTask = new RelayCommand(obj =>
                {
                    Task task = new Task(NewTaskName, t => SelectedTaskList.Tasks.Remove(t), _ => MainWindow.db.SaveMockTaskLists(Convert(TaskLists)));
                    SelectedTaskList.Tasks.Add(task);
                    task.Id = SelectedTaskList.Tasks.Max(x => x.Id) + 1;
                    MainWindow.db.SaveMockTaskLists(Convert(TaskLists));
                    // string s = "INSERT INTO task(title, id_list) VALUES ('" + NewTaskName.ToString() + "','" + SelectedTaskList.Id + "') RETURNING ID";
                    // task.Id = MainWindow.db.insert(s); 
                    NewTaskName = "";    
                }, _ => !(string.IsNullOrWhiteSpace(NewTaskName) || null == SelectedTaskList)));
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