using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using projectC.JWT;

namespace projectC.Controllers
{
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        ProjectContext _context;
        public StatisticsController(ProjectContext context)
        {
            _context = context;
        }

        [HttpGet("Users")]
        public int GetUser()
        {
            var result = (from m in _context.users select m).Count();

            return result;
        }

        [HttpGet("Products")]
        public IQueryable GetProductsStatistics()
        {
            // get 10 lowest stocked items from database
            var result = (from m in _context.products orderby m.Stock ascending select m).Take(10);

            return result;
        }

        [HttpGet("ProductSpend")]

        public double GetProductsAvg(int id)
        {
            var result = (from m in _context.ShoppingCarts where m.ProductId == id select m.Amount).Average();

            return result;
        }

        [HttpGet("Fav")]
        public IQueryable GetTop10Fav()
        {

             var result = (from m in _context.favourites
                          group m by m.ProductId into Fav
                         let count = Fav.Count() orderby count descending select new
                         {
                             Fav,
                            Aantal_keer = Fav.Count()
                         }).Take(10);


           
            return result;



        }
      
    }
}



