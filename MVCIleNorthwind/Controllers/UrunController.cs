using MVCIleNorthwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIleNorthwind.Controllers
{
    public class UrunController : Controller
    {
        NORTHWNDEntities db = new NORTHWNDEntities();
        // GET: Urun
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            return View();
        }

        // POST: Urun/Create
        [HttpPost]
        public ActionResult Create(Products model)
        {
            try
            {
                model.Discontinued = !model.Discontinued;
                model.QuantityPerUnit = "10'lu kutu";
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Products p = db.Products.Find(id);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", p.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", p.SupplierID);
            return View(p);
        }

        // POST: Urun/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Urun/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Urun/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
