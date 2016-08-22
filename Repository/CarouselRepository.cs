using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;

namespace UpDown.Repository
{
    public class CarouselRepository : BaseRepository, ICategoryRepository<carusel_images>
    {
        public List<carusel_images> GetCategories()
        {
            return _db.carusel_images.OrderBy(x => x.orderBy).ToList();
        }

        public carusel_images GetCategory(int id)
        {
            return _db.carusel_images.FirstOrDefault(x => x.imageID == id);
        }

        public void Delete(carusel_images obj)
        {
            _db.DeleteObject(obj);
            _db.SaveChanges();
        }

        public void AddCategory(carusel_images obj)
        {
           
        }

        public void AddCategory(carusel_images obj, string image)
        {
            if (String.IsNullOrWhiteSpace(obj.url) || obj==null)
            {
                obj.url = "";
            }
            _db.carusel_images.AddObject(obj);
            _db.SaveChanges();
        }

        public void UpdateCategory(carusel_images obj)
        {
            
        }

        public void UpdateCategory(carusel_images obj, string image)
        {
            carusel_images old = GetCategory(obj.imageID);
            if (String.IsNullOrWhiteSpace(obj.url) || obj == null)
            {
                obj.url = "";
            }
            old.name = image;
            old.url = obj.url;
            old.text = obj.text;
            old.alt = obj.alt;
            _db.SaveChanges();
        }
    }
}