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
using projectC.JWT;
using System.Text.RegularExpressions;

namespace projectC.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {


        ProjectContext _context;
        public AdminController(ProjectContext context)
        {
            this._context = context;
        }
        // GET Users
        [HttpGet("User")]
        public IActionResult GetUsers(string token, [FromQuery]User u)
        {

            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            if (RoleId)
            {
                var query = _context.users.OrderBy(m => u.Id);
                return Ok(query);
            }
            else
            {

                return Unauthorized();

            }
        }
        [HttpGet("Images")]
        public IActionResult GetImages(string token, [FromQuery]ImageURL i)
        {

            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            if (RoleId)
            {
                var query = _context.imageURLs.OrderBy(m => i.Id);
                return Ok(query);
            }
            else
            {

                return Unauthorized();

            }
        }
        //Get Products
        [HttpGet("Product")]
        public IActionResult GetProducts(string token, [FromQuery]Product p)
        {

            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            if (RoleId)
            {
                var query = _context.products.OrderBy(m => p.Id);
                return Ok(query);
            }
            else
            {

                return Unauthorized();

            }
        }

        //Create user
        [HttpPost("User/Add")]
        public IActionResult CreateUser(string token, [FromBody]User u, string name, string lastname, string age, string password, string gender, string streetname, string email, string housenumber, string addition, string postalcode, string city, string phonenumber)
        {

            bool RoleId = JWTValidator.RoleIDTokenValidation(token);

            if (RoleId)
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

                if (ModelState.IsValid)
                {
                    _context.users.Add(u);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(ModelState);
                }

                return Ok("Account Created.");
            }
            else
            {

                return Unauthorized();

            }

        }

        //Edit User
        [HttpPut("User/Edit/{userid}")]
        public IActionResult Update(string token, int userid, [FromBody]User user)
        {


            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            var edit = _context.users.Find(userid);
            if (RoleId)
            {
                if (user.Name != null)
                {
                    edit.Name = user.Name;
                }
                else
                {
                    edit.Name = edit.Name;
                }
                if (user.LastName != null)
                {
                    edit.LastName = user.LastName;
                }
                else
                {
                    edit.LastName = edit.LastName;
                }
                if (user.Age != null)
                {
                    edit.Age = user.Age;
                }
                else
                {
                    edit.Age = edit.Age;
                }
                if (user.Gender != null)
                {
                    edit.Gender = user.Gender;
                }
                else
                {
                    edit.Gender = edit.Gender;
                }
                if (user.Password != null)
                {
                    edit.Password = user.Password;
                }
                else
                {
                    edit.Password = edit.Password;
                }
                if (user.Street_Name != null)
                {
                    edit.Street_Name = user.Street_Name;
                }
                else
                {
                    edit.Street_Name = edit.Street_Name;
                }
                if (user.email != null)
                {
                    edit.email = user.email;
                }
                else
                {
                    edit.email = edit.email;
                }
                if (user.House_Number != null)
                {
                    edit.House_Number = user.House_Number;
                }
                else
                {
                    edit.House_Number = edit.House_Number;
                }
                if (user.Addition != null)
                {
                    edit.Addition = user.Addition;
                }
                else
                {
                    edit.Addition = edit.Addition;
                }
                if (user.Postalcode != null)
                {
                    edit.Postalcode = user.Postalcode;
                }
                else
                {
                    edit.Postalcode = edit.Postalcode;
                }
                if (user.City != null)
                {
                    edit.City = user.City;
                }
                else
                {
                    edit.City = edit.City;
                }
                if (user.Telephone_Number != null)
                {
                    edit.Telephone_Number = user.Telephone_Number;
                }
                else
                {
                    edit.Telephone_Number = edit.Telephone_Number;
                }

                //Check for potential errors
                bool DupeMail = _context.users.Any(Dupe => Dupe.email == user.email);
                bool PhoneCheck = _context.users.Any(CheckPhone => CheckPhone.Telephone_Number == user.Telephone_Number);


                //Criteria check
                if (DupeMail)
                {
                    return BadRequest("Email bestaat niet of is al in gebruik");
                }
                if (PhoneCheck)
                {
                    return BadRequest("Telefoon nummer bestaat niet of is al in gebruik");
                }
                if (DupeMail == false && PhoneCheck == false)
                {
                    if (ModelState.IsValid)
                    {
                        _context.users.Update(edit);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }

                return Ok("Account edited");

            }
            else
            {

                return Unauthorized();

            }
        }
        //Add product
        [HttpPost("Product/Add")]
        public IActionResult ProductAdd(string token, [FromBody]Product p, User u, string name, string Description, float price, string FirstIMG, int stock)
        {


            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            if (RoleId)
            {
                var ProductData = from Product in _context.products
                                  where (name == p.Name &&
                                  Description == p.Description &&
                                  price == p.Price &&
                                  FirstIMG == p.FirstImg &&
                                  stock == p.Stock)
                                  select p;

                if (ModelState.IsValid)
                {
                    _context.products.Add(p);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(ModelState);
                }

                return Ok("Product added");
            }
            else
            {

                return Unauthorized();

            }

        }

        //Add images
        [HttpPost("Product/Add/Images/{productid}")]
        public IActionResult AddImage(string token, [FromBody]List<ImageURL> imageURLs, int productid)
        {
            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            if (RoleId)
            {

                if (ModelState.IsValid)
                {
                    foreach (var item in imageURLs)
                    {
                        ImageURL imageURL = new ImageURL();
                        imageURL.url = item.url;
                        imageURL.ProductId = productid;
                        _context.Add(imageURL);
                    }
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(ModelState);
                }

                return Ok("Images Added");
            }

            return Unauthorized();
        }

        [HttpPut("Product/Edit/{productid}")]
        public IActionResult ProductEdit(string token, int productid, [FromBody]Product p)
        {
            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            var edit = _context.products.Find(productid);
            if (RoleId)
            {
                if (p.Name != null)
                {
                    edit.Name = p.Name;
                }
                else
                {
                    edit.Name = edit.Name;
                }
                if (p.Description != null)
                {
                    edit.Description = p.Description;
                }
                else
                {
                    edit.Description = edit.Description;
                }
                if(p.Price>0){
                edit.Price = p.Price;
                } else {
                    edit.Price = edit.Price;
                }
                if (p.FirstImg != null)
                {
                    edit.FirstImg = p.FirstImg;
                }
                else
                {
                    edit.FirstImg = edit.FirstImg;
                }
                if(p.Stock>0){
                edit.Stock = p.Stock;
                } else {
                    edit.Stock = edit.Stock;
                }

                if (ModelState.IsValid)
                {
                    _context.products.Update(edit);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(ModelState);
                }

                return Ok("Product updated");
            }

            return Unauthorized();

        }

        //Delete user
        [HttpDelete("User/Delete/{userid}")]
        public IActionResult DeleteUser(string token, int userid, [FromBody]User user)
        {
            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            if (RoleId)
            {
                var remove = (from u in _context.users
                              where u.Id == userid
                              select u).FirstOrDefault();

                if (remove != null)
                {
                    _context.users.Remove(remove);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {

                    return NotFound();

                }

            }

            return Unauthorized();

        }

        //Delete product
        [HttpDelete("Product/Delete/{productid}")]
        public IActionResult DeleteProduct(string token, int productid, [FromBody]User u)
        {
            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            if (RoleId)
            {
                var remove = (from p in _context.products
                              where productid == p.Id
                              select p).FirstOrDefault();

                if (remove != null)
                {
                    _context.products.Remove(remove);
                    _context.SaveChanges();
                    return Ok("Product Deleted");
                }
                else
                {

                    return NotFound();

                }
            }

            return Unauthorized();

        }

        //Delete images
        [HttpDelete("Product/Delete/Images/{imageid}")]
        public IActionResult DeleteImage(string token, int imageid)
        {
            bool RoleId = JWTValidator.RoleIDTokenValidation(token);
            if (RoleId)
            {

                var remove = (from i in _context.imageURLs
                              where imageid == i.Id
                              select i).FirstOrDefault();

                if (remove != null)
                {
                    _context.imageURLs.Remove(remove);
                    _context.SaveChanges();
                    return Ok("Product Deleted");
                }
                else
                {

                    return NotFound();

                }
            }

            return Unauthorized();

        }

        [HttpGet("Status")]
        public Boolean CheckAdminStatus(string token)
        {
            bool Validation = JWTValidator.RoleIDTokenValidation(token);
            return Validation;
        }
    }
}