using PROJECT.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using PROJECT.DTOs;

namespace PROJECT.Controllers
{
    public class ProductsController : Controller
    {
        public static string Scrap = "";
        FashionEaseEntities2 db = new FashionEaseEntities2();

        List<string> url = new List<string>();
        List<string> name = new List<string>();
        List<string> price = new List<string>();

        // GET: Products
        [Authorize(Roles = "VENDOR")]
        public ActionResult Index()
        {
            var id = Convert.ToInt32(Session["id"]);
            ViewBag.Products = db.Products.ToList().Where(x => x.IsActive == true && x.CreatedBy == id);
            ViewBag.ProductColors = db.Product_Color.ToList();
            return View();


        }


        public ActionResult Details(long id)
        {
            ViewBag.details = db.Products.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.reviews = db.Reviews.ToList();
            ViewBag.reviewsCounts = db.Reviews.Count();
            return View();
        }
        [HttpPost]

        public ActionResult AddReview(Review review)
        {
            try
            {
                review.ReviewDate = DateTime.Now; // Use DateTime.Now to get the current date and time
                db.Reviews.Add(review);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            // Use RedirectToAction with an anonymous object to pass the ProductId as a route value
            return RedirectToAction("Details", new { id = review.ProductId });
        }

        [Authorize(Roles = "VENDOR")]
        public ActionResult Create()
        {
            ViewBag.colors = db.Colors.ToList();
            return View();
        }

        [Authorize(Roles = "VENDOR")]
        [HttpPost]
        public ActionResult CreateProduct(Product p, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            if (file != null)
            {


                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path); p.Image = file.FileName;

                }
            }

            if (file1 != null)
            {


                if (file1.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file1.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file1.SaveAs(_path); p.Image2 = file1.FileName;

                }
            }

            if (file2 != null)
            {


                if (file2.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file2.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file2.SaveAs(_path); p.Image3 = file2.FileName;

                }
            }
            if (file3 != null)
            {


                if (file3.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file3.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file3.SaveAs(_path); p.Image4 = file3.FileName;

                }
            }

            p.CreatedOn = DateTime.Now;
            p.CreatedBy = Convert.ToInt32(Session["id"]);
            p.IsActive = true;
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");


        }


