using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;
using System.Text;

namespace projectC.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        ProjectContext _context;
        public AccountsController(ProjectContext context)
        {
            this._context = context;
        }
        // GET api/values
        [HttpGet]
        public IQueryable<User> Get()
        {
            var result = from m in this._context.users select m;

            return result;
        }



        // GET api/values/5
        [HttpGet("{id}")]
        public IQueryable<User> GetAccount(int id)
        {
            //get a specific user
            var result = from m in this._context.users where m.Id == id select m;

            return result;
        }

        //Post api/Accounts
        [HttpPost]
        public IActionResult Register([FromBody]User u)
        {


            //  var UserRole = (u , u.RoleId = 1);

            //register a new User
            if (u == null)
            {
                return NoContent();
            }
            else
            {
               
                _context.Add(u);
                _context.SaveChanges();

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
