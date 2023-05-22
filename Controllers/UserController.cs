using PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PROJECT.Controllers
{
    public class UserController : Controller
    {

        FashionEaseEntities2 db = new FashionEaseEntities2();
        private long id;

        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Vendors()
        {
            ViewBag.list = db.Users.ToList().Where(x => x.RoleId == 3&&x.IsActive ==false).ToList() ;

            return View();
        }
        [HttpPost]
        public ActionResult Accept(int id)
        {
            var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = true;
            }
            db.SaveChanges();
            return RedirectToAction("Vendors","Dashboard");

        }


        [Authorize]
        [Authorize(Roles="ADMIN")]
        public ActionResult Index()
        {
            ViewBag.list = db.Users.ToList().Where(x => x.IsActive==true).ToList();

            return View();
        }

        public ActionResult Home()
       {
            ViewBag.list = db.Users.ToList().Where(x => x.IsActive == true).ToList();
   
            return View();
       }

        [HttpGet] //get for data gathering
        public ActionResult Registration()
        {
            

            return View();
        }
        [HttpPost] // yhn sy database sy data pick kar k submit hoga
        public ActionResult Registration( User u)
        {

            if (db.Users.Where(x => x.Email == u.Email).ToList().Count > 0)
            {
                TempData["EmailExists"] = "Email Address Already Exists*!";
            }
            else if (u.Contact.Length != 11)
            {

                TempData["ContactError"] = "Contact Number should be of 11 digits!";

            }
            else { 
           
                if (u.RoleId != 3)
                {
                    u.IsActive = true;
                    u.CreatedOn = DateTime.Now;
                    db.Users.Add(u);
                    db.SaveChanges();
                }
                else
                {
                    u.IsActive = false;
                    u.CreatedOn = DateTime.Now;
                    db.Users.Add(u);
                    db.SaveChanges();
                    TempData["adminApproval"] = "You are registered, kindly wait for admin approval!"; ;
                }
             
           

           
            }
            return Redirect(Url.Action("Index", "Home") );
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {

            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Index","Dashboard");
        }



        public ActionResult Edit(int id)
        {
            ViewBag.user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {

            var data = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            if (data != null)
            {
                data.Name = user.Name;

                data.Password = user.Password;

                data.IsActive = user.IsActive;

                data.RoleId = user.RoleId;
                data.UpdatedOn = DateTime.Now;
                data.Email = user.Email;
                db.SaveChanges();
            }
            return RedirectToAction("Index","Dashboard");
        }


        public ActionResult Update(int Id)
        {
            using (var context = new FashionEaseEntities2())
            {
                ViewBag.user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                return View();
            }
        }
        [HttpPost]
        public ActionResult Update(User user)
        {
            using (var context = new FashionEaseEntities2())
            {

                // Use of lambda expression to access
                // particular record from a database
                var data = db.Users.FirstOrDefault(x => x.Id == user.Id);

                // Checking if any such record exist
                if (data != null)
                {
                    data.Name = user.Name;

                    data.Password = user.Password;

                    data.IsActive = user.IsActive;

                    data.RoleId = user.RoleId;
                    data.UpdatedOn = DateTime.Now;
                    data.Email = user.Email;
                    db.SaveChanges();

                    // It will redirect to
                    // the Read method
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                   return RedirectToAction("Index", "Dashboard");
            }
        
           
        }



        [HttpPost]
        public ActionResult Delete(int Id)
        {

            var user = db.Users.Where(x => x.Id == Id).FirstOrDefault();

            user.IsActive = false;

            db.SaveChanges();
            return RedirectToAction("Index","Dashboard");
        }

        //[HttpPost] //post for submit
        //public ActionResult Login(String email, String Password)
        //{

        //    var list = db.Users.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == Password).FirstOrDefault();

        //    if (list != null)
        //    {
        //        Session["id"] = list.Id;
        //        Session["name"] = list.Name;
        //        Session["roleid"] = list.RoleId;
        //        var roleid = list.RoleId;
        //        FormsAuthentication.SetAuthCookie(list.Email, false);
        //        if (roleid == 1)
        //        {
        //            return RedirectToAction("Index", "User");
        //        }
        //        else if (roleid == 2)
        //        {
                   

        //            return RedirectToAction("Home");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Product");
        //        }

        //    }
        //    TempData["LoginError"] = "Incorrect Details!";
        //    return View();

        //}

        [Authorize]
        public ActionResult UserProfile()
        {
            int id = Convert.ToInt32(Session["id"]);
            if(id != 0)
                ViewBag.data=  db.Users.ToList().Where(x=>x.Id  == id).FirstOrDefault();


            if(id!= 0) {

                //ViewBag.orders = db.Orders.ToList().Where(x => x.CreatedBy == id).ToList();
                ViewBag.orders = db.OrderDetails.ToList().Where(x => x.Order.CreatedBy == id).ToList();
            }         


            return View();
        }
    }
}