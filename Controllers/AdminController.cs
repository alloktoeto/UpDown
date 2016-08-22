using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpDown.Models;
using System.ComponentModel;
using System.IO;
using UpDown.Repository;

namespace UpDown.Controllers
{
    public class AdminController : BaseController
    {
        #region DashBoard
        public ActionResult Dashboard()
        {
            return View();
        }
        #endregion

        #region Products
        public ActionResult Products()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            ProductsRepository pm = new ProductsRepository();
            List<product> p = pm.GetProductsAdmin();
            return View(p);
        }

        public ActionResult Product(Guid id)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            GenresRepository gm = new GenresRepository();
            FormatsRepository fm = new FormatsRepository();
            EffectReposirory em = new EffectReposirory();
            InstrumentsRepository im = new InstrumentsRepository();
            AuthorRepository am = new AuthorRepository();

            var genres = gm.GetCategories();
            var formats = fm.GetCategories();
            var effects = em.GetCategories();
            var instruments = im.GetCategories();
            var authors = am.GetAuthors();

            List<SelectListItem> authorsSelectList = new List<SelectListItem>();
            authorsSelectList = authors.Select(x => new SelectListItem
            {
                Value = x.authorID.ToString(),
                Text = x.name
            }).ToList();

            List<SelectListItem> instrumentsSelectList = new List<SelectListItem>();
            instrumentsSelectList = instruments.Select(x => new SelectListItem
            {
                Value = x.instrumentID.ToString(),
                Text = x.name
            }).ToList();

            ViewBag.AuthorsSelectList = authorsSelectList;
            ViewBag.InstrumentsSelectList = instrumentsSelectList;
            ViewBag.Genres = genres;
            ViewBag.Formats = formats;
            ViewBag.Effects = effects;
            ViewBag.Instruments = instruments;

            ProductsRepository pm = new ProductsRepository();
            product p = new product();
            p = pm.GetProduct(id);

            var genreId = p.subGenresToProducts.Select(x => x.SubGenre.subGenreGenreId).FirstOrDefault();

            var genreSelected = genres.FirstOrDefault(x => x.genreID == genreId);

            ViewBag.Genre = genreSelected;

