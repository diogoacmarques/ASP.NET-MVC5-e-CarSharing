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
        public ActionResult Index(SearchVehicleViewModel ViewModel) {

            ViewModel = FillSearchViewModel(ViewModel);

            if (ModelState.IsValid)
            {
                if (ViewModel.RoleId != null)
                {
                    ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.User.Roles.Where(r => r.RoleId == ViewModel.RoleId).Any() ? true : false).OrderBy(v => v.VehicleId);
                }

                    if (ViewModel.TypeId != null)
                {
                    ViewModel.Brands = new SelectList(db.Brands.Where(b => b.TypeId == ViewModel.TypeId && b.Deleted == false), "BrandId", "BrandName");
                    ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.TypeId == ViewModel.TypeId).OrderBy(v => v.VehicleId);
                }
                else
                {
                    ViewModel.BrandId = null;
                    ViewModel.ModelId = null;

                }
                   


                if (ViewModel.BrandId != null)
                {
                    ViewModel.Models = new SelectList(db.Models.Where(m => m.BrandId == ViewModel.BrandId && m.Deleted == false), "ModelId", "ModelName");
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
                    ViewModel.Vehicles = ViewModel.Vehicles.Where(v => v.vehiclePassengers == ViewModel.VehiclePassengers).OrderBy(v => v.VehicleId);
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
             //.Include(v => v.HourlyPrice)
             //.Include(v => v.DailyPrice)
             //.Include(v => v.vehiclePassengers)
             //.Include(v => v.VehicleState)
             //.Where(v => v.User.UserStateId == UsersStatesConstants.USERSTATE_ACTIVE_ID)
             //.Where(v => v.VehicleStateId == ActiveStateId && v.Deleted == false)
             .OrderBy(v => v.VehicleId);

            ViewModel.Vehicles = Vehicles.ToList();

            ViewModel.Roles = AccountStaticRoles.GetRolesListForUser();
            ViewModel.Types = new SelectList(db.Types.Where(t => t.Deleted == false), "TypeId", "TypeName");
            ViewModel.Brands = new SelectList(db.Brands.Where(b => b.BrandId == -1), "BrandId", "BrandName");
            ViewModel.Models = new SelectList(db.Models.Where(m => m.ModelId == -1), "ModelId", "ModelName"); 
            ViewModel.Colours = new SelectList(db.Colours, "ColourId", "ColourName");
            ViewModel.Locations = new SelectList(db.Locations.Where(m => m.Deleted == false), "LocationId", "LocationName");
            return ViewModel;
        }

        public ActionResult Create()
        {

            VehicleViewModelCreate model = new VehicleViewModelCreate();
            model.Brands = new SelectList(db.Brands.Where(b => b.Deleted == false), "BrandId", "BrandName");
            model.Models = new SelectList(db.Models.Where(m => m.Deleted == false), "ModelId", "ModelName");
            model.Locations = new SelectList(db.Locations.Where(l => l.Deleted == false), "LocationId", "LocationName");
            model.Types = new SelectList(db.Types.Where(t => t.Deleted == false), "TypeId", "TypeName");
            model.Colours = new SelectList(db.Colours.Where(c => c.Deleted == false), "ColourId", "ColourName");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(VehicleViewModelCreate vehicle)
        {
            if (ModelState.IsValid)
            {
                var newVehicle = new Vehicle();
                //var conditionsDistinct = vehicle.RentConditions.DistinctBy(c => c.RentConditionId).ToList();
                //var LocationsDistinct = vehicle.VehicleLocalizations.DistinctBy(c => c.VehicleLocalizationId).ToList();

                //foreach (var item in conditionsDistinct)
                //{
                //    var condition = db.RentConditions.Find(item.RentConditionId);
                //    if (condition != null && condition.UserId == IdUser)
                //        NovoVeiculo.RentConditions.Add(condition);
                //}

                //foreach (var item in LocationsDistinct)
                //{
                //    var location = db.VehicleLocalizations.Find(item.VehicleLocalizationId);
                //    if (location != null && location.UserId == IdUser)
                //        NovoVeiculo.VehicleLocalizations.Add(location);
                //}


                newVehicle.TypeId = vehicle.TypeId;
                newVehicle.BrandId = vehicle.BrandId;
                newVehicle.ModelId = vehicle.ModelId;
                newVehicle.ColourId = vehicle.ColourId;
                newVehicle.vehiclePassengers = vehicle.vehiclePassengers;
                newVehicle.UserId = User.Identity.GetUserId();
                newVehicle.LocationId = vehicle.LocationId;
                newVehicle.VehicleState = VehicleState.VEHICLESTATE_PENDING_ID;
                newVehicle.HourlyPrice = vehicle.HourlyPrice;
                newVehicle.DailyPrice = vehicle.DailyPrice;           
                newVehicle.Deleted = false;
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


        //public ActionResult Delete(int id)
        //{
        //    Veiculo v = VeiculoMockTmpData.ListaVeiculos.FirstOrDefault(s => s.Id == id);
        //    if (v == null) return RedirectToAction("Index");

        //    return View(v);
        //}

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    Veiculo v = VeiculoMockTmpData.ListaVeiculos.FirstOrDefault(s => s.Id == id);
        //    if (v == null) return RedirectToAction("Index");

        //    try
        //    {
        //        VeiculoMockTmpData.ListaVeiculos.Remove(v);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}





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

            VehicleDetails.VehicleId = vehicle.VehicleId;
            VehicleDetails.Type = db.Types.Find(vehicle.TypeId);
            VehicleDetails.Brand = db.Brands.Find(vehicle.BrandId);
            VehicleDetails.Model = db.Models.Find(vehicle.ModelId);
            VehicleDetails.Location = db.Locations.Find(vehicle.LocationId);
            VehicleDetails.Colour = db.Colours.Find(vehicle.ColourId);
            VehicleDetails.vehiclePassengers = vehicle.vehiclePassengers;
            VehicleDetails.HourlyPrice = vehicle.HourlyPrice;
            VehicleDetails.DailyPrice = vehicle.DailyPrice;
            return View(VehicleDetails);
        }

        //public ActionResult Edit(int id)
        //{
        //    ViewBag.tipo = new List<SelectListItem>{
        //        new SelectListItem { Selected = true, Text = "Carro", Value = "Carro"},
        //        new SelectListItem { Selected = false, Text = "Trotinete", Value = "Trotinete"},
        //        new SelectListItem { Selected = false, Text = "Bicicleta", Value = "Bicicleta"},
        //    };

        //    Veiculo v = VeiculoMockTmpData.ListaVeiculos.FirstOrDefault(s => s.Id == id);
        //    if (v == null) return RedirectToAction("Index");

        //    return View(v);
        //}

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{

        //    Veiculo v = VeiculoMockTmpData.ListaVeiculos.FirstOrDefault(s => s.Id == id);
        //    if (v == null) return RedirectToAction("Index");

        //    try
        //    {
        //        v.Tipo = collection["Tipo"];
        //        v.Marca = collection["Marca"];
        //        v.Modelo = collection["Modelo"];

        //        if (int.TryParse(collection["Passageiros"], out int passageiros))
        //            if (passageiros > 0 && passageiros <= 7)
        //                v.Passageiros = passageiros;
        //            else return View(v);
        //        else return View(v);


        //        return RedirectToAction("ListAllVehicles");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        public ActionResult MyVehicles()
        {
            var userId = User.Identity.GetUserId();

            var MyVehicles = db.Vehicles
              .Include(v => v.User)
              .Include(v => v.Type)
              .Include(v => v.Brand)
              .Include(v => v.Model)
              .Include(v => v.Colour)
              .Include(v => v.Location)
              .Where(v => v.User.Id == userId)
              .OrderBy(v => v.VehicleId);

            return View(MyVehicles);
        }



        //public ActionResult ListAllVehicles()
        //{
        //    return View(VeiculoMockTmpData.ListaVeiculos);
        //}

        //public ActionResult ListMyCars(int id)
        //{
        //    return View();
        //}

        //public ActionResult VehiclesDetails(int id)
        //{
        //    return View(id);
        //}

    }
}