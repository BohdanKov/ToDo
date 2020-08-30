using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using TodoList.Models;
using TodoList.Controllers;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace TodoList.Models
{
    public class DatabaseService
    {
        public static void SetUpDatabase(string listname)
        {
            using (var connection = ProvideConnection())
            {
                BuildSqlCommand(connection, $"CREATE TABLE IF NOT EXISTS {listname}(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR(50), Done TINYINT)").ExecuteNonQuery();
            }
        }

        public static SqliteConnection  ProvideConnection()
        {
            var conBuilder = new SqliteConnectionStringBuilder { DataSource = "./todos.db" };
            return new SqliteConnection(conBuilder.ConnectionString);
        }

        public static SqliteCommand BuildSqlCommand(SqliteConnection connection, string query)
        {
            var cmd = connection.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = query;
            return cmd;
        }

        
    
    }   
}
