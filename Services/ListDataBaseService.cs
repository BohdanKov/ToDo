using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class ListDataBaseService
    {
        public ListDataBaseService(string listname)
        {
            DatabaseService.SetUpDatabase(listname);
        }

        // GET
        public static ArrayList ShowAllLists()
        {
            ArrayList table = new ArrayList();
            DataTable temp = new DataTable();
            using (var connection = DatabaseService.ProvideConnection())
            {
                var reader = DatabaseService.BuildSqlCommand(connection, $"SELECT name FROM sqlite_master WHERE type = 'table' != '%sqlite_sequence%'").ExecuteReader();
                temp.Load(reader);

            }
            foreach (DataRow row in temp.Rows)
            {
                table.Add(row.ItemArray[0].ToString());
            }
            return table;

        }

        // POST
        public static void CreateNewList(string listname)
        {
            DatabaseService.SetUpDatabase(listname);
        }

        // PATCH
        public static void RenameList(string listname, string newname)
        {
            using (var connection = DatabaseService.ProvideConnection())
            {
                DatabaseService.BuildSqlCommand(connection, $"ALTER TABLE {listname} RENAME TO {newname}").ExecuteNonQuery();
            }
        }

        // DELETE 
        public static void RemoveList(string listname)
        {
            using (var connection = DatabaseService.ProvideConnection())
            {
                DatabaseService.BuildSqlCommand(connection, $"DROP TABLE IF EXISTS {listname}").ExecuteNonQuery();
            }
        }




        
    }
}
