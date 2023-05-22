//using PROJECT.Models;
//using System;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using HtmlAgilityPack;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//namespace PROJECT.Controllers
//{
//    public class searchController : Controller
//    {
//        // GET: search
//        FashionEaseEntities2 db = new FashionEaseEntities2();

//        public JsonResult GetCustomers(string term)

//        {

//            List<string> Products = db.Products.Where(s => s.Name.StartsWith(term))

//                .Select(x => x.Name).ToList();

//            return Json(Products, JsonRequestBehavior.AllowGet);
//        }
//    }
//}