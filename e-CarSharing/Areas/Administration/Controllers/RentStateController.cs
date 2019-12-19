using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;

namespace e_CarSharing.Areas.Administration.Controllers
{
    [Authorize(Roles = AccountStaticRoles.ADMINISTRATOR)]
    public class RentStateController : Controller
    {
        // GET: Administration/RentState
        public ActionResult Index()
        {
            return View();
        }


    }
}