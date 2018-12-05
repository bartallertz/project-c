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
        [HttpGet("{id}", Name = "GetUser")]
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
                return Ok();
            }

            //Login combo does not exist
            else
            {
                return Unauthorized();
            }
        }

       
        // [HttpPut("Edit")]
        // public void Put(int Id, [FromBody]User users, string password, string streetname, string email, int housenumber, string addition, string postalcode, string city, string phonenumber)
        //     {
        //         var Edit = from user in _context.users
        //                     where (password == user.Password &&
        //                     streetname == user.Street_Name &&
        //                     email == user.email &&
        //                     housenumber == user.House_Number &&
        //                     addition == user.Addition &&
        //                     postalcode == user.Postalcode &&
        //                     city == user.City &&
        //                     phonenumber == user.Telephone_Number && user.Id == Id)
        //                     select user;
                            
        //                     _context.Update(Edit);
        //                     _context.SaveChanges();
        //     }

       [HttpPut("Edit/{id}")]
        public IActionResult Update(int id, [FromBody]User user)
        {
            var edit = _context.users.Find(id);
            if (edit == null)
            {
                return NotFound();
            }

            edit.Password = user.Password;
            edit.Street_Name = user.Street_Name;
            edit.email = user.email;
            edit.House_Number = user.House_Number;
            edit.Addition = user.Addition;
            edit.Postalcode = user.Postalcode;
            edit.City = user.City;
            edit.Telephone_Number = user.Telephone_Number;

            _context.users.Update(edit);
            _context.SaveChanges();
        return CreatedAtRoute("GetUser", new { id = edit.Id }, edit);

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
            if (DupeMail)
            {
                return BadRequest("Email adress already exists");
            }
            if (PhoneCheck)
            {
                return BadRequest("Phone number already exists");
            }
            if (DupeMail == false && PhoneCheck == false)
            {
                u.RoleId = 1;
                _context.Add(u);
                _context.SaveChanges();
                return Ok("Account created");
            }
            else
            {
                return NoContent();
            }
        }
    }
}

