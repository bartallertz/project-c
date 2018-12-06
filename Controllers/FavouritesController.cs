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

        [HttpGet("MyFavourites/{ProductId}")]
        public Boolean Get(string token, int ProductId)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id1 = JWTValidator.TokenValidation(token);

            var result = (from a_b in _context.favourites
                          where a_b.UserId == id1 && a_b.ProductId == ProductId
                          select a_b).Any();
            return result;
        }

        // POST api/values
        [HttpPost("PostFavourite")]
        public IActionResult Post([FromBody]Favourite f, string token)
        {
            if (f == null)
            {
                return NoContent();
            }
            else
            {
                if (token == null)
                {
                    token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
                }

                int id = JWTValidator.TokenValidation(token);

                f.UserId = id;
                _context.Add(f);
                _context.SaveChanges();

                return Ok();
            }
        }
        // DELETE api/Favourite/1
        [HttpDelete("RemoveFavourite/{productId}")]
        public void Delete(string token, int productId)
        {
            if (token == null)
            {
                token = "eyJFTUFJTCI6IiIsIklEIjoiMCIsIlJPTEUgSUQiOiIxIn0=";
            }

            int id = JWTValidator.TokenValidation(token);

            var remove = (from a_b in _context.favourites
                          where a_b.UserId == id && a_b.ProductId == productId
                          select a_b).FirstOrDefault();

            if (remove != null)
            {
                _context.favourites.Remove(remove);
                _context.SaveChanges();
            }
        }
    }
}
