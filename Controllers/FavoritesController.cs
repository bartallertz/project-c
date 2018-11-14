using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;


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
        // GET api/values
        [HttpGet]
        public Favourite[] Get()
        {

            var result = (from u in _context.users
                          let a_Products =
                          (from a_b in _context.favourites
                           from b in _context.products
                           where a_b.UserId == u.Id && a_b.ProductId == b.Id
                           select b).ToArray()

                          select new Favourite()
                          {
                              User = u,
                              Products = a_Products

                          }).ToArray();

            return result;
        }
        public class Favourite
        {
            public User User { get; set; }

            public Product[] Products { get; set; }
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public Favourite[] Get(int id)
        {
            var result = (from u in _context.users
                          where u.Id == id
                          let a_Products =
                          (from a_b in _context.favourites
                           from b in _context.products
                           where a_b.UserId == u.Id && a_b.ProductId == b.Id
                           select b).ToArray()

                          select new Favourite()
                          {
                              User = u,
                              Products = a_Products

                          }).ToArray();
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
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete()
        {


        }
    }
}
