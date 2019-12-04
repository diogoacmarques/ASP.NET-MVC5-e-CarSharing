﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;

namespace e_CarSharing.Areas.Administration.Controllers
{
    public class LocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locations
        public ActionResult Index()
        {
            //show all available locations
            return View(db.Locations.Where(b => b.Deleted == false).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocalName,GoogleMapsURL")] Location location)
        {
            if (ModelState.IsValid)
            {
                var list = db.Locations.Where(l => l.LocationName == location.LocationName && l.Deleted == false).Count();
                if (list != 0)
                {
                    ModelState.AddModelError(string.Empty, " Já existe essa localização!");
                    return View(location);
                }
                else if (db.Locations.Where(l => l.LocationName == location.LocationName && l.Deleted == true).Count() != 0)
                {
                    Location localExists = db.Locations.Where(l => l.LocationName == location.LocationName).First();
                    localExists.Deleted = false;
                    localExists.GoogleMapsURL = location.GoogleMapsURL;
                    db.Entry(localExists).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Location local = db.Locations.Find(id);
            //if (db.Vehicles.Where(v => v.BrandId == brand.BrandId && v.Deleted == false).Count() != 0 || db.Brands.Where(m => m.BrandId == brand.BrandId && m.Deleted == false).Count() != 0)
            //{
            //    ViewBag.AlertText = " A marca não pode ser eliminada por estar em utilização!";
            //    ViewBag.ShowAlert = true;
            //    return View(brand);

            //}
            local.Deleted = true;
            db.Entry(local).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocalName,GoogleMapsURL,LocalId")] Location location)
        {
            if (ModelState.IsValid)
            {
                Location locationToEdit = db.Locations.Find(location.LocationId);
                if (locationToEdit == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    locationToEdit.GoogleMapsURL = location.GoogleMapsURL;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            }
            return View(location);
        }
    }
}
