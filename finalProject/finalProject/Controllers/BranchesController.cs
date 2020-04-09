using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using finalProject.Data;
using System.Data.Entity;
using System.Threading.Tasks;

namespace finalProject.Controllers
{
    public class BranchesController : Controller
    {
        private readonly StoreContext _context = new StoreContext();

        // GET: Branches
        //public async Task<ActionResult> Index()
        //{
        //  return View(await _context.Branches.ToListAsync());
        //}


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
            return View();
        }
    }
}

