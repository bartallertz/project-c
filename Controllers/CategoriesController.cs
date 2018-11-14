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

        [HttpGet("subcategory/{name}")]
        public IQueryable Get(string name)
            {
                var result = from c in this._context.categories
                             join s in this._context.SubCategories
                             on c.Id equals s.Category.Id into SubCategoriesGroup
                             where c.Name == name
                             select new
                             {
                                 Categories = c,
                                 SubCategories = SubCategoriesGroup.ToList()
                             };

                        return result;

            }


    }
}