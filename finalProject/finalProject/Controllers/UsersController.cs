using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // GET: Users/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Username,Password,Address,BirthDate,Telephone,Gender")] User user)
        {
            if (_context.Users.Any(e => e.Username == user.Username))
            {
                ViewData["errorMessage"] = "שם משתמש זה קיים כבר במערכת";
                return View(nameof(Register));
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return await LoginUser(user.Username, user.Password);
            
           
        }

        // GET: Users/Login
        public ActionResult Login()
        {
            return View();
        }


        // GET: Users/Login/5
        public async Task<ActionResult> LoginUser(string userName, string password)
        {
            if (userName == null || password == null)
            {
                return View("Login");
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userName && u.Password == password);
            if (user == null)
            {
                return View(nameof(NotFound));
            }
            HttpContext.Session.Add("isAdmin", user.IsAdmin ? "true" : "false");
            HttpContext.Session.Add("username", user.Username);
            HttpContext.Session.Add("userid", user.Id.ToString());
            HttpContext.Session.Add("isLogin", "true");

            if (user.IsAdmin)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home", null);
        }

        // GET: Users/NotFound
        public ActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Add("isAdmin", "false");
            HttpContext.Session.Add("username", "");
            HttpContext.Session.Add("userid", "-1");
            HttpContext.Session.Add("isLogin", "false");

            return RedirectToAction("Index", "Home", null);
        }

    }
}