using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace UpDown.Models
{
    public class AppCache
    {
        //-----------------------------PRODUCTS----------------------------------------------------
        public List<product> GetProducts()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("products") as List<product>;
        }

        public bool AddProducts(List<product> products)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("products", products, DateTime.Now.AddMinutes(30));
        }

        public void DeleteProducts()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("products"))
            {
                memoryCache.Remove("products");
            }
        }
        //--------------FORMATS-----------------------------------------------------
        public List<format> GetFormats()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("formats") as List<format>;
        }

        public bool AddFormats(List<format> formats)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("formats", formats, DateTime.Now.AddMinutes(10));
        }

        public void DeleteFormats()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("formats"))
            {
                memoryCache.Remove("formats");
            }
        }

        //---------------------------------EFFECTS-----------------------------------
        public List<effect> GetEffects()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("effects") as List<effect>;
        }

        public bool AddEffects(List<effect> effects)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("effects", effects, DateTime.Now.AddMinutes(10));
        }

        public void DeleteEffects()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("effects"))
            {
                memoryCache.Remove("effects");
            }
        }

        public List<genre> GetGenres()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("genres") as List<genre>;
        }

        public bool AddGenres(List<genre> genres)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("genres", genres, DateTime.Now.AddMinutes(10));
        }

        public void DeleteGenres()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("genres"))
            {
                memoryCache.Remove("genres");
            }
        }

        public List<instrument> GetInstruments()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("instruments") as List<instrument>;
        }

        public bool AddInstruments(List<instrument> instruments)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("instruments", instruments, DateTime.Now.AddMinutes(10));
        }

        public void DeleteInstruments()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("instruments"))
            {
                memoryCache.Remove("instruments");
            }
        }

        public List<tag> GetTags()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("tags") as List<tag>;
        }

        public bool AddTags(List<tag> tags)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("tags", tags, DateTime.Now.AddMinutes(10));
        }

        public void DeleteTags()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("tags"))
            {
                memoryCache.Remove("tags");
            }
        }


        public List<carusel_images> GetCarouselImages()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get("carousel") as List<carusel_images>;
        }

        public bool AddCarouselImages(List<carusel_images> images)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add("carousel", images, DateTime.Now.AddMinutes(10));
        }

        public void DeleteTCarouselImages()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains("carousel"))
            {
                memoryCache.Remove("carousel");
            }
        }

        //public bool Add(Phone value)
        //{
        //    MemoryCache memoryCache = MemoryCache.Default;
        //    return memoryCache.Add(value.Id.ToString(), value, DateTime.Now.AddMinutes(10));
        //}

        //public void Update(Phone value)
        //{
        //    MemoryCache memoryCache = MemoryCache.Default;
        //    memoryCache.Set(value.Id.ToString(), value, DateTime.Now.AddMinutes(10));
        //}

        //public void Delete(int id)
        //{
        //    MemoryCache memoryCache = MemoryCache.Default;
        //    if (memoryCache.Contains(id.ToString()))
        //    {
        //        memoryCache.Remove(id.ToString());
        //    }
        //}
    }
}