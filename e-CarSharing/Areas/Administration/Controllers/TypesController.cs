using e_CarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Areas.Administration.Controllers
{
    public class TypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administration/Type
        public ActionResult Index()
        {
            return View(db.Types.Where(b => b.Deleted == false).ToList());
        }


    }
}