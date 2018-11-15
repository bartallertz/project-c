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
        public IQueryable<Category> Get()
        {
            var result = from m in this._context.categories select m;

            return result;
        }
        [HttpGet("{id}")]
        public IQueryable Get(int id)

        {
            var result = from i in this._context.categories
                         join p in this._context.products
                         on i.Id equals p.Category.Id into CategoriesGroup
                         where i.Id == id
                         select new
                         {
                             Categories = i,
                             Products = CategoriesGroup.ToList()

                         };

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
                         on s.Id equals p.SubCategory.Id into SubGrp
                         where c.Id == id && s.Id == id2
                         select new
                         {
                             SubCategory = s,
                             Products = SubGrp.ToList()
                         };

            return result;
        }




    }
}
