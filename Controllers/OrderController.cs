using PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECT.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        FashionEaseEntities2 db = new FashionEaseEntities2();
        public ActionResult Index()
        {
          
            return View();
        }
    }
}