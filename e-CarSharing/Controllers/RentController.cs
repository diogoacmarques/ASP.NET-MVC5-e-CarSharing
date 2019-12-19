using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using PagedList;

namespace e_CarSharing.Controllers
{
    [Authorize(Roles = AccountStaticRoles.PRIVATE + "," + AccountStaticRoles.PROFESSIONAL + "," + AccountStaticRoles.MOBILITY)]
    public class RentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent
        public ActionResult Index()
        {
            var list = db.Rents
                .Include(r => r.RentState)
                .Include(v => v.Vehicle)
                .Include(c => c.Client)
                .Include(p => p.PickUpLocation)
                .Include(d => d.DeliveryLocation)
                .ToList();

            return View(list);
        }


        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }


            RentViewModelCreate ViewModel = new RentViewModelCreate();
            var UserId = User.Identity.GetUserId();
            if(UserId==null)
                ModelState.AddModelError(string.Empty, "Utilizador inválido");
            ViewModel.ClientId = UserId;
            ViewModel.Client = db.Users.Find(UserId);
            ViewModel.VehicleId = (int)id;
            ViewModel.Vehicle = db.Vehicles.Find(ViewModel.VehicleId);
            ViewModel.Vehicle.Brand = db.Brands.Find(ViewModel.Vehicle.BrandId);
            
            ViewModel.Locations = new SelectList(db.Locations.Where(m => m.Deleted == false), "LocationId", "LocationName");

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RentViewModelCreate ViewModel)
        {
            ViewModel.Locations = new SelectList(db.Locations.Where(m => m.Deleted == false), "LocationId", "LocationName");

            if (ModelState.IsValid)
            {
                //check dates
                if (ViewModel.BeginDate >= ViewModel.EndDate || DateTime.Now >= ViewModel.BeginDate)
                {
                    ModelState.AddModelError(string.Empty, "Por favor verifique as datas");
                    return View(ViewModel);              
                }

                //check availabilty
                if (CheckAvailability(ViewModel.VehicleId, ViewModel.BeginDate, ViewModel.EndDate)){
                    //create rent
                    Rent newRent = new Rent();
                    newRent.VehicleId = ViewModel.VehicleId;
                    newRent.ClientId = ViewModel.ClientId;
                    newRent.BeginDate = ViewModel.BeginDate;
                    newRent.EndDate = ViewModel.EndDate;
                    newRent.DeliveryLocationId = ViewModel.DeliveryLocationId;
                    newRent.PickUpLocationId = ViewModel.PickUpLocationId;
                    newRent.RentStateId = RentState.RENTSTATE_PENDING_ID;
                    newRent.Deleted = false;
                    db.Rents.Add(newRent);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

            }


            return View(ViewModel);
        }

        [NonAction]
        public bool CheckAvailability(int VehicleId,DateTime BeginDate, DateTime EndDate)
        {
            var vehicle = db.Vehicles.Find(VehicleId);
            if(vehicle != null)
            {

            }



            return true;
        }


    }
}