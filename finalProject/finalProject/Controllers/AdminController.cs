using finalProject.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using System.Web.Helpers;

namespace finalProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly StoreContext _context = new StoreContext();

        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Other Web API configuration not shown.
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public AdminController()
        {
        }

        [Route("/Admin/BranchSales")]
        [HttpGet]
        public JsonResult BranchSales()
        {
            try
            {
                var result = _context.Purchases.Join(_context.Branches,
                    (purchase => purchase.Branch.Id),
                    (branch => branch.Id),
                    (p, b) => new
                    {
                        branchId = b.Id,
                        branchCity = b.City,
                        count = p.Count
                    })
                .GroupBy(b => b.branchId)
                .Select(p => new
                {
                    Count = p.Sum(pur => pur.count),
                    Name = p.FirstOrDefault(pur => pur.branchId == p.Key).branchCity,
                    Id = p.Key
                }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e);
            }

            return null;
        }

        [Route("/Admin/ProductSales")]
        [HttpGet]
        public JsonResult ProductSales()
        {
            try
            {
                //if (!IsAuthorized())
                //{
                //    var res = Json("you are not autorized for this request");
                //    res.StatusCode = 401;
                //    return res;
                //}
                var result = _context.Purchases.Join(_context.Products,
                    (purchase => purchase.Product.Id),
                    (product => product.Id),
                    (pur, pro) => new
                    {
                        ProductTypeId = pro.ProductTypeId,
                        productName = pro.ProductType.Name,
                        Count = pur.Count
                    })
                .GroupBy(b => b.ProductTypeId)
                .Select(p => new
                {
                    Count = p.Sum(pur => pur.Count),
                    Name = p.FirstOrDefault(pur => pur.ProductTypeId == p.Key).productName,
                    Id = p.Key
                }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e);
            }

            return null;
        }
    }
}
