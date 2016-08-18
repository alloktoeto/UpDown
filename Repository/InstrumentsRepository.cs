using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;

namespace UpDown.Repository
{
    public class InstrumentsRepository : BaseRepository, ICategoryRepository<instrument>
    {
        public List<instrument> GetCategories()
        {
            return _db.instruments.ToList();
        }

        public instrument GetCategory(int id)
        {
            return _db.instruments.FirstOrDefault(x => x.instrumentID == id);
        }



        public void AddCategory(instrument obj)
        {
            _db.instruments.AddObject(obj);
            _db.SaveChanges();
        }

        public void UpdateCategory(instrument obj)
        {
            instrument old = GetCategory(obj.instrumentID);
            old.name = old.name;
            _db.SaveChanges();
        }
    }
}