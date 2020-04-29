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
    public class SuppliersController : Controller
    {
        private readonly StoreContext _context = new StoreContext();
        private const string _imagesPath = "~/Content/Images";

        public SuppliersController()
        {
        }

        // GET: Suppliers
        public async Task<ActionResult> Index()
        {
            //return View(await _context.Suppliers.AsQueryable().ToListAsync());
            return View(await _context.Suppliers.ToListAsync());
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HttpPostedFileBase file, Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.PictureName != null)
                {
                    supplier.PictureName = file.FileName;
                    file.SaveAs(Path.Combine(Server.MapPath(_imagesPath), file.FileName));
                }

                try
                {
                    _context.Suppliers.Add(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int? SupplierID)
        {

            if (SupplierID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            Supplier supplier = await _context.Suppliers.FindAsync(SupplierID);
            if (supplier == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            //var supplier = _context.Suppliers.ToList().Where(s => s.Id == SupplierID).FirstOrDefault();

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!_context.Suppliers.Any(val => val.Id == supplier.Id))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }

                    _context.Entry(supplier).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Delete(int? SupplierID)
        {
            if (SupplierID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            Supplier supplier = await _context.Suppliers.FindAsync(SupplierID);
            if (supplier == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(supplier);

            //var supplier = _context.Suppliers.ToList().Where(s => s.Id == SupplierID).FirstOrDefault();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int SupplierID, Supplier suplier)
        {
            var supplier = await _context.Suppliers.FindAsync(SupplierID);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            supplier.Products.ToList().ForEach(p => supplier.Products.Remove(p));
            
            if (suplier.PictureName != null)
            {
                // delete the product image from fs
                System.IO.File.Delete(Path.Combine(Server.MapPath(_imagesPath), suplier.PictureName));
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}