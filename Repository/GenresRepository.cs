using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;

namespace UpDown.Repository
{
    public class GenresRepository : BaseRepository, ICategoryRepository<genre>
    {
        public List<genre> GetCategories()
        {
            return _db.genres.ToList();
        }

        public genre GetCategory(int id)
        {
            return _db.genres.FirstOrDefault(x => x.genreID == id);
        }



        public void UpdateCategory(genre g)
        {
            genre old = GetCategory(g.genreID);
            old.name = g.name;
            _db.SaveChanges();
        }


        public void AddCategory(genre obj)
        {
            _db.genres.AddObject(obj);
            _db.SaveChanges();
        }

        public List<SubGenre> GetSubGenres()
        {
            return _db.SubGenres.ToList();
        }

        public SubGenre GetSubGenre(int subId)
        {
            return _db.SubGenres.FirstOrDefault(x => x.subGenreId == subId);
        }

        public void DeleteGenre(int genreId)
        {
            var g = _db.genres.FirstOrDefault(x => x.genreID == genreId);
            for (int i = 0; i < g.SubGenres.Count; i++)
            {
                var x = g.SubGenres.ToList()[i].subGenreId;
                DeleteSubGenre(x);
                i--;
            }
            _db.genres.DeleteObject(g);
            _db.SaveChanges();
        }


        public void AddSubGenre(SubGenre subGenre)
        {
            _db.SubGenres.AddObject(subGenre);
            _db.SaveChanges();
        }

        public void UpdateSubGenre(SubGenre subGenre)
        {
            SubGenre old = GetSubGenre(subGenre.subGenreId);
            old.subGenreName = subGenre.subGenreName;
            _db.SaveChanges();
        }

        public void DeleteSubGenre(int subId)
        {
            SubGenre subGenre = GetSubGenre(subId);
            _db.DeleteObject(subGenre);
            _db.SaveChanges();
        }
    }
}