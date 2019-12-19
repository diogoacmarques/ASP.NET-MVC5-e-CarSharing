using e_CarSharing.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace e_CarSharing.Areas.Administration.Controllers
{
    [Authorize(Roles = AccountStaticRoles.ADMINISTRATOR)]
    public class ColoursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Administration/Colours
        public ActionResult Index()
        {
            return View(db.Colours.Where(c => c.Deleted == false).ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColourName")] Colour colour)
        {
            if (ModelState.IsValid)
            {
                var list = db.Colours.Where(c => c.ColourName == colour.ColourName && c.Deleted == false).Count();
                if (list != 0)
                {
                    ModelState.AddModelError(string.Empty, " Já existe essa cor!");
                    return View(colour);
                }
                else if (db.Colours.Where(c => c.ColourName == colour.ColourName && c.Deleted == true).Count() != 0)
                {
                    Colour colorExist = db.Colours.Where(c => c.ColourName == colour.ColourName).First();
                    colorExist.Deleted = false;
                    db.Entry(colorExist).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                db.Colours.Add(colour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(colour);

        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colour colour = db.Colours.Find(id);
            if (colour == null)
            {
                return HttpNotFound();
            }
            return View(colour);
        }

        // POST: Administration/Colours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Colour colour = db.Colours.Find(id);
            /* if (db.Vehicles.Where(v => v.BrandId == colour.BrandId && v.Deleted == false).Count() != 0 || db.Brands.Where(m => m.BrandId == brand.BrandId && m.Deleted == false).Count() != 0)
             {
                 ViewBag.AlertText = " A marca não pode ser eliminada por estar em utilização!";
                 ViewBag.ShowAlert = true;
                 return View(brand);

             }*/
            colour.Deleted = true;
            db.Entry(colour).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}