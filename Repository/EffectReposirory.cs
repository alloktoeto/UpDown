using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;

namespace UpDown.Repository
{
    public class EffectReposirory : BaseRepository, ICategoryRepository<effect>
    {
        public List<effect> GetCategories()
        {
            return _db.effects.ToList();
        }

        public effect GetCategory(int id)
        {
            return _db.effects.FirstOrDefault(x => x.effectID == id);
        }



        public void AddCategory(effect obj)
        {
            _db.effects.AddObject(obj);
            _db.SaveChanges();
        }

        public void UpdateCategory(effect obj)
        {
            effect old = GetCategory(obj.effectID);
            old.name = obj.name;
            _db.SaveChanges();
        }
    }
}