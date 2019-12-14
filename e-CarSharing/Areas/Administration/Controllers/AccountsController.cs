using e_CarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Areas.Administration.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using System.Data.Entity;

namespace e_CarSharing.Areas.Administration.Controllers
{
    //[Authorize(Roles = AccountLevels.ADMINISTRATOR)]
    public class AccountsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administration/Accounts
        public ActionResult Index()
        {
            var all = db.Users;

            var listToReturn = from u in all
                               select new Account()
                               {
                                   Id = u.Id,
                                   UserName = u.UserName,
                                   Email = u.Email,
                               };


            return View(listToReturn);
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
                //user.UserStateId = UsersStatesConstants.USERSTATE_ACTIVE_ID;
                userManager.Create(user, model.Password);
                userManager.AddToRole(user.Id, AccountStaticRoles.ADMINISTRATOR);
                db.SaveChanges();
                return RedirectToAction("Index", "Accounts", new { @area = "Administration" });
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
                return RedirectToAction("Index", "Accounts", new { @area = "Administration" });
            }
            var usertoview = new Account() { Email = user.Email, Id = user.Id, UserName = user.UserName };
            return View(usertoview);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (id == User.Identity.GetUserId() || user == null)
            {
                View("Error");
            }            

            user.Roles.Clear();
            db.SaveChanges();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Accounts", new { @area = "Administration" });
        }


        public ActionResult Edit()//#################################################################################
        {
            return View();
        }



        public ActionResult ListAll(string id)
        {

            var tmp = id;
            ViewBag.typeOfAccount = AccountStaticRoles.GetRolesList();
            ViewBag.typeOfAccountSelected = id;

            if (id == AccountStaticRoles.ADMINISTRATOR)
            {
               
                var AccountList = db.Users
                 .Where(u => u.Roles.Where(r => (String.Compare(r.RoleId, AccountStaticRoles.ADMINISTRATOR_ID.ToString()) == 0 ? true : false)).Any());

                var listToReturn = from u in AccountList
                                   select new Account()
                                {
                                    Id = u.Id,
                                    UserName = u.UserName,
                                    Email = u.Email,
                                    Type = AccountStaticRoles.ADMINISTRATOR
                                };

                return View(listToReturn);
            }
            else if (id == AccountStaticRoles.PROFESSIONAL)
            {
                var AccountList = db.Users
                 .Where(u => u.Roles.Where(r => (String.Compare(r.RoleId, AccountStaticRoles.PROFESSIONAL_ID.ToString()) == 0 ? true : false)).Any());

                var listToReturn = from u in AccountList
                                   select new Account()
                                   {
                                       Id = u.Id,
                                       UserName = u.UserName,
                                       Email = u.Email,
                                       Type = AccountStaticRoles.PROFESSIONAL
                                   };

                return View(listToReturn);
            }
            else if (id == AccountStaticRoles.PRIVATE)
            {
                var AccountList = db.Users
               .Where(u => u.Roles.Where(r => (String.Compare(r.RoleId, AccountStaticRoles.PRIVATE_ID.ToString()) == 0 ? true : false)).Any());

                var listToReturn = from u in AccountList
                                   select new Account()
                                   {
                                       Id = u.Id,
                                       UserName = u.UserName,
                                       Email = u.Email,
                                       Type = AccountStaticRoles.PRIVATE
                                   };

                return View(listToReturn);
            }
            else
                return RedirectToAction("Index", "Accounts", new { @area = "Administration" });


        }


    }
}