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
        public IActionResult GetUser(string token)
        {
            bool RoleId = JWTValidator.RoleIDTokenValidation(token);

            if (RoleId)
            {


                var result = (from m in _context.users select m).Count();

                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpGet("Products")]
        public IActionResult GetProductsStatistics(string token)
        {
            bool RoleId = JWTValidator.RoleIDTokenValidation(token);

            if (RoleId)
            {


                var result = (from m in _context.products orderby m.Stock ascending select m).Take(10);

                return Ok(result);
            }
            return Unauthorized();
        }

       
        
        [HttpGet("Pending")]
        public IActionResult Getpending(string token)
        {
            bool RoleId = JWTValidator.RoleIDTokenValidation(token);

            if (RoleId)
            {
                var result = (from m in _context.History where m.Status == "Pending" select m).Count();



                return Ok(result);
            }
            return Unauthorized();

        }
        [HttpGet("GetDelivered")]
        public IActionResult GetDelivered(string token)
        {
            bool RoleId = JWTValidator.RoleIDTokenValidation(token);


            if (RoleId)
            {
                var result = (from m in _context.History where m.Status == "Delivered" select m).Count();



                return Ok(result);
            }
            return Unauthorized();



        }
    }
}



