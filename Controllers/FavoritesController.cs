using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;


namespace projectC.Controllers
{
    [Route("api/[controller]")]
    public class FavoritesController : Controller
    {
        ProjectContext _context;
        public FavoritesController(ProjectContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public IQueryable Get()
        {
            var result = from u in this._context.users
                         join p in this._context.favourites
                         on u.Id equals p.UserId into FavGrp
                         select new 
                         {
                             User = u,
                             Product = FavGrp.ToList()
                         };



            return result;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IQueryable Get(int id)
        {
            var result = from u in this._context.users
                         join p in this._context.favourites
                         on u.Id equals p.UserId into FavGrp
                         where u.Id == id
                         select new 
                         {
                             User = u,
                             Product = FavGrp.ToList()
                         };



            return result;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Favorite f)
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
        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public IActionResult Delete(Favorite f)
        // {
        //   if()
        //   {
        //       _context.favourites.Remove(f);
        //       _context.SaveChanges();
        //       return Ok();
        //   }
        //   return Unauthorized();
        // }
    }
}
