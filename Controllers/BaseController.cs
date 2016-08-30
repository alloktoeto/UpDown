using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpDown.Models;
using UpDown.Repository;
using System.Web.Caching;

namespace UpDown.Controllers
{
    public class BaseController : Controller
    {
        protected AppCache appCache;
        public BaseController()
        {
            appCache = new AppCache();
        }

        protected List<product> GetProducts()
        {

            //List<product> products = System.Web.HttpRuntime.Cache["products"] as List<product>;
            List<product> products = appCache.GetProducts();

            if (products == null)
            {
                ProductsRepository prodModel = new ProductsRepository();
                products = prodModel.GetProducts();
                appCache.AddProducts(products);
            }

            return products;


            //if (products == null)
            //{
            //    ProductsRepository prodModel = new ProductsRepository();
            //    products = prodModel.GetProducts();
            //    System.Web.HttpRuntime.Cache["products"] = products;
            //}
            
            //if (System.Web.HttpContext.Current.Application["products"] != null)
            //{
            //    var products = System.Web.HttpContext.Current.Application["products"] as List<product>;
            //    return products;
            //}
            //else
            //{
            //    ProductsRepository prodModel = new ProductsRepository();
            //    var products = prodModel.GetProducts();
            //    System.Web.HttpContext.Current.Application["products"] = products;
            //    return products;
            //}
        }


        protected List<format> GetFormats()
        {
            List<format> formats = appCache.GetFormats();
            if (formats == null)
            {
                FormatsRepository fRepository = new FormatsRepository();
                formats = fRepository.GetCategories();
                appCache.AddFormats(formats);
            }
            return formats;
        }

        protected List<effect> GetEffects()
        {
            List<effect> effects = appCache.GetEffects();
            if (effects == null)
            {
                EffectReposirory effectsModel = new EffectReposirory();
                effects = effectsModel.GetCategories();
                appCache.AddEffects(effects);
            }
            return effects;
        }

        protected List<carusel_images> GetCarouselImages()
        {
            List<carusel_images> images = appCache.GetCarouselImages();
            if (images == null)
            {
                CarouselRepository carouselModel = new CarouselRepository();
                images = carouselModel.GetCategories();
                appCache.AddCarouselImages(images);
            }
            return images;
        }

        protected List<genre> GetGenres()
        {
            List<genre> genres = appCache.GetGenres();
            if (genres == null)
            {
                GenresRepository genreModel = new GenresRepository();
                genres = genreModel.GetCategories();
                appCache.AddGenres(genres);
            }
            return genres;
        }

        protected List<instrument> GetInstruments()
        {
            List<instrument> instruments = appCache.GetInstruments();
            if (instruments == null)
            {
                InstrumentsRepository instrumentModel = new InstrumentsRepository();
                instruments = instrumentModel.GetCategories();
                appCache.AddInstruments(instruments);
            }
            return instruments;
        }

        protected List<tag> GetTags()
        {
            List<tag> tags = appCache.GetTags();
            if (tags == null)
            {
                TagsRepository tagsModel = new TagsRepository();
                tags = tagsModel.GetCategories();
                appCache.AddTags(tags);
            }
            return tags;
        }


        //protected ActionResult Error(string action, string error)
        //{
        //    return RedirectToAction("Errors", "Error");
        //}
    }
}
