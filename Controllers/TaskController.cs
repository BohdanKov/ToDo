using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Controllers;
using Microsoft.Data.Sqlite;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Models
{
    [Route("api/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet("{listname}")]
        public void Get(string listname)
        {
            TaskDataBaseService dataBaseS = new TaskDataBaseService(listname);
            dataBaseS.Select(listname);
        }
        
        [HttpPost("{listname}")]
        public void Post(Task task, string listname)
        {
            TaskDataBaseService dataBaseS = new TaskDataBaseService(listname);
            dataBaseS.Create(task, listname);
        }

        [HttpPatch("{listname}/{id}/{done}")]
        public void Patch(int id, bool done, string listname)
        {
            TaskDataBaseService dataBaseS = new TaskDataBaseService(listname);
            dataBaseS.Update(id, done, listname);
        }

        [HttpDelete("{listname}/{id}")]
        public void Delete(int id, string listname)
        {
            TaskDataBaseService dataBaseS = new TaskDataBaseService(listname);
            dataBaseS.Remove(id, listname);
        }

    }
}
