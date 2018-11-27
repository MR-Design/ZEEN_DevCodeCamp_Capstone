using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _ZEEN.Data;
using _ZEEN.Models;
using _ZEEN.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace _ZEEN.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Payment(int? id, BuyerViewModel view)
        {
            // var currentUser = User.Identity.GetUserId();


            view = new BuyerViewModel()
            {
                sale = new Sale(),
                user = new RegularUser(),
                shipping = new Models.Shipping()
            };
            view.sale = _context.Sales.Where(s => s.Id == id).SingleOrDefault();

            //Show who Pasted the Sale
            var currentUser = User.Identity.GetUserId();

            view.user = _context.RegularUsers.Where(s => s.ApplicationUserId == currentUser).SingleOrDefault();

          
            return View(view);
        }

        [HttpPost]
        public IActionResult Payment(string stripeEmail, string stripeToken)
        {
            BuyerViewModel view = new BuyerViewModel()
            {
                user = new RegularUser(),
                sale = new Sale(),
                shipping = new Models.Shipping()
            };
            view.shipping.SellerID = User.Identity.GetUserId();
            view.shipping.FirstName = view.user.FirstName;
            view.shipping.LastName = view.user.LastName;
            _context.Shippings.Add(view.shipping);

            _context.SaveChanges();
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                //Amount = view.sale.UnitPrice,
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });

            return RedirectToAction("index", "Sales");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}