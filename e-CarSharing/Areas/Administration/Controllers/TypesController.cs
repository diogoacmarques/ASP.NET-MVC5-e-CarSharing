using e_CarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace e_CarSharing.Areas.Administration.Controllers
{
    [Authorize(Roles = AccountStaticRoles.ADMINISTRATOR)]
    public class TypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administration/Type
        public ActionResult Index()
        {
            return View(db.Types.Where(b => b.Deleted == false).ToList());
        }

        //####################################################################################################
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TypeName")] e_CarSharing.Models.Type type)
        {
            if (ModelState.IsValid)
            {
                if (db.Types.Where(t => t.TypeName == type.TypeName && t.Deleted == false).Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, " Já existe essa marca!");
                    return View(type);
                }
                else if (db.Brands.Where(m => m.BrandName == type.TypeName && m.Deleted == true).Count() != 0)
                {
                    Brand brandExist = db.Brands.Where(c => c.BrandName == type.TypeName).First();
                    brandExist.Deleted = false;
                    db.Entry(brandExist).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                db.Types.Add(type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);

        }

        //#####################################################################################################
        public ActionResult Edit(int id)
        {
            return View();
        }

        //#####################################################################################################
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            e_CarSharing.Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            e_CarSharing.Models.Type type = db.Types.Find(id);  
            if (db.Vehicles.Where(v => v.TypeId == type.TypeId && v.Deleted == false).Count() != 0 || db.Types.Where(t => t.TypeId == type.TypeId && t.Deleted == false).Count() == 0)
            {
                ViewBag.AlertText = " O Tipo não pode ser eliminada por estar em utilização!";
                ViewBag.ShowAlert = true;
                return View(type);

            }
            type.Deleted = true;
            db.Entry(type).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}