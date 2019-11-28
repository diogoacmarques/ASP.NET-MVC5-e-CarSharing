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
        public ActionResult Index()//list all
        {
            string userid = User.Identity.GetUserId();
            var users = db.Users;
               // .Where(u => u.Roles.Where(r => (String.Compare(r.RoleId, AccountLevels.ADMINISTRATOR_ID.ToString()) == 0 ? true : false)).Any());
                //.Where(u => u.UserStateId == UsersStatesConstants.USERSTATE_ACTIVE_ID);

            var admins = from u in users
                         select new ListAccounts()
                         {
                             Id = u.Id,
                             UserName = u.UserName,
                             Email = u.Email
                         };

            var adminsExceptAtual = admins.Where(a => string.Compare(a.Id, userid) != 0 ? true : false).ToList();
            return View(adminsExceptAtual);
        }






        public ActionResult Create() //####################################################################
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,UserName,Password,ConfirmPassword")] CreateAccount model)
        {
            if (ModelState.IsValid)
            {

                if (db.Users.Where(u => u.UserName == model.UserName).Count() != 0)
                {
                    ModelState.AddModelError("1", "Já existe um Administrador com esse Username");
                    return View(model);
                }

                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                //user.UserStateId = UsersStatesConstants.USERSTATE_ACTIVE_ID;
                userManager.Create(user, model.Password);
                db.SaveChanges();
                userManager.AddToRole(user.Id, AccountLevels.ADMINISTRATOR);
                db.SaveChanges();
                return RedirectToAction("Index", "Accounts", new { @area = "Administration" });
            }

            return NewMethod(model);
        }

        private ActionResult NewMethod(CreateAccount model)
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
            var usertoview = new ListAccounts() { Email = user.Email, Id = user.Id, UserName = user.UserName };
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




    }
}