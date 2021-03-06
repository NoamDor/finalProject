﻿using finalProject.Models;
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
            if (!IsAuthorized())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HttpPostedFileBase file, Supplier supplier)
        {
            if (!IsAuthorized())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (ModelState.IsValid)
            {
                if (file.FileName != null)
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
            if (!IsAuthorized())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

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
        public async Task<ActionResult> Edit(HttpPostedFileBase file, Supplier supplier)
        {
            if (!IsAuthorized())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            Supplier supplierToUpdate = await _context.Suppliers.FindAsync(supplier.Id);

            if (supplierToUpdate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // In case the user put new image to update
                    if (file != null)
                    {
                        // Save new picture
                        file.SaveAs(Path.Combine(Server.MapPath(_imagesPath), file.FileName));
                        supplier.PictureName = file.FileName;
                    }

                    _context.Entry(supplierToUpdate).CurrentValues.SetValues(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                    return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
                }
            }

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Delete(int? SupplierID)
        {
            if (!IsAuthorized())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (!IsAuthorized())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            var supplier = await _context.Suppliers.FindAsync(SupplierID);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            supplier.Products.ToList().ForEach(p => supplier.Products.Remove(p));

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsAuthorized()
        {
            return (HttpContext.Session["isAdmin"] == "true" ? true : false) &&
               (HttpContext.Session["isLogin"] == "true" ? true : false); 
        }
    }
}