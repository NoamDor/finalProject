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

namespace finalProject.Controllers
{
    public class ProductsController : Controller
    { 
        private readonly StoreContext _context = new StoreContext();
        private const string _imagesPath = "~/Images/Products";

        // GET: Products
        public async Task<ActionResult> Index()
        {
            ViewData["Title"] = "מסך מוצרים";
            PopulateProductTypesList();
            return View(await _context.Products.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Search(int? ProductTypeId, int? size, string name)
        {
            ViewData["Title"] = "מסך מוצרים";
            ViewData["NameField"] = name;
            ViewData["SizeField"] = size;
            ProductType productType = await _context.ProductTypes.FindAsync(ProductTypeId);
            PopulateProductTypesList(productType);

            var items = _context.Products.Select(item => item);

            if (productType != null)
            {
                items = items.Where(s => s.ProductType.Id == ProductTypeId);
            }
            if (size > 0)
            {
                items = items.Where(s => s.Size == size);
            }
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(s => s.Name.Equals(name));
            }

            return View("Index", await items.ToListAsync());
        }

        public async Task<ActionResult> Purchase(int? id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return View(nameof(NotFound));
            }

            PopulateBranchesList();

            Purchase p = new Purchase
            {
                Count = 0,
                BranchId = null,
                Date = DateTime.Now,
                ProductId = id,
                Product = product
            };

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Purchase([Bind(Include = "Count, Date, BranchId, ProductId, UserId")] Purchase purchase)
        {

            if (HttpContext.Session["isLogin"] != "true")
            {
                return RedirectToAction("Login", "User", null);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _context.Products.FindAsync(purchase.ProductId);
                    var branch = await _context.Branches.FindAsync(purchase.BranchId);

                    if (product == null || branch == null)
                    {
                        return View(nameof(NotFound));
                    }
                    purchase.UserId = Convert.ToInt32(HttpContext.Session["userid"]);
                    _context.Purchases.Add(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return View("Error");
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Create()
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }
            ViewData["Title"] = "יצירת מוצר חדש";
            PopulateSuppliersList();
            PopulateProductTypesList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HttpPostedFileBase file, [Bind(Include = "Price,Name,Size,SupplierId,ProductTypeId")] Product product)
        {
            if (!IsAuthorized())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            if (ModelState.IsValid)
            {
                product.PictureName = file.FileName;
                file.SaveAs(Path.Combine(Server.MapPath(_imagesPath), file.FileName));

                try
                {
                    // save to DB
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }

            ViewData["Title"] = "מחיקת מוצר";
            if (id == null)
            {
                return View(nameof(NotFound));
            }

            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return View(nameof(NotFound));
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return View(nameof(NotFound));
            }

            product.Purchases.ToList().ForEach(p => product.Purchases.Remove(p));
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }

            ViewData["Title"] = "עריכת מוצר קיים";

            if (id == null)
            {
                return View(nameof(NotFound));
            }

            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return View(nameof(NotFound));
            }

            PopulateSuppliersList(product.SupplierId);
            PopulateProductTypesList(product.ProductTypeId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HttpPostedFileBase file,
                                            [Bind(Include = "Price,Name,Id,Size,PictureName,SupplierId,ProductTypeId")] Product product)
        {
            if (!IsAuthorized())
            {
                return View("Unauthorized");
            }

            Product productToUpdate = await _context.Products.FindAsync(product.Id);

            if (productToUpdate == null)
            {
                return View(nameof(NotFound));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // case the user put new image to update
                    if (file != null)
                    {
                        // Save new picture
                        file.SaveAs(Path.Combine(Server.MapPath(_imagesPath), file.FileName));
                        product.PictureName = file.FileName;
                    }

                    _context.Entry(productToUpdate).CurrentValues.SetValues(product);
                    await _context.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    return View("Error");
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private void PopulateBranchesList(object selectedBranch = null)
        {
            var branchesQuery = from d in _context.Branches
                                orderby d.City
                                select d;
            ViewBag.BranchId = new SelectList(branchesQuery, "Id", "City", selectedBranch);
        }

        private void PopulateProductTypesList(object selectedProductType = null)
        {
            var ProductTypeQuery = from d in _context.ProductTypes
                                   orderby d.Name
                                   select d;
            ViewBag.ProductTypeId = new SelectList(ProductTypeQuery, "Id", "Name", selectedProductType);
        }

        private void PopulateSuppliersList(object selectedSupplier = null)
        {
            var suppliersQuery = from d in _context.Suppliers
                                 orderby d.Name
                                 select d;
            ViewBag.SupplierId = new SelectList(suppliersQuery, "Id", "Name", selectedSupplier);
        }

        // GET: Products/NotFound
        public ActionResult NotFound()
        {
            return View();
        }

        private bool IsAuthorized()
        {
            return (HttpContext.Session["isAdmin"] == "true" ? true : false) &&
               (HttpContext.Session["isLogin"] == "true" ? true : false);
        }
    }
}