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
            var result = from m in this._context.users orderby m.Id select m;

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

        //Post Accounts/Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User u, string email, string password)
        {
            //Upload credentials
            var credentials = from user in _context.users
                              where email == u.email && password == u.Password
                              select user;

            //Check credentials
            bool LoginCheck = _context.users.Any(CheckCredentials => CheckCredentials.email == u.email && CheckCredentials.Password == u.Password);

            //Login combo is correct
            if (LoginCheck == true)
            {
                Console.WriteLine("Login Suc6");
                return Ok();
            }

            //Login combo does not exist
            else
            {
                Console.WriteLine("no account");
                return NoContent();
            }
        }


        //Post api/Accounts/Register
        [HttpPost("Register")]
        public IActionResult Register([FromBody]User u, string name, string lastname, int age, string password, string gender, string streetname, string email, int housenumber, string addition, string postalcode, string city, string phonenumber)
        {
            var UserData = from user in _context.users
                           where (name == u.Name &&
                           lastname == u.LastName &&
                           age == u.Age &&
                           password == u.Password &&
                           gender == u.Gender &&
                           streetname == u.Street_Name &&
                           email == u.email &&
                           housenumber == u.House_Number &&
                           addition == u.Addition &&
                           postalcode == u.Postalcode &&
                           city == u.City &&
                           phonenumber == u.Telephone_Number)
                           select u;


            //Check for potential errors
            bool DupeMail = _context.users.Any(Dupe => Dupe.email == u.email);
            bool PhoneCheck = _context.users.Any(CheckPhone => CheckPhone.Telephone_Number == u.Telephone_Number);
            

            //Criteria check
            if (DupeMail == true)
            {
                Console.WriteLine("email dupe");
                return NoContent();
            }
            if (PhoneCheck == true)
            {
                Console.WriteLine("Phonenumber already bestaan vieze scammer");
                return NoContent();
            }
            if ( DupeMail == false && PhoneCheck == false)
            {
                Console.WriteLine("account created");
                u.RoleId = 1;             
                _context.Add(u);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                Console.WriteLine("something bad happened");
                return NoContent();
            }
        }
    }

    // // PUT api/values/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody]string value)
    // {
    // }

    // // DELETE api/values/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
}

