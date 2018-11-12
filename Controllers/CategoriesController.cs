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
        // GET api/values
        [HttpGet]
        public IQueryable<Category> Get()
        {
            var result = from m in this._context.categories select m;

            return result;
        }
    //     [HttpGet({"id"})]
    //     public IQueryable<Category> Get(int id)

    //         var result = 


    // }
    }
}