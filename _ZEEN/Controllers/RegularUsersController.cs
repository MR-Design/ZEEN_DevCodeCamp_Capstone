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
using _ZEEN.Utility;

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

        // GET: Fellowers
        public IActionResult Follow()
        {
            UserViewModel view = new UserViewModel()
            {
                follower = new Follower()
            };
            var followers = _context.Followers.Select(f => f.FromId).ToList();
            foreach (var item in followers)
            {
                if (item.Count() != item.Distinct().Count())
                {
                    ViewData["Followers"] = followers.Count();

                }
            }
 
            if (view.user == null)
            {
                return NotFound();
            }
            return View(view);
        }
        [HttpPost]
        public IActionResult Follow(UserViewModel view)
        {
     

            var currentUser = User.Identity.GetUserId();
            view.follower.FromId = currentUser;
            if (ModelState.IsValid && User.Identity.GetUserId()!= view.follower.To)
            {
                _context.Followers.Add(view.follower);

                _context.SaveChanges();
                return RedirectToAction("Index", "Sales");
            }

            return RedirectToAction("Index", "Sales");
        }

        // GET: RegularUsers/Details/5
        public IActionResult Profiles(string id)
        {
           
            UserViewModel view = new UserViewModel()
            {
                message = new Messages(),
                user = new RegularUser(),
                follower =new Follower()

            };
            RegularUser RegularUsers = _context.RegularUsers.Where(s => s.ApplicationUserId == id).SingleOrDefault();
            view.user = RegularUsers;

            ViewData["Followers"] = _context.Followers.Count();

            //id = view.user.ApplicationUserId;
            if (id == null)
            {
                return NotFound();
            }
         
            if (view.user == null)
            {
                return NotFound();
            }

            return View(view);
        }
        [HttpPost]
        public IActionResult Profiles(UserViewModel view)
        {
            //view = new UserViewModel()
            //{
            //    message = new Messages()
            //};
            //view.message = messages; // not good

            var currentUser = User.Identity.GetUserId();
            view.message.FromId = currentUser;
            //view.message.Sender = view.user.UserName;

            // view.message.To = _context.RegularUsers.Where(x => x.UserName ==).SingleOrDefault();
            if (ModelState.IsValid)
            {
                 _context.Messages.Add(view.message);

                _context.SaveChanges();
                return RedirectToAction("Index", "Sales");
            }
            return RedirectToAction();
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
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,AvatarImage,Gender,UserName,Bio,WebSite,FirstName,LastName,Street,City,State,ZipCode")] RegularUser regularUser)
        {

             RegularUser rUser = _context.RegularUsers.Where(s => s.ApplicationUserId == User.Identity.GetUserId().ToString()).SingleOrDefault();
            regularUser.ApplicationUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                _context.Add(regularUser);
                await _context.SaveChangesAsync();

                //Image being  Saved
                string webRootPath = he.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var imageIdInDb = _context.RegularUsers.Find(regularUser.Id);
                if (files[0] != null && files[0].Length > 0)
                {
                    var uploads = Path.Combine(webRootPath, "images");
                    var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."), files[0].FileName.Length - files[0].FileName.LastIndexOf("."));

                    using (var filesstram = new FileStream(Path.Combine(uploads, regularUser.Id + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesstram);

                    }
                    imageIdInDb.AvatarImage = @"\images\" + regularUser.Id + extension;


                }
                //else
                //{
                //    var uploads = Path.Combine(webRootPath, @"\images\" + SD.DefaultProfileImage);
                //    System.IO.File.Copy(uploads, webRootPath + @"\images\" + regularUser.Id + ".png");

                //    imageIdInDb.AvatarImage = @"\images\" + regularUser.Id + ".png";

                //}
                await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Sales");


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
            view.user.Street = collection["user.Street"];
            view.user.City = collection["user.City"];
            view.user.State = collection["user.State"];
            view.user.ZipCode = Int32.Parse(collection["user.ZipCode"]);

            _context.SaveChanges();

            //Image being  Saved
            string webRootPath = he.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var imageIdInDb = _context.RegularUsers.Find(view.user.Id);
            if (files[0] != null && files[0].Length > 0)
            {
                var uploads = Path.Combine(webRootPath, "images");
                var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."), files[0].FileName.Length - files[0].FileName.LastIndexOf("."));

                using (var filesstram = new FileStream(Path.Combine(uploads, view.user.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesstram);

                }
                imageIdInDb.AvatarImage = @"\images\" + view.user.Id + extension;


            }
          
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