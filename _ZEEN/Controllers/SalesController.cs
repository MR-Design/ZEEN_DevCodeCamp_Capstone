﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _ZEEN.Data;
using _ZEEN.Models;
using Microsoft.AspNet.Identity;
using _ZEEN.Models.ViewModels;
using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using _ZEEN.Utility;

namespace _ZEEN.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;

        public SalesController (ApplicationDbContext context, IHostingEnvironment e)
        {
            _context = context;
            he = e;
        }
        // GET: Saller Inedx Page
        public IActionResult Listing()
        {
            SellerViewModel view = new SellerViewModel();
            List<Sale> sales = new List<Sale>();
            List<RegularUser> users = new List<RegularUser>();
            //Items Listed By the Seller
            view.sales = _context.Sales.Where(s => s.SaleID == User.Identity.GetUserId()).ToList();

            //Buyer Informations

            //Informations of Users that bought Items (ApplicationUserID in UserTable ==BuyerID in Sales Table)
            var appUserIds = view.sales.Select(s => s.SaleID).ToList();
            
            view.users = _context.RegularUsers.Where(r => appUserIds.Contains(r.ApplicationUserId)).ToList();

            // just to check the view
            //view.users = _context.RegularUsers.ToList();
            return View(view);
        }
        // GET: Saller Inedx Page
        public IActionResult ManageOrders()
        {
            SellerViewModel view = new SellerViewModel();
            List<Sale> sales = new List<Sale>();
            List<RegularUser> users = new List<RegularUser>();
            //Items Listed By the Seller
            view.sales = _context.Sales.Where(s => s.SaleID == User.Identity.GetUserId()).ToList();

            //Buyer Informations

            //Informations of Users that bought Items (ApplicationUserID in UserTable ==BuyerID in Sales Table)
            var appUserIds = view.sales.Select(s => s.BuyerID).ToList();

            view.users = _context.RegularUsers.Where(r => appUserIds.Contains(r.ApplicationUserId)).ToList();

            return View(view);
        }

        // GET: Saller Inedx Page
        public IActionResult Orders()
        {
            SellerViewModel view = new SellerViewModel();
            List<Sale> sales = new List<Sale>();
            List<RegularUser> users = new List<RegularUser>();
            view.sales = _context.Sales.Where(s => s.BuyerID == User.Identity.GetUserId()&& s.Statu==SD.StatusSold).ToList();

       
            return View(view);
        }
        // GET: 
        public IActionResult CancelOrders(int? id)
        {
            SellerViewModel view = new SellerViewModel()
            {
                sale = new Sale(),
                user = new RegularUser()
            };
            view.sale = _context.Sales.Where(s => s.Id == id).SingleOrDefault();

            //Show who Pasted the Sale


            return View(view);
        }

        // Post: 
        [HttpPost]
        public IActionResult CancelOrders(int? id, SellerViewModel view)
        {
            view = new SellerViewModel()
            {
                sale = new Sale(),
                user = new RegularUser()
            };
            view.sale = _context.Sales.Where(s => s.Id == id).SingleOrDefault();

            var StatusInDb = _context.Sales.Where(a => a.Id == id).SingleOrDefault();
            StatusInDb.Statu = SD.StatusCancelled;
            _context.Update(view.sale);
             _context.SaveChanges();

            return RedirectToAction("Orders", "Sales");
        }





        // GET: 
        public IActionResult Ship(int? id, SellerViewModel view)
        {
             view = new SellerViewModel();
            List<Sale> sales = new List<Sale>();
            List<RegularUser> users = new List<RegularUser>();

            view.sale = _context.Sales.Where(s => s.Id == id).SingleOrDefault();

            view.sales = _context.Sales.Where(s => s.SaleID == User.Identity.GetUserId() &&s.Statu!=SD.StatusShipped).ToList();

            var appUserIds = view.sales.Select(s => s.BuyerID).ToList();

            view.users = _context.RegularUsers.Where(r => appUserIds.Contains(r.ApplicationUserId)).ToList();

            return View(view);
        }

        // Post: 
        [HttpPost]
        public IActionResult Ship(int? id)
        {
            SellerViewModel view = new SellerViewModel()
            {
                sale = new Sale(),
                user = new RegularUser()
            };
            view.sale = _context.Sales.Where(s => s.Id == id).SingleOrDefault();
            var UserLogedin = User.Identity.GetUserId();
            var StatusInDb = _context.Sales.Where(a => a.Id == id).SingleOrDefault();
            StatusInDb.Statu = SD.StatusShipped;
            var UserInDb = _context.RegularUsers.Where(u => u.ApplicationUserId == UserLogedin).SingleOrDefault();
            UserInDb.Wallet = UserInDb.Wallet + view.sale.UnitPrice;
            UserInDb.TotalMoneyMade = UserInDb.TotalMoneyMade + view.sale.UnitPrice;


            _context.Update(view.sale);
            _context.SaveChanges();

            return RedirectToAction("Orders", "Sales");
        }
        // GET: User Index Page Based on Search and Filtring
        public IActionResult Index(string searchString, string Men, string Woman, string Clothing, string Shoes, string Jewelry,string Watches)
        {
            SellerViewModel view = new SellerViewModel();
            List<Sale> sales = new List<Sale>();
            view.sales = _context.Sales.Where(s=>s.Statu ==SD.StatusForSale || s.Statu == SD.StatusCancelled).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                view.sales = _context.Sales.FullTextSearchQuery(searchString).Where(s => s.Statu == SD.StatusForSale || s.Statu == SD.StatusCancelled).ToList();
            }
            return View(view);
        }

       
        // GET: Sales/Details/
        public IActionResult Details(int? id, SellerViewModel view)
        {

            view = new SellerViewModel()
            {
                sale = new Sale(),
                user = new RegularUser()
            };
            view.sale = _context.Sales.Where(s=>s.Id ==id).SingleOrDefault();

           //Show who Pasted the Sale
            view.user = _context.RegularUsers.Where(s => s.ApplicationUserId == view.sale.SaleID).SingleOrDefault();

            return View(view);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> Create(Sale sale) //IFormFile picture
        {

            var currentUser = User.Identity.GetUserId();

            //sale = await StoreHomePicture(sale, picture);
            // Sale seller = _context.Sales.Where(s => s.SaleID == currentUser).SingleOrDefault();
          

            sale.SaleID = currentUser;
            sale.Statu = SD.StatusForSale;
            if (ModelState.IsValid)
            {
              
                _context.Add(sale);
                await _context.SaveChangesAsync();
                //Image being  Saved
                string webRootPath = he.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var imageIdInDb = _context.Sales.Find(sale.Id);
                if (files[0] != null && files[0].Length > 0)
                {
                    var uploads = Path.Combine(webRootPath, "images");
                    var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."), files[0].FileName.Length - files[0].FileName.LastIndexOf("."));

                    using (var filesstram = new FileStream(Path.Combine(uploads, sale.Id + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesstram);

                    }
                    imageIdInDb.Image = @"\images\" + sale.Id + extension;
                    await _context.SaveChangesAsync();


                }

                return RedirectToAction(nameof(Index));
            }
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }
        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Detail,Discription,Category,Gender,Quantity,Size,Brand,Color,UnitPrice")] Sale sale)
        {
            var currentUser = User.Identity.GetUserId();

            sale.SaleID = currentUser;
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();

                    //Image being  Saved
                    string webRootPath = he.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    var imageIdInDb = _context.Sales.Find(sale.Id);
                    if (files[0] != null && files[0].Length > 0)
                    {
                        var uploads = Path.Combine(webRootPath, "images");
                        var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."), files[0].FileName.Length - files[0].FileName.LastIndexOf("."));

                        using (var filesstram = new FileStream(Path.Combine(uploads, sale.Id + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filesstram);

                        }
                        imageIdInDb.Image = @"\images\" + sale.Id + extension;
                        await _context.SaveChangesAsync();


                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }




    }
}

