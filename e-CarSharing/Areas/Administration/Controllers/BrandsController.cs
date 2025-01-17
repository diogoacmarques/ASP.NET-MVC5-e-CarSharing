﻿using e_CarSharing.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Areas.Administration.ViewModels;


namespace e_CarSharing.Areas.Administration.Controllers
{
    [Authorize(Roles = AccountStaticRoles.ADMINISTRATOR)]
    public class BrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administration/Brands
        public ActionResult Index()
        {
            return View(db.Brands.Where(b => b.Deleted == false).Include(t => t.Type).OrderBy(b => b.BrandName));
        }



        // GET: Administration/Brands/Create#################################################################################
        public ActionResult Create()
        {
            BrandViewModelCreate ViewModel = new BrandViewModelCreate
            {   
            Types = new SelectList(db.Types.Where(t => t.Deleted == false), "TypeId", "TypeName")
            };
            return View(ViewModel);
        }

        // POST: Administration/Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrandViewModelCreate ViewModel)
        {
            Brand brand = new Brand
            {
                BrandName = ViewModel.BrandName,
                TypeId = ViewModel.TypeId
            };

            if (ModelState.IsValid)
            {
              

                var list = db.Brands.Where(m => m.BrandName == brand.BrandName && m.Deleted == false).Count();
                if (list != 0)
                {
                    ModelState.AddModelError(string.Empty, " Já existe essa marca");
                    ViewModel.Types = new SelectList(db.Types.Where(t => t.Deleted == false), "TypeId", "TypeName");
                    return View(ViewModel);
                }
                else if (db.Brands.Where(m => m.BrandName == brand.BrandName && m.Deleted == true).Count() != 0)
                {
                    Brand brandExist = db.Brands.Where(c => c.BrandName == brand.BrandName).First();
                    brandExist.Deleted = false;
                    db.Entry(brandExist).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewModel.Types = new SelectList(db.Types.Where(t => t.Deleted == false), "TypeId", "TypeName");
            return View(ViewModel);

        }

        // GET: Administration/Brands/Delete/5 ###############################################################################
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Administration/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            if (db.Vehicles.Where(v => v.BrandId == brand.BrandId && v.Deleted == false).Count() != 0 || db.Models.Where(m => m.BrandId == brand.BrandId && m.Deleted == false).Count() != 0)
            {
                ModelState.AddModelError(string.Empty, "A marca não pode ser eliminada por estar em utilização");
                return View(brand);

            }
            brand.Deleted = true;
            db.Entry(brand).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}