using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;

namespace projectC.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {

        ProjectContext _context;

        public CategoriesController(ProjectContext context)
        {
            this._context = context;
        }
        //GET api/values
        [HttpGet]
        public IQueryable Get()
        {
            var result = from c in this._context.categories
                         join s in this._context.SubCategories
                         on c.Id equals s.Category.Id into Collection
                         select new
                         {
                             Category = c,
                             SubCategory = Collection.ToArray()
                         };
            return result;
        }
        [HttpGet("{id}")]
        public IQueryable Get(int id)

        {
            var result = from i in this._context.categories
                         from p in this._context.products
                         where i.Id == id && p.Category.Id == i.Id
                         select p;

            return result;
        }


        [HttpGet("{id}/Subcategories")]
        public IQueryable Get2(int id)
        {
            var result = from c in this._context.categories
                         join s in this._context.SubCategories
                         on c.Id equals s.Category.Id into Collection
                         where c.Id == id
                         select new
                         {
                             Category = c,
                             SubCategory = Collection.ToArray()
                         };
            return result;
        }



        [HttpGet("{id}/Subcategories/{id2}")]
        public IQueryable Get3(int id, int id2)
        {
            var result = from c in this._context.categories
                         from s in this._context.SubCategories
                         join p in this._context.products
                         on s.Id equals p.SubCategory.Id
                         where c.Id == id && s.Id == id2
                         select p;

            return result;
        }




    }
}
