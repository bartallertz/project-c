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
        [HttpGet("{categoryId}")]
        public IQueryable Get(int categoryId, string token)

        {
            if (token == null) {
                var result = from i in this._context.categories
                            from p in this._context.products
                            where i.Id == categoryId && p.Category.Id == i.Id
                            select p;

                return result;
            } else {
                int id = JWTValidator.TokenValidation(token);

                var result = from i in this._context.categories
                            from p in this._context.products
                            let isFavourite = (
                                from f in _context.favourites
                                where p.Id == f.ProductId && f.UserId == id
                                select p).Any()
                            where i.Id == categoryId && p.Category.Id == i.Id
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



        [HttpGet("{categoryId}/Subcategories/{subCategoryId}")]
        public IQueryable Get3(int categoryId, int subCategoryId, string token)
        {
            if (token == null) {
                var result = from c in this._context.categories
                         from s in this._context.SubCategories
                         join p in this._context.products
                         on s.Id equals p.SubCategory.Id
                         where c.Id == categoryId && s.Id == subCategoryId
                         select p;

                return result;
            } else {
                int id = JWTValidator.TokenValidation(token);

                var result = from c in this._context.categories
                            from s in this._context.SubCategories
                            join p in this._context.products
                            on s.Id equals p.SubCategory.Id
                            let isFavourite = (
                                    from f in _context.favourites
                                    where p.Id == f.ProductId && f.UserId == id
                                    select p).Any()
                            where c.Id == categoryId && s.Id == subCategoryId
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
    }
}
