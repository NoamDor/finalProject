using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using finalProject.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using finalProject.Models;
using System.Net;

namespace finalProject.Controllers
{
    public class BranchesController : Controller
    {
        private readonly StoreContext _context = new StoreContext();

        // GET: Branches
        public async Task<ActionResult> Index(string City, string Address)
        {

            ViewData["BranchesCityQuery"] = City;
            ViewData["BranchesAddressQuery"] = Address;

            var branches = _context.Branches.Select(x => x);

            if (!String.IsNullOrEmpty(City))
            {
                branches = branches.Where(x => x.City.Contains(City));
            }

            if (!String.IsNullOrEmpty(Address))
            {
                branches = branches.Where(x => x.Address.Contains(Address));
            }

            return View(await branches.ToListAsync());
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }

            if (ModelState.IsValid)
            {
                var branches = _context.Branches.Add(branch);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        public ActionResult Edit(int id)
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }

            var branch = _context.Branches.Where(b => b.Id == id)
                                 .FirstOrDefault();

            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Branch branch)
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }

            if (ModelState.IsValid)
            {
                var branchDb = _context.Branches.Where(b => b.Id == branch.Id)
                             .FirstOrDefault();
                branchDb.City = branch.City;
                branchDb.Address = branch.Address;
                branchDb.Telephone = branch.Telephone;
                branchDb.Lat = branch.Lat;
                branchDb.Long = branch.Long;
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
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }

            var branch = _context.Branches.Where(b => b.Id == id)
                                 .FirstOrDefault();

            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Branch branch)
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }

            var branchDb = _context.Branches.Where(b => b.Id == id)
                          .FirstOrDefault();
            if(branchDb == null)
            {
                return View("Error");
            }

            _context.Branches.Remove(branchDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IsAuthorized()
        {
            return (HttpContext.Session["isAdmin"] == "true" ? true : false) &&
               (HttpContext.Session["isLogin"] == "true" ? true : false);
        }

    }
}