        [Authorize(Roles = "VENDOR")]
        public ActionResult Edit(int id)
        {
            ViewBag.user = db.Products.Where(x => x.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]

        [Authorize(Roles = "VENDOR")]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            var data = db.Products.Where(x => x.Id == product.Id).FirstOrDefault();
            if (file != null)
            {


                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path); data.Image = file.FileName;


                }
            }


            if (data != null)
            {
                data.Name = product.Name;

                data.Description = product.Description;

                data.Price = product.Price;

                data.Quantity = product.Quantity;



                db.SaveChanges();
            }
            return RedirectToAction("Index", "Products");
        }

        [Authorize(Roles = "VENDOR")]
        [HttpPost]
        public ActionResult Delete(int Id)
        {

            var product = db.Products.Where(x => x.Id == Id).FirstOrDefault();

            product.IsActive = false;

            db.SaveChanges();
            return RedirectToAction("Index");
        }




        //ajility pack used for scrapping
        public ActionResult J()
        {
            string mainurl = "https://www.junaidjamshed.com/womens/un-stitched.html";
            Task t = CreateTestSuite(mainurl);
            Task.WaitAll(new Task[] { t
    }
            );

            List<CollectionData> Div = new List<CollectionData>();
            if (Scrap != String.Empty)
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Scrap);
                HtmlNode htmlDoc = doc.DocumentNode;

                var list = new List<string>(doc.DocumentNode.SelectNodes("//li[@class='item product product-item']").Select(li => li.InnerHtml));




                for (int i = 0; i < list.Count; i++)
                {
                    var doc2 = new HtmlAgilityPack.HtmlDocument();      
                    doc2.LoadHtml(list[i]);
                    var Name = doc2.DocumentNode.SelectSingleNode(".//h2").InnerText.Trim();
                    var pid = Name.Split('|')[1].Trim();
                    var link = doc2.DocumentNode.SelectSingleNode("//img[@class='product-image-photo lazy']").Attributes["data-original"].Value;
                    var Price = doc2.DocumentNode.SelectSingleNode(".//span[@class='price']").InnerText;

                    Div.Add(new CollectionData2 { pid = pid, LinkImage = link, Price = Price, Name = Name });

                }

                ViewBag.DateOfDiv = Div.ToList();
            }

            return View();
        }

        public ActionResult BeechTree()
        {
            string mainurl = "https://www.limelight.pk/collections/unstitched-embroidered-winter";
            Task t = CreateTestSuite(mainurl);
            Task.WaitAll(new Task[] { t
    });

            List<CollectionData> Div = new List<CollectionData>();

            if (Scrap != String.Empty)
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Scrap);
                HtmlNode htmlDoc = doc.DocumentNode;

                var list = new List<string>(doc.DocumentNode.SelectNodes("//li[@class='grid__item']").Select(li => li.InnerHtml));

                for (int i = 0; i < list.Count; i++)
                {
                    var doc2 = new HtmlAgilityPack.HtmlDocument();
                    doc2.LoadHtml(list[i]);
                    var pid = i + 1;
                    var link = doc2.DocumentNode.SelectSingleNode("//img").Attributes["src"].Value;
                    var Price = doc2.DocumentNode.SelectSingleNode(".//span[@class='price-item price-item--sale']").InnerText.Trim();
                    var Name = doc2.DocumentNode.SelectSingleNode(".//span[@class='card-information__text h5']").InnerText.Trim();

                    Div.Add(new CollectionData { pid = pid, LinkImage = link, Price = Price, Name = Name });

                }

            }
            ViewBag.DateOfDiv = null;
            ViewBag.DateOfDiv = Div.ToList();


            return View();
        }

        public ActionResult Orient()
        {
            string mainurl = "https://www.shopatorient.com/collections/unstitched-shirt-dupatta-pants";
            Task t = CreateTestSuite(mainurl);
            Task.WaitAll(new Task[] { t
    });

            List<CollectionData> Div = new List<CollectionData>();

            if (Scrap != String.Empty)
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Scrap);
                HtmlNode htmlDoc = doc.DocumentNode;

                var list = new List<string>(doc.DocumentNode.SelectNodes("//li[@class='grid__item']").Select(li => li.InnerHtml));


                for (int i = 0; i < list.Count; i++)
                {
                    var doc2 = new HtmlAgilityPack.HtmlDocument();
                    doc2.LoadHtml(list[i]);
                    var pid = i + 1;
                    var link = doc2.DocumentNode.SelectSingleNode("//img").Attributes["src"].Value;
                    var Price = doc2.DocumentNode.SelectSingleNode(".//span[@class='price-item price-item--sale']").InnerText.Trim();
                    var Name = doc2.DocumentNode.SelectSingleNode(".//span[@class='card-information__text h5']").InnerText.Trim();

                    Div.Add(new CollectionData { pid = pid, LinkImage = link, Price = Price, Name = Name });

                }

            }
            ViewBag.DateOfDiv = null;
            ViewBag.DateOfDiv = Div.ToList();
            return View();
        }


        public ActionResult Sapphire()
        {
    //        string mainurl = "https://pk.sapphireonline.pk/collections/unstitched";
    //        Task t = CreateTestSuite(mainurl);
    //        Task.WaitAll(new Task[] { t
    //});

    //        List<CollectionData> Div = new List<CollectionData>();

    //        if (Scrap != String.Empty)
    //        {
    //            var doc = new HtmlAgilityPack.HtmlDocument();
    //            doc.LoadHtml(Scrap);
    //            HtmlNode htmlDoc = doc.DocumentNode;

    //            var list = new List<string>(doc.DocumentNode.SelectNodes("//div[@class='t4s-product-wrapper']").Select(li => li.InnerHtml));


    //            for (int i = 0; i < list.Count; i++)
    //            {
    //                var doc2 = new HtmlAgilityPack.HtmlDocument();
    //                doc2.LoadHtml(list[i]);
    //                var pid = i + 1;
    //                var link = doc2.DocumentNode.SelectSingleNode("//img").Attributes["src"].Value;
    //                var Price = doc2.DocumentNode.SelectSingleNode(".//span[@class='money bacurr-money']").InnerText.Trim();
    //                var Name = doc2.DocumentNode.SelectSingleNode(".//h3[@class='t4s-product-title']").InnerText.Trim();

    //                Div.Add(new CollectionData { pid = pid, LinkImage = link, Price = Price, Name = Name });

    //            }

    //        }
    //        ViewBag.DateOfDiv = null;
    //        ViewBag.DateOfDiv = Div.ToList();

            return View();
        }


        public ActionResult Alkaram()
        {
            string mainurl = "https://www.alkaramstudio.com/collections/unstitched";
            Task t = CreateTestSuite(mainurl);
            Task.WaitAll(new Task[] { t
    });

            List<CollectionData> Div = new List<CollectionData>();

            if (Scrap != String.Empty)
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Scrap);
                HtmlNode htmlDoc = doc.DocumentNode;

                var list = new List<string>(doc.DocumentNode.SelectNodes("//div[@class='t4s-product-wrapper']").Select(li => li.InnerHtml));


                for (int i = 0; i < list.Count; i++)
                {
                    var doc2 = new HtmlAgilityPack.HtmlDocument();
                    doc2.LoadHtml(list[i]);
                    var pid = i + 1;
                    var link = doc2.DocumentNode.SelectSingleNode(".//img[@class='t4s-product-main-img']").Attributes["src"].Value;
                    var Price = doc2.DocumentNode.SelectSingleNode(".//div[@class='t4s-product-price']").InnerText.Trim();
                    var Name = doc2.DocumentNode.SelectSingleNode(".//h3[@class='t4s-product-title']").InnerText.Trim();

                    Div.Add(new CollectionData { pid = pid, LinkImage = link, Price = Price, Name = Name });

                }

            }
            ViewBag.DateOfDiv = null;
            ViewBag.DateOfDiv = Div.ToList();

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