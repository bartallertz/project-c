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

namespace projectC.Controllers
{
    [Route("api/[controller]")]
    public class FavouritesController : Controller
    {
        ProjectContext _context;
        public FavouritesController(ProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable Get()
        {
            var result = (from u in _context.users
                          let a_Products =
                          (from a_b in _context.favourites
                           from b in _context.products
                           where a_b.UserId == u.Id && a_b.ProductId == b.Id
                           select b).ToArray()

                          select new
                          {
                              Products = a_Products
                          });
            return result;
        }

        [HttpGet("MyFavourites")]
        public IQueryable Get(string token)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id = JWTValidator.TokenValidation(token);

            var result = from u in _context.users
                         from p in _context.products
                         from u_p in _context.favourites
                         where u.Id == id && u_p.UserId == id && u_p.ProductId == p.Id
                         select p;
            return result;
        }

        [HttpGet("MyFavourites/{id2}")]
        public Boolean Get(string token, int id2)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id1 = JWTValidator.TokenValidation(token);

            var result = (from a_b in _context.favourites
                          where a_b.UserId == id1 && a_b.ProductId == id2
                          select a_b).Any();
            return result;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Favourite f)
        {
            if (f == null)
            {
                return NoContent();
            }
            else
            {
                _context.Add(f);
                _context.SaveChanges();

                return Ok();
            }
        }
        // DELETE api/Favourite/1
        [HttpDelete("{userId}/{productId}")]
        public void Delete(int userId, int productId)
        {
            var remove = (from a_b in _context.favourites
                          where a_b.UserId == userId && a_b.ProductId == productId
                          select a_b).FirstOrDefault();

            if (remove != null)
            {
                _context.favourites.Remove(remove);
                _context.SaveChanges();
            }
        }
    }
}
