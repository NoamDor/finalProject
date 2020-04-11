using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using finalProject.Data;
using finalProject.Models;

namespace finalProject.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly StoreContext _context = new StoreContext();

        public SuppliersController()
        {
        }

        // // GET: Suppliers
        public ActionResult Index()
        {
            //return View(await _context.Suppliers.AsQueryable().ToListAsync());
            return View(_context.Suppliers.ToList());
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Suppliers.Add(supplier);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    //ViewBag.RoomTypeList = GetRoomTypeListItems();
                    return View(supplier);
                }
            }
            catch
            {
                ViewBag.Error = "Error occurred, new room was not saved";
                //ViewBag.RoomTypeList = GetRoomTypeListItems();
                return View();
            }
        }

        public ActionResult Edit(int SupplierID)
        {
            var supplier = _context.Suppliers.ToList().Where(s => s.Id == SupplierID).FirstOrDefault();

            return View(supplier);
        }

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            _context.Entry(supplier).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int SupplierID)
        {
            var supplier = _context.Suppliers.ToList().Where(s => s.Id == SupplierID).FirstOrDefault();

            return View(supplier);
        }

        [HttpPost]
        public ActionResult Delete(int SupplierID, Supplier suplier)
        {
            var supplier = _context.Suppliers.ToList().Where(s => s.Id == SupplierID).FirstOrDefault();

            if (supplier == null)
            {
                return HttpNotFound();
            }

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}