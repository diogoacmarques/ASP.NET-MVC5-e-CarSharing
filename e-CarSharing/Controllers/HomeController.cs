using e_CarSharing.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (db.Users.Where(u => u.UserName == "Admin").Count() == 0)//make sure that exists a admin
            {
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@echarsharing.pt",
                    UserRole = AccountStaticRoles.ADMINISTRATOR,
                };
                userManager.Create(user, "Admin123_");
                userManager.AddToRole(user.Id, AccountStaticRoles.ADMINISTRATOR);
                db.SaveChanges();
                return RedirectToAction("Index", "AdminAccounts", new { @area = "Administration" });
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}