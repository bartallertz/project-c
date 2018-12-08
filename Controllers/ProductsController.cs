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
    public class ProductsController : Controller
    {

        ProjectContext _context;

        public ProductsController(ProjectContext context)
        {
            this._context = context;
        }

        //GET Products
        [HttpGet]
        public IQueryable Get(string token)
        {
            if (token == null)
            {
                var result = from p in _context.products
                         orderby p.Id
                         select new
                         {
                             isFavourite = false,
                             Id = p.Id,
                             Name = p.Name,
                             Description = p.Description,
                             Price = p.Price,
                             FirstImg = p.FirstImg
                         };

                return result;
            } else {
                int id = JWTValidator.TokenValidation(token);

                var result = from p in _context.products
                            let isFavourite = (from f in _context.favourites where p.Id == f.ProductId && f.UserId == id select p).Any()
                            orderby p.Id
                            select new
                            {
                                isFavourite = isFavourite,
                                Id = p.Id,
                                Name = p.Name,
                                Description = p.Description,
                                Price = p.Price,
                                FirstImg = p.FirstImg
                            };

                return result;
            }

        }

        //GET Products/Price=50-100
        [HttpGet("Price={min}-{max}")]
        public IQueryable PriceRange(int min, int max)
        {
            var result = from p in this._context.products
                         where ((p.Price >= min) && (p.Price <= max))
                         select p;
            return result.Distinct();
        }

        //GET Products/Search=(insert text)
        [HttpGet("Search={searchquery}")]
        public IQueryable Search(string searchquery)
        {

            var result = from p in this._context.products
                         from c in this._context.categories
                         where (p.Description.ToString().ToLower().Contains(searchquery.ToLower()) ||
                          p.Name.ToString().ToLower().Contains(searchquery.ToLower()) ||
                            (c.Name.ToString().ToLower().Contains(searchquery.ToLower()) && p.Category.Id == c.Id))
                         orderby p.Id
                         select p;
            return result.Distinct();
        }



        // GET api/values/5
        [HttpGet("{id}")]
        public IQueryable Get(int id)
        {
            var result = from p in this._context.products
                         join i in this._context.imageURLs
                         on p.Id equals i.product.Id into imageURLsGroup
                         where p.Id == id
                         select new
                         {
                             Product = p,
                             Image = imageURLsGroup.ToList()
                         };

            return result;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Product p)
        {

            if (p == null)
            {
                return NoContent();
            }
            else
            {
                this._context.Add(p);
                this._context.SaveChanges();

                return Ok();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id, Product p)
        {

            if (p != null)
            {
                _context.Remove(p);
                _context.SaveChanges();
            }
        }
    }
}
