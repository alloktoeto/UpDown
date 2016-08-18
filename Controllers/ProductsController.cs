using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpDown.Models;
using UpDown.Repository;
using System.IO;

namespace UpDown.Controllers
{
    public class ProductsController : BaseController
    {
        private ProductsRepository pModel;

        public ProductsController()
        {
            pModel = new ProductsRepository();
        }

        public ActionResult Index(int? instrument, int? format, int? effect, int? genre)
        {
            //InstrumentsRepository im = new InstrumentsRepository();
            //GenresRepository gm = new GenresRepository();
            //FormatsRepository fm = new FormatsRepository();
            //EffectReposirory em = new EffectReposirory();

            ViewBag.Instruments = GetInstruments();// im.GetCategories();
            ViewBag.Formats = GetFormats();// fm.GetCategories();

            var genres = GetGenres(); // gm.GetCategories();
            ViewBag.Genres = genres;
            ViewBag.Effects = GetEffects();// em.GetCategories();

            ViewBag.Instrument = instrument;
            ViewBag.Format = format;
            ViewBag.Effect = effect;
            ViewBag.Genre = genre;

            List<product> products = new List<product>();

            if (Session["productsFilter"]!=null)
            {
                products = Session["productsFilter"] as List<product>;
            }

            else
            {
                products = GetProducts();// pModel.GetProducts();
                Session["productsFilter"] = products;
            }
            

            if (instrument!=null)
            {
                var instrumentsToProducts = pModel.GetInstrumentsToProducts();
                var prodInInstruments = instrumentsToProducts.Where(x => x.instrument.instrumentID == instrument).Select(c=>c.product.prodID).Distinct();
                products = products.Where(x => prodInInstruments.Contains(x.prodID)).ToList();
            }
            if (format!=null)
            {
                var formatsToProducts = pModel.GetFormatsToProducts();
                var prodInFormats = formatsToProducts.Where(x => x.format.formatID == format).Select(y => y.product.prodID).Distinct();
                products = products.Where(x=>prodInFormats.Contains(x.prodID)).ToList();
            }

            if (effect!=null)
            {
                products = products.Where(x => x.sound!=null && x.sound.soundID == effect).ToList();
            }
            if (genre!=null)
            {
                var genreSelected = genres.FirstOrDefault(x => x.genreID == genre);
                var subGenresSelected = genreSelected.SubGenres.Select(x=>x.subGenreId);
                ProductsRepository pm = new ProductsRepository();
                product pr;
                List<product> newListProducts = new List<product>();
                foreach (var p in products)
                {
                    pr = new product();
                    foreach (var sg in subGenresSelected)
                    {
                        if (p.subGenresToProducts.Select(x=>x.subGenreId).Contains(sg))
                        {
                            newListProducts.Add(p);
                        }
                    }
                }
                products = newListProducts.GroupBy(x=>x.prodID).Select(x=>x.First()).ToList();
            }
            return View(products);
        }

        public ActionResult More()
        {
            return View();
        }

        public ActionResult Filter(int? instrument, int? format, int? effect, int? genre)
        {
            return RedirectToAction("Index", "Products", new { instrument = instrument, format = format, effect = effect, genre = genre });
        }

        [HttpGet]
        public ActionResult Product(string id)
        {
            TagsRepository tg = new TagsRepository();
            ViewBag.Tags = tg.GetCategories();
            product p = new product();
            try
            {
                Guid idGuid = new Guid(id);
                p = pModel.GetProduct(idGuid);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errors", "Error");
                //return Error("Products/Product", ex.Message);
            }
            

            return View(p);
        }

        [HttpGet]
        public ActionResult DeleteFromCard(Guid id)
        {
            List<product> products = new List<product>();
            if (Session["card"] != null)
            {
                products = Session["card"] as List<product>;
            }
            products.RemoveAll(x=>x.prodID==id);
            Session["card"] = products;
            decimal? sum = 0;
            foreach (var item in products)
            {
                if (item.price_New > 0)
                {
                    sum += item.price_New;
                }
                else
                {
                    sum += item.price;
                }
            }
            ViewBag.SumResult = sum;
            return PartialView(products);
        }

