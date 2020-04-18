using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using finalProject.Models;

namespace finalProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly StoreContext _context = new StoreContext();

        // GET: Users
        public async Task<ActionResult> Index(string Username, string Password, string Address)
        {
            ViewData["Title"] = "מסך משתמשים";
            ViewData["BranchesUsernameQuery"] = Username;
            ViewData["BranchesPasswordQuery"] = Password;
            ViewData["BranchesAddressQuery"] = Address;

            var users = _context.Users.Select(x => x);

            if (!String.IsNullOrEmpty(Username))
            {
                users = users.Where(x => x.Username.Contains(Username));
            }

            if (!String.IsNullOrEmpty(Password))
            {
                users = users.Where(x => x.Password.Contains(Password));
            }

            if (!String.IsNullOrEmpty(Address))
            {
                users = users.Where(x => x.Address.Contains(Address));
            }

            return View(await users.ToListAsync());
        }

        public ActionResult Edit(int id)
        {
            var user = _context.Users.Where(b => b.Id == id)
                                 .FirstOrDefault();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var userDb = _context.Users.Where(u => u.Id == user.Id)
                             .FirstOrDefault();
                userDb.Username = user.Username;
                userDb.Address = user.Address;
                userDb.Password = user.Password;
                userDb.IsAdmin = user.IsAdmin;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {

            var user = _context.Users.Where(u => u.Id == id)
                                 .FirstOrDefault();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, User user)
        {
            var userDb = _context.Users.Where(u => u.Id == id)
                          .FirstOrDefault();
            if (userDb == null)
            {
                return View("Error");
            }

            _context.Users.Remove(userDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}