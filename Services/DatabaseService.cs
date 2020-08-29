using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using TodoList.Models;
using TodoList.Controllers;



namespace TodoList.Models
{
    public class DatabaseService
    {
        public DatabaseService()
        {
            SetUpDatabase();
        }

        public void SetUpDatabase()
        {
            using (var connection = ProvideConnection())
            {
                BuildSqlCommand(connection, "CREATE TABLE IF NOT EXISTS toDo(id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(50), done TINYINT)").ExecuteNonQuery();
            }
        }

        public SqliteConnection  ProvideConnection()
        {
            var conBuilder = new SqliteConnectionStringBuilder { DataSource = "./todos.db" };
            return new SqliteConnection(conBuilder.ConnectionString);
        }

        public SqliteCommand BuildSqlCommand(SqliteConnection connection, string query)
        {
            var cmd = connection.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = query;
            return cmd;
        }

        internal Task Create(Task task)
        {
            using (var connection = ProvideConnection())
            {
                BuildSqlCommand(connection, $"INSERT INTO toDo(name, done) VALUES('{task.name}','{task.done}')").ExecuteNonQuery();
            }
            return task;
        }

        public List<Task> Select()
        {
            var listOfTasks = new List<Task>();
            using (var connection = ProvideConnection())
            {
                using (var reader = BuildSqlCommand(connection, "SELECT * FROM toDo").ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfTasks.Add(new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2)));
                    }
                }
            }
            return listOfTasks;
        }

    }
}
