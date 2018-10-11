using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;

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
        // GET api/Products
        [HttpGet]
        public IQueryable<Product> Get()
        {
            var result = from m in this._context.Products select m;

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
    //     public IQueryable<Product> Get(int id)
    //     {
            
    //    }

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
