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

        [HttpGet]
        public IQueryable Get()
        {
            var result = from p in this._context.products
                         orderby p.Id
                         select new
                         {
                             Product = p,
                         };

            return result;
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
