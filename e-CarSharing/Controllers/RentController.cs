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
    //[Authorize(Roles = AccountStaticRoles.PRIVATE + "," + AccountStaticRoles.PROFESSIONAL + "," + AccountStaticRoles.MOBILITY)]
    public class RentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent
        public ActionResult Index()
        {
            var list = db.Rents
                .Include(r => r.RentState)
                .Include(v => v.Vehicle.Brand)
                .Include(p => p.Vehicle.User)
                .Include(c => c.Client)
                .Include(p => p.PickUpLocation)
                .Include(d => d.DeliveryLocation)
                .ToList();

            return View(list);
        }

        public ActionResult MyRents()
        {
            var UserId = User.Identity.GetUserId();
            var Rents = db.Rents
               .Include(r => r.RentState)
               .Include(v => v.Vehicle.Brand)
               .Include(p => p.Vehicle.User)
               .Include(c => c.Client)
               .Include(p => p.PickUpLocation)
               .Include(d => d.DeliveryLocation)
               .Where(i => i.Vehicle.UserId == UserId)
               .ToList();

            return View(Rents);
        }

        public ActionResult CheckVehicleAvailability(int Id)
        {
            var list = db.Rents
                .Include(v => v.Vehicle.Brand)
                .Include(p => p.Vehicle.User)
                .Where(r => r.RentId == Id)
                .Where(r => r.RentStateId == RentState.RENTSTATE_ACCEPTED_ID)
                .ToList();

            return View(list);
        }


        public ActionResult ChangeRentState(int RentId)
        {
            var rent = db.Rents
                .Include(r => r.RentState)
               .Include(v => v.Vehicle.Brand)
               .Include(p => p.Vehicle.User)
               .Include(c => c.Client)
               .Include(p => p.PickUpLocation)
               .Include(d => d.DeliveryLocation)
               .Where(i => i.RentId == RentId)
               .FirstOrDefault();

            RentViewModelChangeState ViewModel = new RentViewModelChangeState
            {
                Rent = rent,
                RentStateList = RentState.GetStatesList()
            };
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult ChangeRentState(RentViewModelChangeState ViewModel)
        {
            var rentToChange = db.Rents.Find(ViewModel.Rent.RentId);
            var tmp = rentToChange;
            if (ViewModel.Rent.RentStateId == RentState.RENTSTATE_ACCEPTED_ID)
            {
                if (CheckAvailability(rentToChange.VehicleId, rentToChange.BeginDate, rentToChange.EndDate))
                {  
                    rentToChange.RentStateId = RentState.RENTSTATE_ACCEPTED_ID;
                    db.Entry(rentToChange).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("MyRents");
                }
                else
                {
                    ModelState.AddModelError("", "Esta data não se encontra disponível");
                }
            }
            else
            {
                rentToChange.RentStateId = ViewModel.Rent.RentStateId;
                db.Entry(rentToChange).State = EntityState.Modified;
                return RedirectToAction("MyRents");
            }

            ViewModel.RentStateList = RentState.GetStatesList();
            return View(ViewModel);
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

  
            RentViewModelCreate ViewModel = new RentViewModelCreate();
            var UserId = User.Identity.GetUserId();
            if (UserId == null)
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

                //create rent(pendente)
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

            return View(ViewModel);
        }

        [NonAction]
        public bool CheckAvailability(int VehicleId,DateTime BeginDate, DateTime EndDate)
        {
            var vehicle = db.Vehicles.Find(VehicleId);
            if(vehicle != null)
            {
                var list = db.Rents
                    .Where(v => v.VehicleId == VehicleId)
                    .Where(r => r.RentStateId == RentState.RENTSTATE_ACCEPTED_ID)
                    .Where(b => (EndDate >= b.BeginDate && BeginDate <= b.BeginDate) || 
                    (BeginDate <= b.EndDate && EndDate >= b.EndDate) || 
                    (BeginDate >= b.BeginDate && EndDate <= b.EndDate))
                    .ToList();

                if (list.Count() == 0)
                    return true;
                else
                    return false;
            }

            return false;
        }


    }
}