using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using TodoList.Controllers;


namespace TodoList.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }

        public Task()
        {
        }

        public Task(int id, string name, bool done)
        {
            this.Id = id;
            this.Name = name;
            this.Done = done;
        }

        

        static Dictionary<int, Task> ToDoList = new Dictionary<int, Task>();

        public static void addTask(Task task)
        {
            task.Id = Task.ToDoList.Count > 0 ? Task.ToDoList.Keys.Max() + 1 : 1; //max needs at least 1 element in dictionary
            Task.ToDoList.Add(task.Id, task);
            
        }

        public static string getTaskListJson()
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return JsonSerializer.Serialize(Task.ToDoList.Select(d => d.Value), jso);
        }
    }
}
