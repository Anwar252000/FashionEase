using HtmlAgilityPack;
using PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PROJECT.Controllers
{
    public class HomeController : Controller
    {
        public static string Scrap = "";
        FashionEaseEntities2 db = new FashionEaseEntities2();
        //    // GET: Home
        public ActionResult Index()
        {
            string mainurl = "https://www.junaidjamshed.com/womens/un-stitched.html";
            List<string> url = new List<string>();
            List<string> dataget = new List<string>();
            List<Product> products = new List<Product>();

            ViewBag.allProducts = db.Products.ToList();

            Task t = CreateTestSuite(mainurl);
            Task.WaitAll(new Task[] { t });
            if (Scrap != String.Empty)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(Scrap);
                HtmlNode htmlDoc = doc.DocumentNode;

                List<HtmlNode> list = new List<HtmlNode>();
                foreach (HtmlNode heading in htmlDoc.SelectNodes("//li[@class='item product product-item']"))
                {
                    list.Add(heading);
                }
                foreach (HtmlNode item in list)
                {
                    var value = item.InnerText;

                    value = value.Replace(" ", "").Replace("\n", " ");
                    dataget.Add(value);
                    var data = item.SelectSingleNode("//img[@class='product-image-photo lazy']");
                    url.Add(data.Attributes["data-original"].Value);
                }

                string newdata = "";
                foreach (var item in dataget)
                {
                    string[] productData = { };

                    string[] ch = item.Split(' ');
                    int productDataIndex = 0;
                    foreach (var item2 in ch)
                    {
                        if (item2 != string.Empty)
                        {
                            newdata += item2 + " ";
                            string neww = newdata.Replace("\n", "");
                            string[] splitdata = neww.Split(' ');

                            //productData[productData.Length - 1] = splitdata[0];
                            //productDataIndex++;
                        }
                    }

                    //Product product = new Product()
                    //{
                    //    Name = productData[0],
                    //    Price = Convert.ToDouble(productData[1]),
                    //};

                    //products.Add(product);

                }
                //ViewBag.allPrducts = products;
            }

            return View();
        }



        public ActionResult Index2(string searchItem, string term)
        {

            if (searchItem == null || searchItem == "")
            {
                ViewBag.allProducts = db.Products.ToList();
            }
            else
            {
                ViewBag.allProducts = db.Products.ToList().Where(x => x.Name == searchItem);

            }

            List<string> Products = db.Products.Where(s => s.Name.StartsWith(term))

               .Select(x => x.Name).ToList();

            return ActionResult(Products, JsonRequestBehavior.AllowGet);

        }

        private ActionResult ActionResult(List<string> products, JsonRequestBehavior allowGet)
        {
            return View();
        }

        public ActionResult SamplePage()
        {


            return View();
        }


        public ActionResult About()
        {


            return View();
        }
        public ActionResult ContactUs()
        {


            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(Submission s)
        {
            if (db.Submissions.Where(x => x.Email == s.Email).ToList().Count > 0)
            {
                TempData["EmailExists"] = "Email Address Already Exists*!";
            }
            if (s.Phone != 11)
            {

                TempData["PhoneError"] = "Contact Number should be of 11 digits!";

            }
            else {
                db.Submissions.Add(s);
                int id = db.SaveChanges();

                if (id.ToString() != "")
                {
                    TempData["alert"] = " Thank you for submitting the form. we will get back to you soon.";

                }
            }

            return RedirectToAction("ContactUs");
        }


        public ActionResult PrivacyPolicy()
        {

            return View();
        }

        public ActionResult FAQs()
        {

            return View();
        }


        public ActionResult Orders()
        {


            return View();
        }


        public ActionResult ReturnsandExchanges()
        {


            return View();
        }



        public ActionResult WishList()
        {

            return View();
        }

        [HttpPost]
        public ActionResult UpdateShippingAddress(string userEmail, string shippingAddress)
        {

            User u = db.Users.ToList().Where(x => x.Email == userEmail).FirstOrDefault();
            u.ShippingAddress = shippingAddress;
            db.SaveChanges();

            return RedirectToAction("CustomerCare");

            

        }

        public ActionResult CustomerCare()
        {
           return View();
            }

    private static async Task CreateTestSuite(string url)
    {
        try
        {
            string responseBody = "";

            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36");
                // string url = "https://www.junaidjamshed.com/mens/kameez-shalwar.html";

                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {

                    responseBody = await response.Content.ReadAsStringAsync();
                    Scrap = responseBody;

                }


            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
}

