using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Net;
using PagedList;

namespace e_CarSharing.Controllers
{
    public class VehicleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index(int? LocationId, DateTime? BeginDate)
        {
            var ViewModel = new SearchVehicleViewModel();

            ViewModel = FillSearchViewModel(ViewModel);

            ViewModel.LocationId = LocationId;

            if (LocationId != null)
                ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.LocationId == ViewModel.LocationId).OrderBy(v => v.VehicleId);

            //if(BeginDate != null)


            return View(ViewModel);

        }

        [HttpPost]
        public ActionResult Index(SearchVehicleViewModel ViewModel)
        {

            ViewModel = FillSearchViewModel(ViewModel);

            if (ModelState.IsValid)
            {
                if (ViewModel.RoleId != null)
                {
                ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.User.UserRole == ViewModel.RoleId).OrderBy(v => v.VehicleId);
                }

                if (ViewModel.TypeId != null)
                {
                    ViewModel.Brands = new SelectList(db.Brands.Where(b => b.TypeId == ViewModel.TypeId && b.Deleted == false), "BrandId", "BrandName");
                    ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.TypeId == ViewModel.TypeId).OrderBy(v => v.VehicleId);
                    ViewModel.ModelId = null;
                }

                if (ViewModel.BrandId != null)
                {
                    ViewModel.Models = new SelectList(db.Models.Where(m => m.BrandId == ViewModel.BrandId && m.Brand.TypeId == ViewModel.TypeId && m.Deleted == false), "ModelId", "ModelName");
                    ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.BrandId == ViewModel.BrandId).OrderBy(v => v.VehicleId);
                }

                if (ViewModel.ModelId != null)
                {
                    ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.ModelId == ViewModel.ModelId).OrderBy(v => v.VehicleId);
                }

                if (ViewModel.ColourId != null)
                {
                    ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.ColourId == ViewModel.ColourId).OrderBy(v => v.VehicleId);
                }

                if (ViewModel.VehiclePassengers != null)
                {
                    ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.VehiclePassengers == ViewModel.VehiclePassengers).OrderBy(v => v.VehicleId);
                }


                if (ViewModel.LocationId != null)
                {
                    ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.LocationId == ViewModel.LocationId).OrderBy(v => v.VehicleId);
                }

            }

            return View(ViewModel);
        }

        [NonAction]
        private SearchVehicleViewModel FillSearchViewModel(SearchVehicleViewModel ViewModel)
        {

            var Vehicles = db.Vehicles
             .Include(v => v.User)
             .Include(v => v.Type)
             .Include(v => v.Brand)
             .Include(v => v.Model)
             .Include(v => v.Colour)
             .Include(v => v.Location)
             .Where(v => v.Deleted == false)
             .Where(v => v.VehicleStateId == VehicleState.VEHICLESTATE_ACCEPTED_ID)
             .OrderBy(v => v.VehicleId);

            ViewModel.Vehicles = Vehicles.ToList();

            ViewModel.Roles = AccountStaticRoles.GetRolesListForVehicleSearch();
            ViewModel.Types = new SelectList(db.Types.Where(t => t.Deleted == false), "TypeId", "TypeName");
            ViewModel.Brands = new SelectList(db.Brands.Where(b => b.BrandId == -1), "BrandId", "BrandName");
            ViewModel.Models = new SelectList(db.Models.Where(m => m.ModelId == -1), "ModelId", "ModelName");
            ViewModel.Colours = new SelectList(db.Colours, "ColourId", "ColourName");
            ViewModel.Locations = new SelectList(db.Locations.Where(m => m.Deleted == false), "LocationId", "LocationName");
            return ViewModel;
        }

        [Authorize(Roles = AccountStaticRoles.PRIVATE + "," + AccountStaticRoles.PROFESSIONAL)]
        public ActionResult Create()
        {

            VehicleViewModelCreate model = new VehicleViewModelCreate
            {
                Brands = new SelectList(db.Brands.Where(b => b.Deleted == false), "BrandId", "BrandName"),
                Models = new SelectList(db.Models.Where(m => m.Deleted == false), "ModelId", "ModelName"),
                Locations = new SelectList(db.Locations.Where(l => l.Deleted == false), "LocationId", "LocationName"),
                Types = new SelectList(db.Types.Where(t => t.Deleted == false), "TypeId", "TypeName"),
                Colours = new SelectList(db.Colours.Where(c => c.Deleted == false), "ColourId", "ColourName")
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(VehicleViewModelCreate vehicle)
        {
            if (ModelState.IsValid)
            {
                var newVehicle = new Vehicle
                {
                    TypeId = vehicle.TypeId,
                    BrandId = vehicle.BrandId,
                    ModelId = vehicle.ModelId,
                    ColourId = vehicle.ColourId,
                    VehiclePassengers = vehicle.vehiclePassengers,
                    UserId = User.Identity.GetUserId(),
                    LocationId = vehicle.LocationId,
                    VehicleStateId = VehicleState.VEHICLESTATE_PENDING_ID,
                    HourlyPrice = vehicle.HourlyPrice,
                    DailyPrice = vehicle.DailyPrice,
                    Deleted = false
                };
                db.Vehicles.Add(newVehicle);
                //var VehicleEvaluation = new VehicleEvaluation() { VehicleId = NovoVeiculo.VehicleId };
                //db.VehicleEvaluations.Add(VehicleEvaluation);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                vehicle.Brands = new SelectList(db.Brands, "BrandId", "BrandName");
                vehicle.Models = new SelectList(db.Models, "ModelId", "ModelName");
                vehicle.Locations = new SelectList(db.Locations, "LocationId", "LocationName");
                vehicle.Types = new SelectList(db.Types, "TypeId", "TypeName");
                vehicle.Colours = new SelectList(db.Colours, "ColourId", "ColourName");
                return View(vehicle);
            }
        }

        [Authorize(Roles = AccountStaticRoles.PRIVATE + "," + AccountStaticRoles.PROFESSIONAL)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");



            Vehicle vehicle = new Vehicle();
            vehicle = db.Vehicles.Find(id);

            if (vehicle == null)
            {
                return HttpNotFound();
            }


            vehicle.Type = db.Types.Find(vehicle.TypeId);
            vehicle.Brand = db.Brands.Find(vehicle.BrandId);
            vehicle.Model = db.Models.Find(vehicle.ModelId);
            vehicle.Colour = db.Colours.Find(vehicle.ColourId);

            vehicle.Location = db.Locations.Find(vehicle.LocationId);

            vehicle.User = db.Users.Find(vehicle.UserId);


            return View(vehicle);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            //    Veiculo v = VeiculoMockTmpData.ListaVeiculos.FirstOrDefault(s => s.Id == id);
            //    if (v == null) return RedirectToAction("Index");

            //    try
            //    {
            //        VeiculoMockTmpData.ListaVeiculos.Remove(v);
            //        return RedirectToAction("Index");
            //    }
            //    catch
            //    {
            return View();
            //    }
        }


        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleViewModelDetails VehicleDetails = new VehicleViewModelDetails();
            Vehicle vehicle = db.Vehicles.Find(id);
    
            if (vehicle == null)
            {
                return HttpNotFound();
            }



            var user = db.Users.Find(User.Identity.GetUserId());

            if (User.IsInRole(AccountStaticRoles.ADMINISTRATOR))
            {
                VehicleDetails.IsAdmin = true;
                VehicleDetails.States = new SelectList(db.VehicleStates, "VehicleStateId", "VehicleStateName");
            }else if (User.IsInRole(AccountStaticRoles.MOBILITY) || user == null)
            {
                VehicleDetails.CanRent = true;
            }
            else
            {
                VehicleDetails.IsAdmin = false;
                VehicleDetails.CanRent = false;
            }


            VehicleDetails.VehicleId = vehicle.VehicleId;
            VehicleDetails.Owner = db.Users.Find(vehicle.UserId);
            VehicleDetails.Type = db.Types.Find(vehicle.TypeId);
            VehicleDetails.Brand = db.Brands.Find(vehicle.BrandId);
            VehicleDetails.Model = db.Models.Find(vehicle.ModelId);
            VehicleDetails.Location = db.Locations.Find(vehicle.LocationId);
            VehicleDetails.Colour = db.Colours.Find(vehicle.ColourId);
            VehicleDetails.vehiclePassengers = vehicle.VehiclePassengers;
            VehicleDetails.HourlyPrice = vehicle.HourlyPrice;
            VehicleDetails.DailyPrice = vehicle.DailyPrice;
            VehicleDetails.VehicleState = db.VehicleStates.Find(vehicle.VehicleStateId);

            if(vehicle.VehicleStateId != null)
            {
                VehicleDetails.VehicleStateId = (int)vehicle.VehicleStateId;
            }
            else
            {
                VehicleDetails.VehicleStateId = -1;
            }
          
            return View(VehicleDetails);
        }

        [HttpPost]
        public ActionResult Details(VehicleViewModelDetails VehicleDetails)
        {
            if (User.IsInRole(AccountStaticRoles.ADMINISTRATOR))
            {
                if (ModelState.IsValid)
                {
                    Vehicle vehicle = db.Vehicles.Find(VehicleDetails.VehicleId);

                    if(vehicle != null)
                    {
                        vehicle.VehicleStateId = VehicleDetails.VehicleStateId;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                return HttpNotFound();
            }
            VehicleDetails.States = new SelectList(db.VehicleStates, "VehicleStateId", "VehicleStateName");
            return View(VehicleDetails);
        }

        [Authorize(Roles = AccountStaticRoles.PRIVATE + "," + AccountStaticRoles.PROFESSIONAL + "," + AccountStaticRoles.ADMINISTRATOR)]
        public ActionResult MyVehicles()
        {
            var userId = User.Identity.GetUserId();

            if (User.IsInRole(AccountStaticRoles.ADMINISTRATOR))
            {
                var AllVehicles = db.Vehicles
                   .Include(v => v.User)
                   .Include(v => v.Type)
                   .Include(v => v.Brand)
                   .Include(v => v.Model)
                   .Include(v => v.Colour)
                   .Include(v => v.Location)
                   .Include(v => v.VehicleState)
                   .OrderBy(v => v.VehicleId);

                return View(AllVehicles);
            }

            var MyVehicles = db.Vehicles
           .Include(v => v.User)
           .Include(v => v.Type)
           .Include(v => v.Brand)
           .Include(v => v.Model)
           .Include(v => v.Colour)
           .Include(v => v.Location)
           .Include(v => v.VehicleState)
           .Where(v => v.User.Id == userId)
           .OrderBy(v => v.VehicleId);

            return View(MyVehicles);
        }


    }
}