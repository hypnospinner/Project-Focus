using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Collections;
using Newtonsoft.Json;
using System.IO;

namespace ToDo.Data
{
    public class DataLayer
    {
        NpgsqlConnection conn;

        public void start()
        {
            conn = new NpgsqlConnection("Server=127.0.0.1;Port=5433;User Id=postgres;Password=database;Database=ToDo;");
        }

        public int insert(string query)
        {
            conn.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            int id;
            try
            {
                int.TryParse(command.ExecuteScalar().ToString(), out id);
            }
            finally
            {
                conn.Close();
            }

            return id;
        }

        public class DbSubTask
        {
            public string Title { get; set; }

            public int Id { get; set; }

            public bool IsDone { get; set; }
        }

        public class DbTaskList
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public IEnumerable<DbTask> Tasks { get; set; }
        }

        public class DbTask
        {
            public string Title { get; set; }

            public int Id { get; set; }

            public int ListId { get; set; }

            public string Description { get; set; }

            public string Date { get; set; }

            public bool IsDone { get; set; }

            public IEnumerable<DbSubTask> SubTasks { get; set; }
        }

        public IEnumerable<DbTaskList> GetMockTaskLists()
        {
            string jsonString;
            using (var reader = new StreamReader("mock.json"))
                jsonString = reader.ReadToEnd();
            var taskLists = JsonConvert.DeserializeObject<DbTaskList[]>(jsonString);
            return taskLists;
        }

        public void SaveMockTaskLists(IEnumerable<DbTaskList> taskLists)
        {
            var jsonString = JsonConvert.SerializeObject(taskLists);
            File.WriteAllText("mock.json", jsonString);
        }

        public IEnumerable<DbTaskList> GetTaskLists()
        {
            var readTasksSql = "SELECT title, id_list, id, description, deadline, is_done FROM task";
            var tasksGrouped = ReadEntities(readTasksSql,
                r =>
                {
                    var task = new DbTask();
                    task.Title = r.GetString(0);
                    task.ListId = r.GetInt32(1);
                    task.Id = r.GetInt32(2);
                    task.Description = r.GetValue(3).ToString();
                    task.Date = r.GetValue(4).ToString();
                    task.IsDone = r.GetBoolean(5);
                    return task;
                }).ToArray().GroupBy(x => x.ListId);

            var readTaskListsSql = "SELECT title, id FROM tasks_list";
            var taskListsSeq = ReadEntities(readTaskListsSql,
                r =>
                {
                    var taskList = new DbTaskList();
                    taskList.Name = r.GetString(0);
                    taskList.Id = r.GetInt32(1);
                    return taskList;
                }).ToArray();
            return taskListsSeq.Select(lst => 
            {
                var tasksGroup = tasksGrouped.FirstOrDefault(x => x.Key == lst.Id);
                lst.Tasks = tasksGroup == null? new DbTask[] { } : tasksGroup.ToArray();
                return lst;
            }).ToArray();
        }

        public void update(string query)
        {
            conn.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            int rowsaffected;
            try
            {
                rowsaffected = command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        private IEnumerable<T> ReadEntities<T>(string query, Func<NpgsqlDataReader, T> getEntity)
        {
            conn.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            try
            {
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    yield return getEntity(dr);
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public void delete(string query)
        {
            conn.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            int rowsaffected;
            try
            {
                rowsaffected = command.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public ArrayList select(string query)
        {
            conn.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            ArrayList array = new ArrayList();
            try
            {
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        array.Add(dr[i]);
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return array;
        }

    }

}