            ViewBag.Formats = fm.GetCategories();
            return View(p);
        }

        [HttpPost]
        public ActionResult Product(product model, List<string> subgenres, List<string> instruments, HttpPostedFileBase image)
        {
            ProductsRepository productDataModel = new ProductsRepository();
            productDataModel.UpdateProduct(model, instruments, image.FileName);
            return RedirectToAction("UpdateFormats", new { productId = model.prodID });
        }

        public ActionResult DeleteProduct(Guid id)
        {
            ProductsRepository pd = new ProductsRepository();
            pd.RemoveProduct(id);
            System.Web.HttpRuntime.Cache["products"] = null;
            return RedirectToAction("Products", "Admin");
        }


        public ActionResult Add()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            GenresRepository gm = new GenresRepository();
            FormatsRepository fm = new FormatsRepository();
            EffectReposirory em = new EffectReposirory();
            InstrumentsRepository im = new InstrumentsRepository();
            AuthorRepository am = new AuthorRepository();

            var genres = gm.GetCategories();
            var formats = fm.GetCategories();
            var effects = em.GetCategories();
            var instruments = im.GetCategories();
            var authors = am.GetAuthors();

            List<SelectListItem> authorsSelectList = new List<SelectListItem>();
            authorsSelectList = authors.Select(x => new SelectListItem
            {
                Value = x.authorID.ToString(),
                Text = x.name
            }).ToList();

            List<SelectListItem> instrumentsSelectList = new List<SelectListItem>();
            instrumentsSelectList = instruments.Select(x => new SelectListItem
            {
                Value = x.instrumentID.ToString(),
                Text = x.name
            }).ToList();

            ViewBag.AuthorsSelectList = authorsSelectList;
            ViewBag.InstrumentsSelectList = instrumentsSelectList;
            ViewBag.Genres = genres;
            ViewBag.Formats = formats;
            ViewBag.Effects = effects;
            ViewBag.Instruments = instruments;
            
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(product model, List<string> instruments, HttpPostedFileBase file, HttpPostedFileBase demo)//, HttpPostedFileBase image, HttpPostedFileBase demo)
        {
            ProductsRepository productDataModel = new ProductsRepository();
            model.isDisplayed = true;
            model.url = demo!=null ? demo.FileName: "";
            product product = productDataModel.AddNewProduct(model, instruments, file.FileName);
            
            var path = Path.Combine(Server.MapPath("~/images/Products"), file.FileName);
            file.SaveAs(path);

            var pathDemo = Path.Combine(Server.MapPath("~/Files/demo"), demo.FileName);
            demo.SaveAs(pathDemo);

            return RedirectToAction("ProductGenres", new { productId = product.prodID });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(product model, List<string> instruments, HttpPostedFileBase file, HttpPostedFileBase demo)
        {
            ProductsRepository productDataModel = new ProductsRepository();
            string image = string.Empty;
            if (file!=null)
            {
                image = file.FileName;
                var path = Path.Combine(Server.MapPath("~/images/Products"), file.FileName);
                file.SaveAs(path);
            }
            if (demo!=null)
            {
                model.url = demo != null ? demo.FileName : "";
                var pathDemo = Path.Combine(Server.MapPath("~/Files/demo"), demo.FileName);
                demo.SaveAs(pathDemo);
            }

            productDataModel.UpdateProduct(model, instruments, image);
            ProductsNull();
            return RedirectToAction("ProductGenresEdit", new { productId = model.prodID });
            //return RedirectToAction("AddFormats", new { productId = model.prodID });
        }

        [HttpGet]
        public ActionResult AddFormats(Guid productId)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            FormatsRepository fm = new FormatsRepository();
            var formats = fm.GetCategories();
            var formatsToProduct = fm.GetFormatsByProductId(productId);
            formats.RemoveAll(x => formatsToProduct.Contains(x.formatID));
            ViewBag.Formats = formats;
            ViewBag.ProductId = productId;
            return View();
        }

        [HttpGet]
        public ActionResult EditFormats(Guid productId)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            System.Web.HttpContext.Current.Application["products"] = null;
            FormatsRepository fm = new FormatsRepository();
            var formats = fm.GetCategories();
            var product = GetProducts().FirstOrDefault(x => x.prodID == productId);
            //var formatsToProduct = fm.GetFormatsByProductId(productId);
            //formats.RemoveAll(x => formatsToProduct.Contains(x.formatID));
            ViewBag.Formats = formats;
            ViewBag.ProductId = productId;
            return View(product);
        }

        public ActionResult ProductsNull()
        {
            if (System.Web.HttpRuntime.Cache["products"]!=null)
            {
                System.Web.HttpRuntime.Cache.Remove("products"); //["products"] = null;
            }
            if (System.Web.HttpContext.Current.Application["products"] != null)
            {
                System.Web.HttpContext.Current.Application["products"] = null;    
            }
            return RedirectToAction("Products", "Admin");
        }

        [HttpGet]
        public ActionResult UpdateFormats(Guid productId)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            FormatsRepository fm = new FormatsRepository();
            var formats = fm.GetCategories();
            var formatsToProduct = fm.GetFormatsByProductId(productId);
            //formats.RemoveAll(x => formatsToProduct.Contains(x.formatID));
            ViewBag.Formats = formats;
            ViewBag.FormatsToProducts = formatsToProduct;
            ViewBag.ProductId = productId;
            System.Web.HttpContext.Current.Application["products"] = null;
            ProductsRepository pd = new ProductsRepository();

            product p = pd.GetProduct(productId);
            return View(p);
        }

        public ActionResult ProductGenres(Guid productId)
        {
            ViewBag.ProductId = productId;
            TempData["productId"] = productId;
            var genres = GetGenres();
            return View(genres);
        }

        [HttpPost]
        public ActionResult ProductGenres(FormCollection formvalue)
        {
            string productId = TempData["productId"].ToString();
            List<string> subgenres = new List<string>();
            ProductsRepository productDataModel = new ProductsRepository();
            string values = formvalue["subgenres"];

            subgenres = values.Split(',').ToList();
            if (subgenres.Count>0)
            {
                productDataModel.AddSubGenres(subgenres, productId);
                return RedirectToAction("AddFormats", new { productId = productId });
            }

            return RedirectToAction("ProductGenres", new { productId = productId });
                        
        }

        public ActionResult ProductGenresEdit(Guid productId)
        {
            ViewBag.ProductId = productId;

            TempData["productId"] = productId;
            var product = GetProducts().FirstOrDefault(x => x.prodID == productId);
            var genres = GetGenres();
            ProductGenresUpdateModel productGenresUpdateModel = new ProductGenresUpdateModel 
            {
                Product = product,
                Genres = genres
            };
            return View(productGenresUpdateModel);
        }


        [HttpPost]
        public ActionResult ProductGenresEdit(FormCollection formvalue)
        {
            string productId = TempData["productId"].ToString();
            List<string> subgenres = new List<string>();
            ProductsRepository productDataModel = new ProductsRepository();
            string values = formvalue["subgenres"];
            subgenres = values.Split(',').ToList();
            if (subgenres.Count > 0)
            {
                productDataModel.UpdateSubGenres(subgenres, productId);
                return RedirectToAction("EditFormats", new { productId = productId });
            }
            return RedirectToAction("ProductGenresEdit", new { productId = productId });
        }

        public ActionResult DeleteFormatFromProduct(Guid productId, int formatId)
        {
            return RedirectToAction("UpdateFormats", new { productId = productId });
        }

        //[HttpPost]
        //public ActionResult AddFormats(List<int> formatsSelectedList)
        //{
        //    FormatDataModel fm = new FormatDataModel();
        //    var formats = fm.GetCategories();
        //    ViewBag.Formats = formats;
        //    return View();
        //}

        public ActionResult DeleteFileFromProduct(int formatId, Guid productId)
        {
            if (Directory.Exists(Server.MapPath("~/Files"+"/" + productId + "/" + formatId)))
            {
                Directory.Delete(Server.MapPath("~/Files" + "/" + productId + "/" + formatId), true);    
            }
            FormatsRepository fm = new FormatsRepository();
            fm.DeleteFormatFromProduct(productId, formatId);
            return RedirectToAction("EditFormats", new { productId = productId });
        }

        public ActionResult UploadFile(HttpPostedFileBase file, int formatId, Guid productId, bool? isEdit=null)
        {
            var fileName = Path.GetFileName(file.FileName);
            if (!Directory.Exists(Server.MapPath("~/Files"+"/" + productId)))
            {
                Directory.CreateDirectory((Server.MapPath("~/Files"+"/" + productId)));
            }

            if (!Directory.Exists(Server.MapPath("~/Files"+"/" + productId + "/" + formatId)))
            {
                Directory.CreateDirectory(Server.MapPath("~/Files"+"/" + productId + "/" + formatId));
            }

            var path = Path.Combine(Server.MapPath("~/Files"+"/" + productId + "/" + formatId), fileName);
            file.SaveAs(path);
            FormatsRepository fm = new FormatsRepository();
            fm.AddFormatByProduct(productId, formatId, fileName);
            System.Web.HttpContext.Current.Application["products"] = null;
            if (isEdit!=null && isEdit==true)
            {
                return RedirectToAction("EditFormats", new { productId = productId });
            }
            return RedirectToAction("AddFormats", new { productId = productId });
        }

        public ActionResult AddDemoProduct(HttpPostedFileBase demo, Guid productId)
        {
            var fileName = Path.GetFileName(demo.FileName);
            if (!Directory.Exists(Server.MapPath("~/Files/demo")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Files/demo"));
            }
            var path = Path.Combine(Server.MapPath("~/Files/demo"), fileName);
            demo.SaveAs(path);
            ProductsRepository productDataModel = new ProductsRepository();
            productDataModel.UpdateProductDemo(productId, demo.FileName);
            //FormatDataModel fm = new FormatDataModel();
            //fm.AddFormatByProduct(productId, formatId);
            System.Web.HttpContext.Current.Application["products"] = null;
            return RedirectToAction("AddFormats", new { productId = productId });
        }


        

        [HttpPost]
        public ActionResult GetSubGenres(int? genreId = null)
        {
            var genres = GetGenres();
            if (genreId!=null)
            {
                genres = genres.Where(x => x.genreID == genreId).ToList();
            }
            return PartialView(genres);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetSubGenres()
        {
            var genres = GetGenres();
            
            return PartialView(genres);
        }
        #endregion

        #region Effects
        public ActionResult AddEffect()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddEffect(effect effect)
        {
            EffectReposirory em = new EffectReposirory();
            em.AddCategory(effect);
            System.Web.HttpContext.Current.Application["effects"] = null;
            return RedirectToAction("Effects");
        }

        public ActionResult Effects()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            EffectReposirory fm = new EffectReposirory();
            List<effect> effects = fm.GetCategories();
            return View(effects);
        }
        #endregion

        #region Instruments
        public ActionResult Instruments()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            InstrumentsRepository fm = new InstrumentsRepository();
            List<instrument> instruments = fm.GetCategories();
            return View(instruments);
        }

        public ActionResult AddInstrument()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddInstrument(instrument instrument)
        {
            InstrumentsRepository im = new InstrumentsRepository();
            im.AddCategory(instrument);
            System.Web.HttpContext.Current.Application["instruments"] = null;
            return View();
        }
        #endregion

        #region Genres
        public ActionResult Genres()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            GenresRepository fm = new GenresRepository();
            List<genre> genres = fm.GetCategories();
            return View(genres);
        }

        public ActionResult AddGenre()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddGenre(genre genre)
        {
            GenresRepository gm = new GenresRepository();
            gm.AddCategory(genre);
            
            System.Web.HttpContext.Current.Application["genres"] = null;

            return RedirectToAction("Genres");
        }

        public ActionResult EditGenre(int genreId)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            GenresRepository gr = new GenresRepository();

            var genre = gr.GetCategory(genreId);

            return View(genre);
        }

        [HttpPost]
        public ActionResult EditGenre(genre genre)
        {
            GenresRepository gm = new GenresRepository();
            gm.UpdateCategory(genre);
            System.Web.HttpContext.Current.Application["genres"] = null;
            return RedirectToAction("Genres");
        }

        public ActionResult SubGenres(int genreId)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }

            GenresRepository gr = new GenresRepository();

            var genre = gr.GetCategory(genreId);

            return View(genre);
        }

        public ActionResult AddSubGenre(int genreId)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }

            SubGenre subGenre = new SubGenre 
            { 
                subGenreGenreId=genreId
            };
            return View(subGenre);
        }

        [HttpPost]
        public ActionResult AddSubGenre(SubGenre subGenre)
        {
            GenresRepository gr = new GenresRepository();
            gr.AddSubGenre(subGenre);
            System.Web.HttpContext.Current.Application["genres"] = null;
            return RedirectToAction("SubGenres", new { genreId = subGenre.subGenreGenreId });
        }

        public ActionResult SubGenreEdit(int subId)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            GenresRepository gr = new GenresRepository();
            var subGenre = gr.GetSubGenre(subId);

            return View(subGenre);
        }

        [HttpPost]
        public ActionResult SubGenreEdit(SubGenre subGenre)
        {
            GenresRepository gr = new GenresRepository();
            gr.UpdateSubGenre(subGenre);
            System.Web.HttpContext.Current.Application["genres"] = null;
            return RedirectToAction("SubGenres", new { genreId = subGenre.subGenreGenreId });
        }


        public ActionResult DeleteGenre(int genreId) 
        {
            GenresRepository gr = new GenresRepository();
            gr.DeleteGenre(genreId);
            System.Web.HttpContext.Current.Application["genres"] = null;
            return RedirectToAction("Genres");
        }

        public ActionResult DeleteSubGenre(int subId)
        {
            GenresRepository gr = new GenresRepository();
            gr.DeleteSubGenre(subId);
            System.Web.HttpContext.Current.Application["genres"] = null;
            return RedirectToAction("Genres");
        }

        #endregion

        #region Formats
        public ActionResult Formats()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            FormatsRepository fm = new FormatsRepository();
            List<format> formats = fm.GetCategories();
            return View(formats);
        }

        public ActionResult AddFormat()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddFormat(format format)
        {
            FormatsRepository fm = new FormatsRepository();
            fm.AddCategory(format);
            System.Web.HttpContext.Current.Application["formats"] = null;
            return RedirectToAction("Formats");
        }
        #endregion

        #region Users
        public ActionResult Users()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            UsersRepository ud = new UsersRepository();
            var users = ud.GetUsers();
            return View();
        }

        public ActionResult Users1()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            UsersRepository ud = new UsersRepository();
            var users = ud.GetUsers();
            return View(users);
        }

        public ActionResult User()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult User(user user)
        {
            return View();
        }
        #endregion

        #region Tags
        public ActionResult Tags()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            TagsRepository td = new TagsRepository();
            List<tag> tags = td.GetCategories();
            return View(tags);
        }

        public ActionResult AddTag()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddTag(tag tag)
        {
            TagsRepository td = new TagsRepository();
            td.AddCategory(tag);
            System.Web.HttpContext.Current.Application["tags"] = null;
            return RedirectToAction("Tags");
        }

        public ActionResult UpdateTag(int id)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            TagsRepository tm = new TagsRepository();
            var tag = tm.GetCategory(id);
            return View(tag);
        }

        [HttpPost]
        public ActionResult UpdateTag(tag tag)
        {
            TagsRepository tm = new TagsRepository();
            tm.UpdateCategory(tag);
            System.Web.HttpContext.Current.Application["tags"] = null;
            return RedirectToAction("Tags");
        }

        public ActionResult DeleteTag(int id)
        {
            TagsRepository tm = new TagsRepository();
            var tag = tm.GetCategory(id);
            tm.Delete(tag);
            System.Web.HttpContext.Current.Application["tags"] = null;
            return RedirectToAction("Tags");
        }
        #endregion

        #region Statistict
        public ActionResult Statistics()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            return View();
        }
        #endregion

        #region Settingd
        public ActionResult Settings()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            return View();
        }
        #endregion

        #region CarouselImages
        public ActionResult CarouselImages()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            CarouselRepository cr = new CarouselRepository();
            List<carusel_images> images = cr.GetCategories();
            return View(images);
        }

        public ActionResult AddCarouselImage()
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddCarouselImage(carusel_images model, HttpPostedFileBase image)
        {
            CarouselRepository cr = new CarouselRepository();
            cr.AddCategory(model, image.FileName);
            var path = Path.Combine(Server.MapPath("~/images/slider"), image.FileName);
            image.SaveAs(path);
            System.Web.HttpContext.Current.Application["carousel"] = null;
            return RedirectToAction("CarouselImages");
        }

        public ActionResult UpdateCarouselImage(int id)
        {
            if (!CheckIfLogined())
            {
                return RedirectToAction("LoginAdmin", "Account");
            }
            CarouselRepository cr = new CarouselRepository();
            var tag = cr.GetCategory(id);
            return View(tag);
        }

        [HttpPost]
        public ActionResult UpdateCarouselImage(carusel_images model, HttpPostedFileBase image, string oldImage)
        {
            CarouselRepository cr = new CarouselRepository();
            string imageStr = "";

            if (image!=null)
            {
                imageStr = image.FileName;
                var path = Path.Combine(Server.MapPath("~/images/slider"), image.FileName);
                image.SaveAs(path);
            }
            else
            {
                imageStr = oldImage;
            }

            cr.UpdateCategory(model, imageStr);

            System.Web.HttpContext.Current.Application["carousel"] = null;
            return RedirectToAction("CarouselImages");
        }

        public ActionResult DeleteCarouselImage(int id)
        {
            CarouselRepository cr = new CarouselRepository();
            var image = cr.GetCategory(id);
            cr.Delete(image);
            System.Web.HttpContext.Current.Application["carousel"] = null;
            return RedirectToAction("CarouselImages");
        }
        
        #endregion

        private bool CheckIfLogined()
        {
            if (Session["AdminLoginUser"]!=null)
            {
                return true;
            }
            return false;
        }
    }
}
