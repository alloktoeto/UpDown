using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;

namespace UpDown.Repository
{
    public class AuthorRepository : BaseRepository
    {
        public List<author> GetAuthors()
        {
            return _db.authors.ToList();
        }

        public author GetAuthor(int id)
        {
            return _db.authors.FirstOrDefault(x => x.authorID == id);
        }

        public void AddAuthor(author author)
        {
            _db.authors.AddObject(author);
            _db.SaveChanges();
        }

        public void UpdateAuthor(author author)
        {
            author old = GetAuthor(author.authorID);
            old.name = author.name;
            _db.SaveChanges();
        }
    }
}