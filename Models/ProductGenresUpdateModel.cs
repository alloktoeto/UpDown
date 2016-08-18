using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpDown.Models
{
    public class ProductGenresUpdateModel
    {
        public List<genre> Genres { get; set; }
        public product Product { get; set; }
    }
}