using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;

namespace UpDown.Repository
{
    public class TagsRepository : BaseRepository, ICategoryRepository<tag>
    {
        public List<tag> GetCategories()
        {
            return _db.tags.ToList();
        }

        public tag GetCategory(int id)
        {
            return _db.tags.FirstOrDefault(x => x.tagId == id);
        }

        public void Delete(tag obj)
        {
            _db.DeleteObject(obj);
            _db.SaveChanges();
        }

        public void AddCategory(tag obj)
        {
            _db.tags.AddObject(obj);
            _db.SaveChanges();
        }

        public void UpdateCategory(tag obj)
        {
            tag old = GetCategory(obj.tagId);
            old.name = obj.name;
            old.url = obj.url;
            _db.SaveChanges();
        }
    }
}