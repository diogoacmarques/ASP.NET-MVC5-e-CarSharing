using e_CarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Areas.Administration.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using System.Data.Entity;

namespace e_CarSharing.Areas.Administration.Controllers
{
    [Authorize(Roles = AccountStaticRoles.ADMINISTRATOR)]
    public class AdminAccountsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administration/Accounts
        public ActionResult Index()
        {
            SearchAccoutViewModel ViewModel = new SearchAccoutViewModel();

            ViewModel.Users = db.Users.ToList();
            ViewModel.Roles = AccountStaticRoles.GetRolesList();

            return View(ViewModel);
        }

        // GET: Administration/Accounts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SearchAccoutViewModel ViewModel)
        {
            ViewModel.Users = db.Users.ToList();
            ViewModel.Roles = AccountStaticRoles.GetRolesList();

            if (ModelState.IsValid)
            {

                if (ViewModel.Role == AccountStaticRoles.ADMINISTRATOR)
                    ViewModel.Users = ViewModel.Users.Where(u => u.UserRole == AccountStaticRoles.ADMINISTRATOR);

                if (ViewModel.Role == AccountStaticRoles.PRIVATE)
                    ViewModel.Users = ViewModel.Users.Where(u => u.UserRole == AccountStaticRoles.PRIVATE);

                if (ViewModel.Role == AccountStaticRoles.PROFESSIONAL)
                    ViewModel.Users = ViewModel.Users.Where(u => u.UserRole == AccountStaticRoles.PROFESSIONAL);

                if (ViewModel.Role == AccountStaticRoles.MOBILITY)
                    ViewModel.Users = ViewModel.Users.Where(u => u.UserRole == AccountStaticRoles.MOBILITY);

            }

            return View(ViewModel);
        }


        public ActionResult Create() //####################################################################
        {
          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,UserName,Password,ConfirmPassword")] Account model)
        {
            if (ModelState.IsValid)
            {
              
                if (db.Users.Where(u => u.UserName == model.UserName).Count() != 0)
                {
                    ModelState.AddModelError("1", "Já existe um utilizador com esse Username");
                    return View(model);
                }

                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = new ApplicationUser {
                    UserName = model.UserName,
                    Email = model.Email,
                    UserRole = AccountStaticRoles.ADMINISTRATOR,
                };
                userManager.Create(user, model.Password);
                userManager.AddToRole(user.Id, AccountStaticRoles.ADMINISTRATOR);
                db.SaveChanges();
                return RedirectToAction("Index", "AdminAccounts", new { @area = "Administration" });
            }

            return View(model);
        }

        private ActionResult NewMethod(Account model)
        {
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "AdminAccounts", new { @area = "Administration" });
            }
            var usertoview = new Account() { Email = user.Email, Id = user.Id, UserName = user.UserName };
            return View(usertoview);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var userView = new Account();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (id == User.Identity.GetUserId() || user == null)
            {
                View("Error");
            }

            if(db.Vehicles.Where(v => v.UserId == user.Id).Count() != 0)
            {
                ModelState.AddModelError(string.Empty, "Este utilizador ainda possui veículos!");
                userView.UserName = user.UserName;
                userView.Email = user.Email;
                return View(userView);
            }

            user.Roles.Clear();
            db.SaveChanges();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index", "AdminAccounts", new { @area = "Administration" });
        }

    }
}