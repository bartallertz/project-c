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

        // GET api/shoppingcarts    
        // id = UserId
        [HttpGet("MyCart")]
        public IQueryable Get(string token)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id = JWTValidator.TokenValidation(token);

            var result = from u in _context.users
                         from p in _context.products
                         from u_p in _context.ShoppingCarts
                         where u.Id == id && u_p.ProductId == p.Id
                         select p;

            return result;
        }

        [HttpPut("MyCart/Buy/{ProductId}")]
        public IActionResult Buy([FromBody] Product p, [FromBody] ShoppingCart S, string token)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id = JWTValidator.TokenValidation(token);

            var result = from B in this._context.ShoppingCarts
            where B.UserId == id && B.ProductId == p.Id
            select B.Amount;
            
            foreach(var item in result)
            {
                p.Stock = p.Stock - item;
            }

            _context.Update(p);
            _context.SaveChanges();
            
            return Ok();
        }

        // GET api/values/5
        [HttpGet("MyCart/{ProductId}")]
        public Boolean Get(string token, int ProductId)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id = JWTValidator.TokenValidation(token);
            var result = (from a_b in _context.ShoppingCarts
                          where a_b.UserId == id && a_b.ProductId == ProductId
                          select a_b).Any();
            return result;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ShoppingCart s)
        {
            if (s == null)
            {
                return NoContent();
            }
            else
            {
                _context.Add(s);
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

        [HttpDelete("MyCart/{productId}")]
        public void RemoveProduct(string token, int productId)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id = JWTValidator.TokenValidation(token);

            var remove = (from a_b in _context.ShoppingCarts
                          where a_b.UserId == id && a_b.ProductId == productId
                          select a_b).FirstOrDefault();

            if (remove != null)
            {
                _context.ShoppingCarts.Remove(remove);
                _context.SaveChanges();
            }
        }
        [HttpDelete("MyCart/Empty")]
        public void EmptyCart(string token)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id = JWTValidator.TokenValidation(token);

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