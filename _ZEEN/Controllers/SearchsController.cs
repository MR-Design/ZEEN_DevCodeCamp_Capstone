using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _ZEEN.Data;
using _ZEEN.Models;
using _ZEEN.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _ZEEN.Controllers
{
    public class SearchsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Search(string searchString)
        {
            SellerViewModel view = new SellerViewModel();
            List<Sale> sales = new List<Sale>();

            if (!String.IsNullOrEmpty(searchString))
            {
                view.sales = _context.Sales.Where(s => s.Brand == searchString).ToList(); // I need to search in the hole database

            }
            return View(view);
        }
    }
}