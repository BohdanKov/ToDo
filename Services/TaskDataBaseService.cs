using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Models
{
    public class TaskDataBaseService
    {
        public TaskDataBaseService(string listname)
        {
            DatabaseService.SetUpDatabase(listname);
        }

        // POST
        public Task Create(Task task, string listname)
        {
            using (var connection = DatabaseService.ProvideConnection())
            {
                DatabaseService.BuildSqlCommand(connection, $"INSERT INTO {listname}(Name, Done) VALUES('{task.Name}','{task.Done}')").ExecuteNonQuery();
            }
            return task;
        }

        // GET
        internal List<Task> Select(string listname)
        {
            var listOfTasks = new List<Task>();
            using (var connection = DatabaseService.ProvideConnection())
            {
                using (var reader = DatabaseService.BuildSqlCommand(connection, $"SELECT * FROM {listname}").ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfTasks.Add(new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2)));
                    }
                }
            }
            return listOfTasks;
        }

        // PATCH
        internal void Update(int id, bool done, string listname)
        {
            using (var connection = DatabaseService.ProvideConnection())
            {
                DatabaseService.BuildSqlCommand(connection, $"UPDATE {listname} SET done = {done} WHERE id = {id}").ExecuteNonQuery();
            }
        }

        // DELETE
        internal void Remove(int id, string listname)
        {
            using (var connection = DatabaseService.ProvideConnection())
            {
                DatabaseService.BuildSqlCommand(connection, $"DELETE FROM {listname} WHERE id = {id}").ExecuteNonQuery();
            }
        }
    }
}
