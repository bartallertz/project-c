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
        public int GetUser(int id)
        {
            var result = (from m in _context.users where m.RoleId == 1 select m).Count();

            return result;
        }

        [HttpGet("Products")]
        public IQueryable GetProductsStatistics(int number)
        {
            var result = (from m in _context.products orderby m.Stock ascending select m).Take(10);




            return result;
        }
    }
}



