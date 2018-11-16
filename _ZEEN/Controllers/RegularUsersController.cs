using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _ZEEN.Data;
using _ZEEN.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using _ZEEN.Models.ViewModels;

namespace _ZEEN.Controllers
{
    public class RegularUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;

        public RegularUsersController(ApplicationDbContext context, IHostingEnvironment e)
        {
            _context = context;
            he = e;
        }




        public IActionResult ShowFields(string fullname, IFormFile pic)
        {
            ViewData["fname"] = fullname;
            if (pic !=null)
            {
                var fileName = Path.Combine(he.WebRootPath, Path.GetDirectoryName(pic.FileName));
                pic.CopyTo(new FileStream(fileName, FileMode.Create));
                    ViewData["fileLocation"] ="/"+ Path.GetFileName(pic.FileName);
            }
            return View();
        }

        // GET: RegularUsers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RegularUsers.Include(r => r.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RegularUsers/Details/5
        public async Task<IActionResult> UserDetails()
        {
            UserViewModel view = new UserViewModel()
            {
                user = new RegularUser()

            };
            var currentUser = User.Identity.GetUserId();
            RegularUser RegularUsers = _context.RegularUsers.Where(s => s.ApplicationUserId == currentUser).SingleOrDefault();
         
            
            view.user = RegularUsers;


            var regularUser = await _context.RegularUsers
                .Include(r => r.ApplicationUser)
                .FirstOrDefaultAsync(m => m.ApplicationUserId == currentUser);
            if (regularUser == null)
            {
                return NotFound();
            }

            return View(view);
        }

        // GET: RegularUsers/Create
        public IActionResult Create()
        {

            ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: RegularUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,Image,Gender,UserName,Bio,WebSite,FirstName,LastName,City,State,ZipCode")] RegularUser regularUser, IFormFile picture)
        {
           
            RegularUser rUser = _context.RegularUsers.Where(s => s.ApplicationUserId == User.Identity.GetUserId().ToString()).SingleOrDefault();
            regularUser.ApplicationUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                _context.Add(regularUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (picture != null)
            {
                using (var stream = new MemoryStream())
                {
                    await picture.CopyToAsync(stream);
                    regularUser.Picture = stream.ToArray();
                }
            }

            ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", regularUser.ApplicationUserId);
            return View(regularUser);
        }

        // GET: RegularUsers/Edit/5
        public ActionResult EditProfile()
        {
            var currentUser = User.Identity.GetUserId();
            RegularUser RegularUsers = _context.RegularUsers.Where(s => s.ApplicationUserId == currentUser).SingleOrDefault();
            UserViewModel view = new UserViewModel()
            {
                user = new RegularUser()

            };

          
            view.user = RegularUsers;     
            return View(view);
        }

        // POST: RegularUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EditProfile(IFormCollection collection)
        {
            var currentUser = User.Identity.GetUserId();
            RegularUser thisUser = _context.RegularUsers.Where(u => u.ApplicationUserId == currentUser).SingleOrDefault();
            UserViewModel view = new UserViewModel()
            {
                user = new RegularUser()

            };
            view.user = thisUser;
            view.user.UserName = collection["user.UserName"];
            view.user.LastName = collection["user.LastName"];
            view.user.FirstName = collection["user.FirstName"];
            view.user.WebSite = collection["user.WebSite"];
            view.user.Bio = collection["user.Bio"];
            view.user.City = collection["user.City"];
            view.user.State = collection["user.State"];
            view.user.ZipCode = Int32.Parse(collection["user.ZipCode"]);
            _context.SaveChanges();
            return RedirectToAction("UserDetails", "RegularUsers");
        }



        // GET: RegularUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regularUser = await _context.RegularUsers
                .Include(r => r.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regularUser == null)
            {
                return NotFound();
            }

            return View(regularUser);
        }

        // POST: RegularUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var regularUser = await _context.RegularUsers.FindAsync(id);
            _context.RegularUsers.Remove(regularUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegularUserExists(int? id)
        {
            return _context.RegularUsers.Any(e => e.Id == id);
        }
    }
}


//here an exmeple from DropZone to Drog and Drop Pictures
//[HttpPost]
//public async Task<IActionResult> Upload(IFormFile file)
//{
//    var uploads = Path.Combine(he.WebRootPath, "uploads");
//    if (file.Length > 0)
//    {
//        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
//        {
//            await file.CopyToAsync(fileStream);
//        }
//    }
//    return RedirectToAction("Index");
//}