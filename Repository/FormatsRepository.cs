using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;

namespace UpDown.Repository
{
    public class FormatsRepository : BaseRepository, ICategoryRepository<format>
    {
        public List<format> GetCategories()
        {
            return _db.formats.ToList();
        }

        public format GetCategory(int id)
        {
            return _db.formats.FirstOrDefault(x => x.formatID == id);
        }


        public void AddCategory(format obj)
        {
            _db.formats.AddObject(obj);
            _db.SaveChanges();
        }

        public void UpdateCategory(format obj)
        {
            format old = GetCategory(obj.formatID);
            old.name = obj.name;
            _db.SaveChanges();
        }

        public List<int> GetFormatsByProductId(Guid productId)
        {
            List<int> formatsByProduct = new List<int>();
            formatsByProduct = _db.formatsToProducts.Where(x => x.product_Id == productId).Select(x => x.format_Id).ToList();
            return formatsByProduct;
        }

        public List<formatsToProduct> GetFormatsToProductByProduct(Guid productId)
        {
            return _db.formatsToProducts.Where(x => x.product_Id == productId).ToList();
        }

        public formatsToProduct GetFormatsToProductByProduct(Guid productId, int formatId)
        {
            return _db.formatsToProducts.FirstOrDefault(x => x.product_Id == productId && x.format_Id == formatId);
        }

        public void AddFormatByProduct(Guid productId, int formatId, string filename)
        {
            var formatsByProduct = _db.formatsToProducts.Where(x => x.product_Id == productId).Select(x => x.format_Id).ToList();
            _db.formatsToProducts.AddObject(new formatsToProduct { format_Id = formatId, product_Id = productId, file_name=filename });
            _db.SaveChanges();
        }

        public void DeleteFormatFromProduct(Guid productId, int formatId)
        {
            var formatToDelete = _db.formatsToProducts.FirstOrDefault(x => x.product_Id == productId && x.format_Id==formatId);

            _db.formatsToProducts.DeleteObject(formatToDelete);
            //_db.formatsToProducts.AddObject(new formatsToProduct { format_Id = formatId, product_Id = productId, file_name = filename });
            _db.SaveChanges();
        }
    }
}