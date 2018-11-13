using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;


namespace projectC.Controllers
{
    [Route("api/[controller]")]
    public class LogInController : Controller
    {
        ProjectContext _context;
        public LogInController(ProjectContext context)
        {
            _context = context;
        }

        // GET api/LogIn
        [HttpGet]
        public IQueryable<User> Get()
        {
            var result = from m in this._context.users select m;

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IQueryable<User> Get(int id)
        {
            var result = from m in this._context.users select m;

            return result;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]User u, string password, string name)
        {

            //lijst maken van Users [Name, password]
            var userData = this._context.users.Select(m => m).ToList();



            if (u.Name != name | u.Password != password)
            {
                return NoContent();
            }
            else
            {
                return Ok();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
