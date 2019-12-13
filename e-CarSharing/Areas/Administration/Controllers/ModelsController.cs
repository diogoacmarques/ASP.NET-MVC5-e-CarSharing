using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;
using e_CarSharing.Areas.Administration.ViewModels;
using Newtonsoft.Json;

namespace e_CarSharing.Areas.Administration.Controllers
{
    public class ModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administration/Models
        public ActionResult Index()
        {
            return View(db.Models.Include(m => m.Brand).Where(m => m.Deleted == false).ToList());
        }


        // GET: Administration/Models/Create#################################################################################
        public ActionResult Create()
        {
            ModelViewModelCreate createModel = new ModelViewModelCreate();
            createModel.Brands = new SelectList(db.Brands.Where(t => t.Deleted == false), "BrandId", "BrandName");
            return View(createModel);
        }

        // POST: Administration/Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelViewModelCreate ViewModel)
        {

            Model model = new Model
            {
                ModelName = ViewModel.ModelName,
                BrandId = ViewModel.BrandId
            };

            if (ModelState.IsValid)
            {
                var list = db.Models.Where(m => m.ModelName == model.ModelName && m.Deleted == false).Count();
                if (list != 0)
                {
                    ModelState.AddModelError(string.Empty, " Já existe esse modelo!");
                    ViewModel.Brands = new SelectList(db.Brands.Where(t => t.Deleted == false), "BrandId", "BrandName");
                    return View(ViewModel);
                }
                else if (db.Models.Where(m => m.ModelName == model.ModelName && m.Deleted == true).Count() != 0)
                {
                    Model modelExists = db.Models.Where(m => m.ModelName == model.ModelName).First();
                    modelExists.Deleted = false;
                    modelExists.BrandId = model.BrandId;
                    db.Entry(modelExists).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                db.Models.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewModel.Brands = new SelectList(db.Brands.Where(t => t.Deleted == false), "BrandId", "BrandName");
            return View(ViewModel);

        }

        // GET: Administration/Models/Delete/5 ###############################################################################
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Brand = db.Brands.Find(model.BrandId);
            return View(model);
        }

        // POST: Administration/Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Model model = db.Models.Find(id);
            if (db.Vehicles.Where(v => v.ModelId == model.ModelId && v.Deleted == false).Count() != 0)
            {
                ViewBag.AlertText = " O modelo não pode ser eliminado por estar em utilização!";
                ViewBag.ShowAlert = true;
                return View(model);

            }
            model.Deleted = true;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        // GET: Administration/Models/Edit/5##################################################################
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Administration/Models/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModelId,ModelName")] Model model)
        {
            if (ModelState.IsValid)
            {
                var list = db.Models.Where(m => m.ModelName == model.ModelName).Count();
                if (list != 0)
                {
                    ModelState.AddModelError(string.Empty, "* Já existe esse modelo!");
                    return View(model);
                }
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}