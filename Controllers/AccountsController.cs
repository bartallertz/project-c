using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;

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

            //Check credentials
            bool LoginCheck = _context.users.Any(CheckCredentials => CheckCredentials.email == u.email && CheckCredentials.Password == u.Password);


            //Login combo is correct
            if (LoginCheck == true)
            {
                User LoggedUser = (from user in this._context.users
                                   where user.email == u.email && user.Password == u.Password
                                   select user).FirstOrDefault();


                string key = "401b09eab3c013d4c37591abd3e44453b954SALEEM0812e1081c39b740293f765eae731f5a65ed51";
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var LoginCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var header = new JwtHeader(LoginCredentials);

                var payload = new JwtPayload
                {
                    { "EMAIL", LoggedUser.email},
                    { "ID", LoggedUser.Id.ToString()},
                    { "ROLE ID", LoggedUser.RoleId.ToString()}
                };

                var secToken = new JwtSecurityToken(header, payload);
                var handler = new JwtSecurityTokenHandler();
                var tokenString = handler.WriteToken(secToken);
                string PayloadString = tokenString.Split('.')[1];

                return Ok(PayloadString);
            }

            //Login combo does not exist
            else
            {
                return Unauthorized();
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

