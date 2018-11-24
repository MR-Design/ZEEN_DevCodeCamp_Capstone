using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _ZEEN.Data;
using _ZEEN.Models;
using _ZEEN.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

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
                user = new RegularUser()
            };
            view.sale = _context.Sales.Where(s => s.Id == id).SingleOrDefault();

            //Show who Pasted the Sale
            var currentUser = User.Identity.GetUserId();

            view.user = _context.RegularUsers.Where(s => s.ApplicationUserId == currentUser).SingleOrDefault();



            return View(view);
        }
    }
}