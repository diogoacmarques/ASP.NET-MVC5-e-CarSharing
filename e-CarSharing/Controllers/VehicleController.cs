using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using e_CarSharing.Models;

namespace e_CarSharing.Controllers
{
    public class VehicleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
           // var ViewModel = new SearchVehicleViewModel();//
            //string UserId = User.Identity.GetUserId();
         /*   int ActiveStateId = VehicleState.VEHICLESTATE_ACCEPTED_ID;
            var Vehicles = db.Vehicles
                .Include(v => v.User)
                .Include(v => v.Brand)
                .Include(v => v.Model)
                .Include(v => v.Colour)
                .Include(v => v.VehicleDoor)
                .Include(v => v.VehicleSeat)
                .Include(v => v.Transmissions)
                .Include(v => v.VehicleState)
                .Where(v => v.User.UserStateId == UsersStatesConstants.USERSTATE_ACTIVE_ID)
                .Where(v => v.VehicleStateId == ActiveStateId && v.Deleted == false)
                .OrderBy(v => v.VehicleId);*/
            return View(db.Vehicles.ToList());
        }


        public ActionResult Create()
        {
            ViewBag.tipo = new List<SelectListItem>{
                new SelectListItem { Selected = true, Text = "Carro", Value = "Carro"},
                new SelectListItem { Selected = false, Text = "Trotinete", Value = "Trotinete"},
                new SelectListItem { Selected = false, Text = "Bicicleta", Value = "Bicicleta"},
            };

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Vehicle novo = new Vehicle();
                novo.BrandId = 1;

                db.Vehicles.Add(novo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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





        //public ActionResult Details(int id)
        //{
        //    Veiculo v = VeiculoMockTmpData.ListaVeiculos.FirstOrDefault(s => s.Id == id);
        //    if (v == null) return RedirectToAction("Index");

        //    return View(v);
        //}

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