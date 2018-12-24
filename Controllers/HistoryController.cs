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
    public class HistoryController : Controller
    {

        ProjectContext _context;

        public HistoryController(ProjectContext context)
        {
            this._context = context;
        }

        //GET Products
        [HttpGet]
        public IQueryable Get(string token)
        {
            int id = JWTValidator.IDTokenValidation(token);

            var result = from h in this._context.History
                         where h.UserId == id
                         select h;

            return result;
        }

        [HttpPost("Update")]
        public IActionResult UpdateHistory([FromBody] History h, string token)
        {
            int id = JWTValidator.IDTokenValidation(token);
            var result = from s in this._context.ShoppingCarts
                         where (s.UserId == id && 
                         h.UserId == s.UserId &&
                         h.Amount == s.Amount &&
                         h.ProductId == s.ProductId &&
                         h.Date == DateTime.Now)
                         select s;

            foreach (var item in result)
            {
                if (item != null)
                {
                    _context.Add(item);
                    _context.SaveChanges();
                }
            }

            return Ok("isgoed");
        }
    }
}