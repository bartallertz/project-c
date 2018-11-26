using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;

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

        // GET api/values
        [HttpGet]
        public IQueryable Get()
        {
        var result = (from u in _context.users
                        let a_products = 
                        (from a_b in _context.shoppingCarts
                        from b in _context.products
                        where a_b.UserId == u.Id && a_b.ProductId == b.Id select b).ToArray()
                        select new
                        {
                            Product = a_products
                        });

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IQueryable Get(int id)
        {
          
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
