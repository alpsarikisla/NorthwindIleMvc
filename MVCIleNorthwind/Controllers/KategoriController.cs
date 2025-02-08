using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCIleNorthwind.Models;

namespace MVCIleNorthwind.Controllers
{
    public class KategoriController : Controller
    {
        NORTHWNDEntities db = new NORTHWNDEntities();
        // GET: Kategori
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Categories c)
        {
            try
            {
                if (!string.IsNullOrEmpty(c.CategoryName))
                {
                    db.Categories.Add(c);
                    db.SaveChanges();
                    ViewBag.basarili = "Kategori Ekleme Başarılı";
                }
                else
                {
                    ViewBag.basarisiz = "Kategori Adı boş olamaz";
                }
            }
            catch
            {
                ViewBag.basarisiz = "Ekleme İşlemi Başarısız";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                Categories c = db.Categories.Find(id);
                if (c != null)
                {
                    return View(c);
                }
                else
                {
                    return RedirectToAction("Index", "Kategori");
                }
            }
            else
            {
                return RedirectToAction("Index", "Kategori");
            }
        }
        [HttpPost]
        public ActionResult Duzenle(Categories model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(model);
        }
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                Categories c = db.Categories.Find(id);
                if (c != null)
                {
                    if (c.Products.Count == 0)
                    {
                        db.Categories.Remove(c);
                        db.SaveChanges();
                    }
                    else
                    {
                        TempData["mesaj"] = $"Bu kategori {c.Products.Count} adet üründe kullanıldığı için şu anda silinemez";
                    }
                }
            }
            return RedirectToAction("Index", "Kategori");
        }
    }
}