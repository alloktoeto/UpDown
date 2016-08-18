using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;
using System.IO;
using Dapper;
using UpDown.DTO;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace UpDown.Repository
{
    public class ProductsRepository: BaseRepository
    {

        //stp_Products_Get] 


        public List<product> GetProducts()
        {
            //var prods = GetFiles().ToList();
            //var products = ConvertProducts(prods); //_db.products.Where(x => x.isDisplayed == true).OrderBy(o=>o.orderBy).ToList();
            var products = _db.products.Where(x => x.isDisplayed == true).OrderBy(o=>o.orderBy).ToList();

            //var json = new JavaScriptSerializer().Serialize(products);

            //var model = JsonConvert.DeserializeObject<List<product>>(json);

            //var model = new JavaScriptSerializer().Deserialize<List<product>>(json);

            foreach (var item in products)
            {
                if (string.IsNullOrWhiteSpace(item.prod_image))
                {
                    item.prod_image = "default-cover.jpg";
                }
                if (string.IsNullOrWhiteSpace(item.description))
                {
                    item.description = "No description";
                }
            }
            return products;
        }

        private IEnumerable<ProductDTO> GetFiles()
        {
            IEnumerable<ProductDTO> products;
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var lookup = new Dictionary<Guid, ProductDTO>();
                    dbConnection.Open();
                    dbConnection.Query<ProductDTO, ProductStatusDTO, AuthorDTO, EffectDTO, FormatDTO, InstrumentDTO, SubGenreDTO, ProductDTO>(
                            "stp_Products_Get", (prod, prodStatus, author, effect, formats, instruments, subGenres) =>
                    {
                        ProductDTO product;
                        if (!lookup.TryGetValue(prod.Id, out product))
                        {
                            lookup.Add(prod.Id, product = prod);
                        }

                        if (product.Formats == null)
                        {
                            product.Formats = new List<FormatDTO>();
                        }
                        product.Formats.Add(formats);

                        if (product.Instruments == null)
                        {
                            product.Instruments = new List<InstrumentDTO>();
                        }
                        product.Instruments.Add(instruments);

                        if (product.SubGenres == null)
                        {
                            product.SubGenres = new List<SubGenreDTO>();
                        }
                        product.SubGenres.Add(subGenres);
                        return product;
                    },
                        commandType: CommandType.StoredProcedure);
                    products = lookup.Values;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return products;
        }

        

        private List<product> ConvertProducts(List<ProductDTO> productsOld)
        {
            List<product> products = new List<product>();
            foreach (var p in productsOld)
            {
                product product = new product();
                product.prodID = p.Id;
                product.name = p.Name;
                product.url = p.Url;
                product.price = p.Price;
                product.price_New = p.PriceNew;
                product.description = p.Description;
                product.prod_status = p.Status;
                product.authorID = p.authorID;
                product.prod_image = p.Image;
                product.isRecommeded = p.IsRecommended ?? false;
                product.prod_rating = p.Rating;
                product.product_statuses = new product_statuses();
                if (p.ProductStatus!=null)
                {
                    product.product_statuses.Id = p.ProductStatus.Id;
                    product.product_statuses.name = p.ProductStatus.Name;
                    product.product_statuses.cl = p.ProductStatus.Cl;
                }
                product.author = new author();
                if (p.Author!=null)
                {
                    product.author.authorID = p.Author.Id;
                    product.author.name = p.Author.Name;
                }

                product.effect = new effect();
                if (p.Effect!=null)
                {
                    product.effect.effectID = p.Effect.Id;
                    product.effect.name = p.Effect.EffectName;
                }
                product.formatsToProducts = new System.Data.Objects.DataClasses.EntityCollection<formatsToProduct>();
                if (p.Formats!=null)
                {
                    foreach (var format in p.Formats)
                    {
                        formatsToProduct ftp = new formatsToProduct();
                        ftp.format_Id = format.Id;
                        ftp.file_name = format.FileName;
                        ftp.format = new format();
                        ftp.format.formatID = format.Id;
                        ftp.format.name = format.FormatName;
                        product.formatsToProducts.Add(ftp);
                    }
                }

                product.instrumentsToProducts = new System.Data.Objects.DataClasses.EntityCollection<instrumentsToProduct>();
                if (p.Instruments!=null)
                {
                    foreach (var instrument in p.Instruments)
                    {
                        instrumentsToProduct itp = new instrumentsToProduct();
                        itp.instrumentId = instrument.Id;
                        itp.instrument = new instrument();
                        itp.instrument.instrumentID = instrument.Id;
                        itp.instrument.name = instrument.InstrumentName;
                    }
                }

                product.subGenresToProducts = new System.Data.Objects.DataClasses.EntityCollection<subGenresToProduct>();
                if (p.SubGenres!=null)
                {
                    foreach (var subgenre in p.SubGenres)
                    {
                        subGenresToProduct sgtp = new subGenresToProduct();
                        sgtp.SubGenre = new SubGenre();
                        sgtp.SubGenre.genre = new genre();
                        sgtp.SubGenre.subGenreId = subgenre.Id;
                        sgtp.SubGenre.subGenreName = subgenre.SubGenreName;
                        sgtp.SubGenre.genre.genreID = subgenre.GenreId;
                        sgtp.SubGenre.genre.name = subgenre.GenreName;
                    }
                }
                products.Add(product);
            }
            return products;
        }

        public List<product> GetProductsAdmin()
        {
            var products = _db.products;
            foreach (var item in products)
            {
                if (string.IsNullOrWhiteSpace(item.prod_image))
                {
                    item.prod_image = "cover.jpeg";
                }
                if (string.IsNullOrWhiteSpace(item.description))
                {
                    item.description = "No description";
                }
            }
            return products.ToList();
        }

        public product GetProduct(Guid id)
        {
            product product = _db.products.FirstOrDefault(x => x.prodID == id);
            if (string.IsNullOrWhiteSpace(product.prod_image))
            {
                product.prod_image = "cover.jpeg";
            }
            if (string.IsNullOrWhiteSpace(product.description))
            {
                product.description = "No description";
            }
            return product;
        }

        public void UpdateSubGenres(List<string> subgenres, string productId)
        {
            var prodId = new Guid(productId);
            var currentSubgenres = _db.subGenresToProducts.Where(x => x.productId == prodId).ToList();

            if (currentSubgenres!=null && currentSubgenres.Count>0)
            {
                foreach (subGenresToProduct sgtp in currentSubgenres)
                {
                    _db.subGenresToProducts.DeleteObject(sgtp);
                }
            }

            int result;
            
            foreach (string s in subgenres)
            {
                if (int.TryParse(s, out result))
                {
                    subGenresToProduct sp = new subGenresToProduct();
                    sp.productId = prodId;
                    sp.subGenreId = result;
                    _db.subGenresToProducts.AddObject(sp);
                }
            }
            _db.SaveChanges();
        }

        public void AddSubGenres(List<string> subgenres, string productId)
        {
            int result;
            var prodId = new Guid(productId);
            foreach (string s in subgenres)
            {
                if (int.TryParse(s, out result))
                {
                    subGenresToProduct sp = new subGenresToProduct();
                    sp.productId = prodId;
                    sp.subGenreId = result;
                    _db.subGenresToProducts.AddObject(sp);
                }
            }
            _db.SaveChanges();
        }

        public product AddNewProduct(product prod, List<string> instruments, string image)
        {
            prod.prodID = Guid.NewGuid();
            prod.dateUpload = DateTime.Now;
            //prod.url = "url123";
            int result;
            

            foreach (string s in instruments)
            {
                if (int.TryParse(s, out result))
                {
                    instrumentsToProduct sp = new instrumentsToProduct();
                    sp.productId = prod.prodID;
                    sp.instrumentId = result;
                    _db.instrumentsToProducts.AddObject(sp);
                }
            }

            if (!string.IsNullOrWhiteSpace(image))
            {
                prod.prod_image = image;
            }

            _db.products.AddObject(prod);
            _db.SaveChanges();
            System.Web.HttpContext.Current.Application["products"] = null;
            return prod;
        }

        public void UpdateProduct(product prod, List<string> instruments, string image)
        {
            product old = GetProduct(prod.prodID);
            old.name = prod.name;
            old.authorID = prod.authorID;
            //old.author = prod.author;
            //old.dateUpload = prod.dateUpload;
            old.description = prod.description;
            old.description_short = prod.description_short;
            old.price = prod.price;
            old.price_New = prod.price_New;
            old.prod_status = prod.prod_status;
            old.size = prod.size;
            old.soundID = prod.soundID;
            old.isDisplayed = prod.isDisplayed;
            old.isRecommeded = prod.isRecommeded;
            old.orderBy = prod.orderBy;
            if (!string.IsNullOrWhiteSpace(image))
            {
                old.prod_image = image;    
            }
            
            int result;

            var currentInstruments = _db.instrumentsToProducts.Where(x => x.productId == prod.prodID).ToList();

            if (currentInstruments!=null && currentInstruments.Count>0)
            {
                foreach (instrumentsToProduct itp in currentInstruments)
                {
                    _db.instrumentsToProducts.DeleteObject(itp);
                }
            }

            foreach (string s in instruments)
            {
                if (int.TryParse(s, out result))
                {
                    instrumentsToProduct sp = new instrumentsToProduct();
                    sp.productId = prod.prodID;
                    sp.instrumentId = result;
                    _db.instrumentsToProducts.AddObject(sp);
                }
            }

            _db.SaveChanges();
        }

        public void UpdateProductDemo(Guid prod, string demo)
        {
            product old = GetProduct(prod);
            old.url = demo;
            
            old.duration = 12;
            _db.SaveChanges();
        }

        public void RemoveProduct(Guid id)
        {
            var prod = GetProduct(id);
            prod.isDisplayed = false;
            _db.SaveChanges();
            var path = HttpContext.Current.Server.MapPath("~/Files" + "/" + id);
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                di.Delete(true);
            }
            System.Web.HttpContext.Current.Application["products"] = null;
            
        }

        public List<instrumentsToProduct> GetInstrumentsToProducts()
        {
            return _db.instrumentsToProducts.ToList();
        }

        public List<formatsToProduct> GetFormatsToProducts()
        {
            return _db.formatsToProducts.ToList();
        }

        public List<subGenresToProduct> GetSubGenresToProduct()
        {
            return _db.subGenresToProducts.ToList();
        }

        //public List<product> GetProductsByParam(string param, int val)
        //{
        //    switch (param)
        //    {
        //        case "genre":
        //        //return _db.products.Where(x => x.genreID == val).ToList();
        //        //case "format":
        //        //    return _db.products.Where(x => x.formatID == val).ToList();
        //        case "instrument":
        //        //return _db.products.Where(x => x.instrumentID == val).ToList();
        //        case "sound":
        //        //return _db.products.Where(x => x.soundID == val).ToList();
        //        case "effect":
        //        //return _db.products.Where(x => x.effectID == val).ToList();
        //        default:
        //            return _db.products.ToList();
        //    }
        //}
    }
}