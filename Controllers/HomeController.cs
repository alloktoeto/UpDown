using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpDown.Models;
using UpDown.Repository;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace UpDown.Controllers
{
    public class HomeController : BaseController
    {

        [ChildActionOnly]
        public ActionResult HeaderPartial()
        {
            List<product> products = new List<product>();
            if (Session["card"] != null)
            {
                products = Session["card"] as List<product>;
            }
            products = products.GroupBy(x => x.prodID).Select(y => y.FirstOrDefault()).ToList();
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

            ViewBag.Sum = sum;

            IHelperRepository _helperRepository = new HelperRepository();
            _helperRepository.SetSearchItems();

            var jsonSearch = _helperRepository.GetSearchItems();

            ViewBag.SearchItems = jsonSearch;

            return PartialView();
        }

        public ActionResult Search(string search)
        {
            var products = GetProducts(); // pm.GetProducts();
            ViewBag.Instruments = GetInstruments(); // im.GetCategories();
            ViewBag.Formats = GetFormats();// fm.GetCategories();
            
            var genres = GetGenres();// gm.GetCategories();
            ViewBag.Genres = genres;
            ViewBag.Effects = GetEffects();// em.GetCategories();

            products = products.Where(x => x.name.ToLower().Contains(search.ToLower()) 
                                        || x.formatsToProducts.Select(y=>y.format.name.ToLower()).Contains(search.ToLower())
                                        || x.instrumentsToProducts.Select(y=>y.instrument.name.ToLower()).Contains(search.ToLower())
                                        || x.subGenresToProducts.Select(y=>y.SubGenre.subGenreName.ToLower()).Contains(search.ToLower())
                                        //|| x.sound.name.ToLower().Contains(search.ToLower())
                                        //|| x.effect.name.ToLower().Contains(search.ToLower())
                                        ).ToList();
            return View(products);
        }

        [OutputCache(Duration = 600)]
        [ChildActionOnly]
        public ActionResult CarouselPartial()
        {
            var images = GetCarouselImages();
            return PartialView(images);
        }

        [OutputCache(Duration = 600)]
        [ChildActionOnly]
        public ActionResult FooterPartial()
        {
            ViewBag.Effects = GetEffects();// new EffectReposirory().GetCategories();
            ViewBag.Genres = GetGenres();// new GenresRepository().GetCategories();
            ViewBag.Formats = GetFormats();// new FormatsRepository().GetCategories();
            ViewBag.Instruments = GetInstruments();// new InstrumentsRepository().GetCategories();
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult PlayerPartial()
        {
            bool playerOpened = false;

            if (Session["playerOpened"] == null || (bool)Session["playerOpened"] == true)
            {
                playerOpened = true;
            }
            ViewBag.PlayerOpened = playerOpened;
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult MobileMenu()
        {
            ViewBag.Effects = GetEffects();
            ViewBag.Genres = GetGenres();
            ViewBag.Formats = GetFormats();
            ViewBag.Instruments = GetInstruments();
            return PartialView();
        }

       // [OutputCache(Location = OutputCacheLocation.Server, Duration = 60)]
        public ActionResult Index()
        {

            //string password = "123456";
            //string plaintext = "qwerty";
            //string encryptedstring = StringCipher.Encrypt(plaintext, password);
            //string decryptedstring = StringCipher.Decrypt(encryptedstring, password);
            ProductsRepository prodModel = new ProductsRepository();
            //PaymentRepository paymentRepository = new PaymentRepository();
            //string zzz = paymentRepository.DecryptString("XzX3iQQBNCBioJEN8vs8f43yYsywq/zQ9GKS3XIyG9DJ5VoF//wObSkGRxqwfNZ+BqgPtHdYCN8dzVAM35fAtFLIl3G0drs98cu96oYP1caKQ0OhMSI9FEkQi/oius/XkwtB6NLuXRiwpcd5jXgzKCFmBnwbU/b3pwSmc4LJBnc5Q1CGTjes3EnN7XMx6W5GRQ9HxJMacCtTAYVvpFFOF3JiR82tnDJNEhz/4IurC0kyFg+e1vjrxpQULsgW8XR+");
            //zzz = "product=cf8562c9-a351-41aa-bc7d-09277b6289e9&format=1&date=3/10/2016 12:40:00 AM&user=0f8a304e-ce20-4802-8851-3152a308831a";
            ////user u = new user();



            List<product> products = GetProducts();//prodModel.GetProducts();
            ViewBag.Effects = GetEffects(); //new EffectReposirory().GetCategories();
            ViewBag.Genres = GetGenres(); // new GenresRepository().GetCategories();
            ViewBag.Formats = GetFormats(); // new FormatsRepository().GetCategories();
            ViewBag.Instruments = GetInstruments(); //new InstrumentsRepository().GetCategories();
            ViewBag.Tags = GetTags(); //new TagsRepository().GetCategories();



            return View(products);
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Cooperation()
        {
            return View();
        }

        public ActionResult CooperationMsgSend(string name, string email, string instrument, string message)
        {
            IHelperRepository _helperRepository = new HelperRepository();
            string msg = name + "<br/>" + email + "<br/>" + instrument + "<br/>" + message;
            _helperRepository.SendMail(msg);
            return RedirectToAction("Cooperation");
        }

        public ActionResult GhostProduction()
        {
            return View();
        }

        public ActionResult Support()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Articles()
        {
            return View();
        }

        public ActionResult Article(int id)
        {
            return View();
        }

    }
}
