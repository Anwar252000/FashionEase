using PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PROJECT.Controllers
{
    public class AccountsController : Controller
    {

        FashionEaseEntities2 db = new FashionEaseEntities2();
      

        [HttpPost] //post for submit
        public ActionResult Login(String email, String Password)
        {


            var list = db.Users.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == Password && x.IsActive == true).FirstOrDefault();

            if (list != null)
            {

                //session use to store data value across request
                Session["id"] = list.Id;
                Session["name"] = list.Name;
                Session["roleid"] = list.RoleId;
                Session["email"] = list.Email;
                Session["role"] = list.Role == null ? "" : list.Role.Name;
                var roleid = list.RoleId;


                //FA enables user and password validation for Web applications
                FormsAuthentication.SetAuthCookie(list.Email, false);
                if (roleid == 1)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (roleid == 2)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return RedirectToAction("Index", "Products");
                }

            }
            // transfer data from view to controller, controller to view, or from one action method to another action method of the same or a different controller.
            TempData["LoginError"] = "Incorrect Details!";
          return  Redirect(Url.Action("Index", "Home") + "#signin");

        }



        [HttpPost] //post for submit
        public ActionResult Logout()
        {

            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();

            //makes new requests and URL in the browser's address bar is updated with the generated URL by MVC
            return RedirectToAction("Index", "Home");
        }





        }
}