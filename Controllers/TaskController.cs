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
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public static DatabaseService dataBaseS;

        static TaskController()
        {
            dataBaseS = new DatabaseService();
        }
        // GET: api/tasks
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(dataBaseS.Select());
        }

        // POST api/tasks
        [HttpPost("")]
        public IActionResult Post(Task task)
        {
            return Ok(dataBaseS.Create(task));
        }
    }
}
