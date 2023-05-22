using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJECT.Models;

namespace PROJECT.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class DashboardController : Controller
    {
        FashionEaseEntities2 db = new FashionEaseEntities2();

        // GET: Dashboard
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            ViewBag.list = db.Users.ToList().Where(x => x.IsActive == true).ToList();

            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult Vendors()
        {
            ViewBag.list = db.Users.ToList().Where(x => x.RoleId == 3 && x.IsActive == false).ToList();

            return View();
        }


        public ActionResult Orders()
        {
            ViewBag.Orders = db.OrderDetails.ToList();

            return View();
        }


    }
}