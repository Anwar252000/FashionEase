using PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECT.Controllers
{
    public class CheckoutController : Controller
    {

        FashionEaseEntities2 db = new FashionEaseEntities2();

        public class ProductViewModel
        {
            public int Id { get; set; }
            public decimal PPrice { get; set; }
            public string Pname { get; set; }
            public string Img { get; set; }
        }
        [HttpPost]
        // public ActionResult CartAdd(long? Id, long? quantity, long? ColorId, string Size,string myModel)
        public ActionResult CartAdd(int? Id, decimal? Price, int Quantity, string Size, bool IsExternal, ProductViewModel myArray)

        {
            int productId = myArray.Id;
            string productName = myArray.Pname;
            string productImage = myArray.Img;
            // long Quantity = quantity.Value;

            Cart cart = new Cart();

            if (IsExternal)
            {
                cart.ProductId = (long) Id;
                cart.Name = myArray.Pname;
                cart.Price = Convert.ToDouble(myArray.PPrice);
                cart.Quantity = Quantity;
                cart.Image = myArray.Img;

                var total = cart.Price.Value * cart.Quantity;
                cart.bill = cart.Price == null ? 0 : float.Parse(total.ToString());
                cart.Size = Size;
            }
            else
            {

                var item = db.Products.Where(x => x.Id == Id).FirstOrDefault();

                cart.ProductId = item.Id;
                cart.Name = item.Name;
                cart.Price = item.Price;
                cart.Quantity = Quantity;
                cart.Image = item.Image;

                var total = cart.Price.Value * cart.Quantity;
                cart.bill = cart.Price == null ? 0 : float.Parse(total.ToString());
                cart.Size = Size;
            }

            if (Session["Cart"] == null)
            {
                List<Cart> li = new List<Cart>();

                li.Add(cart);
                Session["Cart"] = li;


                //ViewBag is a way to pass data from the controller to the view.
                ViewBag.Cart = li.Count();


            }
            else
            {
                List<Cart> li2 = Session["Cart"] as List<Cart>;

                //flag--makes sure condition is met
                int flag = 0;

                foreach (var item in li2)
                {
                    if (item.ProductId == cart.ProductId && item.Size == cart.Size)
                    {
                        item.Quantity += cart.Quantity;
                        item.bill += cart.bill;
                        flag = 1;
                    }
                }

                if (flag == 0)
                {
                    li2.Add(cart);
                }


                Session["Cart"] = li2;
                ViewBag.Cart = li2;



            }
            long count = 0;
            foreach (var item in Session["Cart"] as List<Cart>)
            {
                count = count + item.Quantity;
                Session["Count"] = count;

            }

            Session["Count"] = count;

            return RedirectToAction("Cart");
        }

        public ActionResult home()
        {
            return View();
        }

        public ActionResult Cart()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RemoveFromCart(int ProductId)
        {
            List<Cart> cart = (List<Cart>)Session["Cart"];
            foreach (var item in cart)
            {
                if (item.ProductId == ProductId)
                {
                    cart.Remove(item);
                    break;
                }
            }

            long count = 0;
            foreach (var item in Session["Cart"] as List<Cart>)
            {
                count = count + item.Quantity;


            }
            Session["Cart"] = cart;
            Session["Count"] = count;
            return RedirectToAction("Cart", "Checkout");
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
            public ActionResult CheckoutOrder(Order obj)
        {
            try
            {
                if (obj == null)
                {
                    TempData["MesasgeCartEmpty"] = "Kindly fill the form and continue checkout!";
                    return View("Index");
                }
                if (Session["Cart"] == null)
                {
                    TempData["MesasgeCartEmpty"] = "Kindly Add to Cart for Checkout!";
                    return View("Index");
                }
                else
                {

                    foreach (var item in Session["Cart"] as List<Cart>)
                    {
                        using (FashionEaseEntities2 db = new FashionEaseEntities2())
                        {

                            Order ord = new Order();

                            ord.Amount = obj.Amount;
                            ord.Name = obj.Name;
                            ord.Email = obj.Email;
                         
                            ord.Phone = obj.Phone;
                        
                            ord.ShipppingAddress = obj.ShipppingAddress;
                            ord.BillingAddress = obj.BillingAddress;
                            if(Session["id"]!=null)
                                    ord.CreatedBy = Convert.ToInt64(Session["id"]);
                            ord.CreatedOn = DateTime.Now;
                            ord.OrderStatus = "Pending";
                            db.Orders.Add(ord);
                            db.SaveChanges();


                            OrderDetail od = new OrderDetail();
                            od.OrderId = ord.Id;
                            od.ProductId = item.ProductId;
                            od.Quantity = item.Quantity;
                           

                            od.CreatedBy = Convert.ToInt32(Session["id"]);
                            od.CreatedOn = DateTime.Now;
                            db.OrderDetails.Add(od);

                            db.SaveChanges();
                            //try
                            return RedirectToAction("Index", "Home");

                        }
                    }
                }

            }
            catch
            {
                Console.WriteLine("");
            }

            return View();

        }
    }

}