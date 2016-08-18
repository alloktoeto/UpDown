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
        
        protected List<product> GetProducts()
        {

            List<product> products = null;// System.Web.HttpRuntime.Cache["products"] as List<product>;
            if (products == null)
            {
                ProductsRepository prodModel = new ProductsRepository();
                products = prodModel.GetProducts();
                System.Web.HttpRuntime.Cache["products"] = products;
            }
            return products;
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
            List<format> formats = System.Web.HttpRuntime.Cache["formats"] as List<format>;
            if (formats == null)
            {
                FormatsRepository fRepository = new FormatsRepository();
                formats = fRepository.GetCategories();
                System.Web.HttpRuntime.Cache["formats"] = formats;
            }
            return formats;
        }

        protected List<effect> GetEffects()
        {
            if (System.Web.HttpContext.Current.Application["effects"] != null)
            {
                var effects = System.Web.HttpContext.Current.Application["effects"] as List<effect>;
                return effects;
            }
            else
            {
                var effects = new EffectReposirory().GetCategories();
                System.Web.HttpContext.Current.Application["effects"] = effects;
                return effects;
            }
        }

        protected List<carusel_images> GetCarouselImages()
        {
            if (System.Web.HttpContext.Current.Application["carousel"] != null)
            {
                var caruselImages = System.Web.HttpContext.Current.Application["carousel"] as List<carusel_images>;
                return caruselImages;
            }
            else
            {
                var caruselImages = new CarouselRepository().GetCategories();
                System.Web.HttpContext.Current.Application["carousel"] = caruselImages;
                return caruselImages;
            }
        }

        protected List<genre> GetGenres()
        {
            if (System.Web.HttpContext.Current.Application["genres"] != null)
            {
                var genres = System.Web.HttpContext.Current.Application["genres"] as List<genre>;
                return genres;
            }
            else
            {
                var genres = new GenresRepository().GetCategories();
                System.Web.HttpContext.Current.Application["genres"] = genres;
                return genres;
            }
        }

        protected List<instrument> GetInstruments()
        {
            if (System.Web.HttpContext.Current.Application["instruments"] != null)
            {
                var instruments = System.Web.HttpContext.Current.Application["instruments"] as List<instrument>;
                return instruments;
            }
            else
            {
                var instruments = new InstrumentsRepository().GetCategories();
                System.Web.HttpContext.Current.Application["instruments"] = instruments;
                return instruments;
            }
        }


        protected List<tag> GetTags()
        {
            List<tag> tags = System.Web.HttpRuntime.Cache["tags"] as List<tag>;
            if (tags == null)
            {
                TagsRepository tRepository = new TagsRepository();
                tags = tRepository.GetCategories();
                System.Web.HttpRuntime.Cache["tags"] = tags;
            }
            return tags;
            //if (System.Web.HttpContext.Current.Application["tags"] != null)
            //{
            //    var tags = System.Web.HttpContext.Current.Application["tags"] as List<tag>;
            //    return tags;
            //}
            //else
            //{
            //    var tags = new TagsRepository().GetCategories();
            //    System.Web.HttpContext.Current.Application["tags"] = tags;
            //    return tags;
            //}
        }


        //protected ActionResult Error(string action, string error)
        //{
        //    return RedirectToAction("Errors", "Error");
        //}
    }
}
