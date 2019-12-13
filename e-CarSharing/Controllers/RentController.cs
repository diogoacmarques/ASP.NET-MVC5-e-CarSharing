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
    public class RentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent
        public ActionResult Index()
        {
            RentViewModel viewModel = new RentViewModel
            {
                Locations = new SelectList(db.Locations, "LocationId", "LocationName")
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(RentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index", "Vehicle", new { LocationId = viewModel.PickUpLocationId });
            }
            else
            {
                viewModel.Locations = new SelectList(db.Locations, "LocationId", "LocationName");
                return View(viewModel);
            }
        }


        public ActionResult Create(int? idVehicle)
        {
            var userId = User.Identity.GetUserId();

            return View();
        }

        [HttpPost]
        public ActionResult Create(RentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                return HttpNotFound();
            }
            else
            {

                return View(viewModel);
            }
        }


    }
}