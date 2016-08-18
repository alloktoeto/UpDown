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
            _db.carusel_images.AddObject(obj);
            _db.SaveChanges();
        }

        public void UpdateCategory(carusel_images obj)
        {
            
        }

        public void UpdateCategory(carusel_images obj, string image)
        {
            carusel_images old = GetCategory(obj.imageID);
            old.name = image;
            old.url = obj.url;
            old.text = obj.text;
            old.alt = obj.alt;
            _db.SaveChanges();
        }

        //public List<carusel_images> GetCaruselImages()
        //{
        //    return _db.carusel_images.ToList();
        //}

        //public carusel_images GetCaruselImage(int id)
        //{
        //    return _db.carusel_images.FirstOrDefault(x=>x.imageID==id);
        //}

        //public void UpdateCaruselImage(carusel_images image)
        //{
        //    return _db.carusel_images.FirstOrDefault(x => x.imageID == id);
        //}

        //public void AddCaruselImage(carusel_images image)
        //{
        //    return _db.carusel_images.FirstOrDefault(x => x.imageID == id);
        //}

        //public void DeleteCaruselImage(carusel_images image)
        //{
        //    return _db.carusel_images.FirstOrDefault(x => x.imageID == id);
        //}
    }
}