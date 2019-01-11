using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;
using projectC.JWT;

namespace projectC.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartsController : Controller
    {
        ProjectContext _context;

        public ShoppingCartsController(ProjectContext context)
        {
            this._context = context;
        }


        [HttpGet("MyCart")]
        public IQueryable Get(string token)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id = JWTValidator.IDTokenValidation(token);

            var result = from u in _context.users
                         from p in _context.products
                         from u_p in _context.ShoppingCarts
                         where u.Id == id && u_p.ProductId == p.Id
                         select u_p;

            return result;
        }

        // GET api/values/5
        [HttpGet("MyCart/{ProductId}")]
        public Boolean Get(string token, int ProductId)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id1 = JWTValidator.IDTokenValidation(token);
            var result = (from a_b in _context.ShoppingCarts
                          where a_b.UserId == id1 && a_b.ProductId == ProductId
                          select a_b).Any();
            return result;
        }

         // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ShoppingCart s, string token)
        {
            int id = JWTValidator.IDTokenValidation(token);

            var ProdDate = (from s2 in _context.ShoppingCarts
                            where (id == s2.UserId &&
                                   s.ProductId == s2.ProductId)
                            select s2);

            if (s.UserId != id)
            {
                Unauthorized();
            }

            //checking for dupe users
            bool DupeUser = _context.ShoppingCarts.Any(dupe => dupe.UserId == id);
            bool DupeProd = _context.ShoppingCarts.Any(dupe => dupe.ProductId == s.ProductId);




            if (DupeUser && DupeProd)
            {
                var UserData = (from s1 in _context.ShoppingCarts where s1.UserId == id && s1.ProductId == s.ProductId select s1.Amount).ToList();

                s.Amount = s.Amount + UserData[0];
                s.UserId = id;
                _context.Update(s);
                _context.SaveChanges();
            }
            else
            {
                s.UserId = id;
                _context.Add(s);
                _context.SaveChanges();
            }


            return Ok();
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5

        [HttpDelete("{productId}")]
        public void DeleteSingle(int productId, string token)
        {
            int id = JWTValidator.IDTokenValidation(token);
            var remove = (from a_b in _context.ShoppingCarts
                          where a_b.UserId == id && a_b.ProductId == productId
                          select a_b).FirstOrDefault();

            if (remove != null)
            {
                _context.ShoppingCarts.Remove(remove);
                _context.SaveChanges();
            }
        }
        [HttpDelete("d")]
        public void DeleteAll(string token)
        {
            int id = JWTValidator.IDTokenValidation(token);
            var remove = (from a_b in _context.ShoppingCarts
                          where a_b.UserId == id
                          select a_b).ToList();

            foreach (var item in remove)
            {
                if (item != null)
                {
                    _context.ShoppingCarts.Remove(item);
                    _context.SaveChanges();
                }
                else
                {
                    Unauthorized();
                }
            }

        }
    }
}