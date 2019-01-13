using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectC.model;
using projectC.JWT;
using projectC.Mail;

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
        // bool CheckRequestedAmount(int Amount, int ProductId)
        // {
        //     var Stock = (from p in this._context.products
        //                  where p.Id == ProductId
        //                  select p.Stock).Single();
        //     if (Amount <= Stock)
        //     {
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }
        // }

        public void DeleteAll(int userId)
        {
            var remove = (from a_b in _context.ShoppingCarts
                          where a_b.UserId == userId
                          select a_b).ToList();

            foreach (var item in remove)
            {
                if (item != null)
                {
                    _context.ShoppingCarts.Remove(item);
                    _context.SaveChanges();
                }
                else
                {
                    BadRequest();
                }
            }
        }
        public void UpdateStock(string token)
        {
            int id = JWTValidator.IDTokenValidation(token);
            var result = from p in this._context.products
                         from s in this._context.ShoppingCarts
                         where p.Id == s.ProductId && s.UserId == id
                         select new { p, s };

            foreach (var item in result)
            {
                item.p.Stock = item.p.Stock - item.s.Amount;
                if (item.p.Stock < 0)
                {
                    item.p.Stock = 0;
                }
            }
        }

        [HttpGet]
        public IQueryable Get(string token)
        {
            int id = JWTValidator.IDTokenValidation(token);

            var result = from h in this._context.History
                         where h.UserId == id
                         select new
                         {
                             productName = h.Product.Name,
                             amount = h.Amount,
                             totalPrice = (double)h.Product.Price * (double)h.Amount,
                             image = h.Product.FirstImg,
                             purchaseDate = h.Date,
                             status = h.Status
                         };

            return result;
        }

        [HttpPost("Update")]
        public IActionResult UpdateHistory(string token)
        {
            string Total = "";
            int id = JWTValidator.IDTokenValidation(token);
            var result = from s in this._context.ShoppingCarts
                         where s.UserId == id
                         select s;

            foreach (var item in result)
            {
                History history = new History();
                history.Amount = item.Amount;
                history.Date = DateTime.Now.ToString();
                history.ProductId = item.ProductId;
                history.UserId = item.UserId;
                history.Status = "Pending";              
                _context.Add(history);
                string GetProduct()
                {
                    var Productname = (from p in this._context.products
                                       where history.ProductId == p.Id
                                       select p.Name).First();
                    return Productname;
                }
                Total = Total + "<li>" + GetProduct() + " " + history.Amount.ToString() + "x" + "</li>";
            }
            string GetMail()
            {
                var mailinfo = (from u in this._context.users
                                where u.Id == id
                                select u.email).First();
                return mailinfo;
            }
            UpdateStock(token);
            Mail.MailProduct.PurchaseMail(GetMail(), Total);
            DeleteAll(id);
            _context.SaveChanges();
            return Ok("Geschiedenis bijgewerkt");
        }
    }
}