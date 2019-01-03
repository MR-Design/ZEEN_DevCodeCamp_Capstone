using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _ZEEN.Data;
using _ZEEN.Models;
using _ZEEN.Models.ViewModels;
using _ZEEN.Utility;
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
                shipment = new Shipment()
            };
            view.sale = _context.Sales.Where(s => s.Id == id).SingleOrDefault();

            //Show who Pasted the Sale
            var currentUser = User.Identity.GetUserId();

            view.user = _context.RegularUsers.Where(s => s.ApplicationUserId == currentUser).SingleOrDefault();

          
            return View(view);
        }

        [HttpPost]
        public IActionResult Payment(int? id, string stripeEmail, string stripeToken)
        {

            BuyerViewModel view = new BuyerViewModel()
            {
                user = new RegularUser(),
                sale = new Sale(),
                shipment = new Shipment()
            };

            //view.shipment.FirstName = view.user.FirstName;
            //_context.RegularUsers.Select(x=>x.LastName == view.shipping.LastName).SingleOrDefault();

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
            var userInDb = _context.Sales.Where(a => a.Id == id).Single();
            userInDb.BuyerID = User.Identity.GetUserId();
            var StatusInDb = _context.Sales.Where(a => a.Id == id).Single();
            StatusInDb.Statu = SD.StatusSold;
            _context.SaveChanges();

            return RedirectToAction("Index", "Sales");
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