        [HttpPost]
        public decimal? AddToCard(Guid id)
        {
            List<product> products = new List<product>();
            if (Session["card"] != null)
            {
                products = Session["card"] as List<product>;
            }
            var prod = pModel.GetProduct(id);
            prod.FormatsSelectedList = new List<int>();
            products.Add(prod);
            products = products.GroupBy(x => x.prodID).Select(y => y.FirstOrDefault()).ToList();
            Session["card"] = products;
            decimal? sum = 0;
            foreach (var item in products)
            {
                if (item.price_New>0)
                {
                    sum += item.price_New;
                }
                else
                {
                    sum += item.price;
                }
            }
            return sum;
        }

        [HttpPost]
        public ActionResult AddProductWithFormats(Guid prodID, List<int> formatsSelected)
        {
            List<product> products = new List<product>();
            if (Session["card"] != null)
            {
                products = Session["card"] as List<product>;
            }
            var prodNew = pModel.GetProduct(prodID);
            prodNew.FormatsSelectedList = formatsSelected;
            products.Add(prodNew);
            products = products.GroupBy(x => x.prodID).Select(y => y.FirstOrDefault()).ToList();
            Session["card"] = products;

            return RedirectToAction("Product", new { id = prodID });
        }

        public ActionResult Cart()
        {
            if (Session["card"]!=null)
            {
                List<product> products = Session["card"] as List<product>;
                return View(products);
            }
            return View();
        }

        public ActionResult RecommendedProducts()
        {
            ProductsRepository pm = new ProductsRepository();
            var products = GetProducts().Where(x => x.isRecommeded == true).Take(6).ToList(); //; pm.GetProducts().Where(x => x.isRecommeded == true).Take(6).ToList();
            
            return PartialView(products);
        }

        public ActionResult Download(string url)
        {
            if (Session["userLogin"] != null)
            {
                PaymentRepository paymentRepository = new PaymentRepository();
                string decryptedUrlString = paymentRepository.DecryptString(url);
                return Redirect("DownloadDecrypted?" + decryptedUrlString);
            }

            return RedirectToAction("Index", "Profile");

            //string zzz = paymentRepository.DecryptString("XzX3iQQBNCBioJEN8vs8f43yYsywq/zQ9GKS3XIyG9DJ5VoF//wObSkGRxqwfNZ+BqgPtHdYCN8dzVAM35fAtFLIl3G0drs98cu96oYP1caKQ0OhMSI9FEkQi/oius/XkwtB6NLuXRiwpcd5jXgzKCFmBnwbU/b3pwSmc4LJBnc5Q1CGTjes3EnN7XMx6W5GRQ9HxJMacCtTAYVvpFFOF3JiR82tnDJNEhz/4IurC0kyFg+e1vjrxpQULsgW8XR+");
            //zzz = "product=cf8562c9-a351-41aa-bc7d-09277b6289e9&format=1&date=3/10/2016 12:40:00 AM&user=0f8a304e-ce20-4802-8851-3152a308831a";
        }

        public ActionResult DownloadDecrypted()
        {
            try
            {
                string product = Request["product"].ToString();
                string user = Request["user"].ToString();
                string date = Request["date"].ToString();
                string format = Request["format"].ToString();
                int formatId;
                bool x = int.TryParse(format, out formatId);
                Guid productId = new Guid(product);
                Guid userId = new Guid(user);
                DateTime dt = Convert.ToDateTime(date);

                if (Session["userLogin"] != null)
                {
                    user uId = Session["userLogin"] as user;
                    if (uId.userID!=userId)
                    {
                        return RedirectToAction("Index", "Profile");
                    }
                }

                ProductsRepository pr = new ProductsRepository();
                FormatsRepository fr = new FormatsRepository();
                PaymentRepository pm = new PaymentRepository();
                var prod = pr.GetProduct(productId);
                var frmt = fr.GetCategory(formatId);
                var formatToProductFileName = fr.GetFormatsToProductByProduct(productId, formatId);
                //"product=" + product.Id + "&format=" + format + "&date=" + dl.date + "&user=" + user.userID);
                var download = pm.GetDownloads().FirstOrDefault(d => d.userID == userId && d.productId == productId && d.format == formatId);

                if (download.count<=0)
                {
                    return RedirectToAction("Index", "Profile");
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Files/" +
                                                                                product + "/" +
                                                                                format + "/" +
                                                                                //prod.name +
                                                                                //"_" +
                                                                                formatToProductFileName.file_name));

                string fileName = prod.name + ".rar";

                pm.ReduceCountDownload(download.downloadID);

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Profile");
            }
        }

        public ActionResult ProductPartial(product model)
        {
            return PartialView(model);
        }
    }
}
