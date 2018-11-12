using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;
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
          // GET api/values/5
        [HttpGet("{id}")]
        public IQueryable<User> GetAccount(int id)
        {   //lijst van alle gebruikers
            var result = from m in this._context.users where m.Id == id select m;

            return result;
        }

        //Post api/Accounts
        [HttpPost]
        public IActionResult Post([FromBody] User u)
        {
            //als u null is return null statement. anders add u aan databse 
            if(u == null)
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
