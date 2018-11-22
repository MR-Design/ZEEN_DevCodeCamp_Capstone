using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _ZEEN.Data;
using _ZEEN.Models;
using _ZEEN.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace _ZEEN.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public IActionResult Index()
        {
            UserViewModel view = new UserViewModel();
            List<Messages> messages = new List<Messages>();
            view.messages = _context.Messages.ToList();
            return View(view);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Include(m => m.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // GET: Messages/Create
        public IActionResult SendMessage()
        {
            //ViewData["FromId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult SendMessage(UserViewModel view)
        {
             view = new UserViewModel()
            {
               message =new Messages()
            };
            //view.message = messages; // not good

            var currentUser = User.Identity.GetUserId();
            view.message.FromId = currentUser;

            view.message.To = _context.RegularUsers.Select(x => x.UserName).SingleOrDefault();

            if (ModelState.IsValid)
            {
               // _context.Add(messages);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["FromId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", messages.FromId);
            return View(view);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }
            ViewData["FromId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", messages.FromId);
            return View(messages);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromId,To,Message,Read")] Messages messages)
        {
            if (id != messages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagesExists(messages.Id))
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
            ViewData["FromId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", messages.FromId);
            return View(messages);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Include(m => m.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messages = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(messages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessagesExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }



    //// GET: Messages/Details/5
    //public IActionResult Details(Messages messages)
    //{
    //    var currentUser = User.Identity.GetUserId();
    //    messages = _context.Messages.Where(s => s.FromId == currentUser).SingleOrDefault();
    //    if (currentUser == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Messages
    //       .Include(m => m.ApplicationUser)
    //       .FirstOrDefaultAsync(m => m.FromId == currentUser);
    //    if (messages == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(messages);
}
