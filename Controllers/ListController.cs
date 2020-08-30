using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Models
{
    [Route("api/lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(ListDataBaseService.ShowAllLists());
        }

        [HttpPost("{listname}")]
        public void Post(string listname)
        { 
            ListDataBaseService.CreateNewList(listname);
        }

        [HttpPatch("{listname}/{newname}")]
        public void Patch(string listname, string newname)
        {
            ListDataBaseService.RenameList(listname, newname);
        }

        [HttpDelete("{listname}")]
        public void Delete(int id, string listname)
        {
            ListDataBaseService.RemoveList(listname);
        }
    }
}
