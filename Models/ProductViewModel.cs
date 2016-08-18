using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpDown.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public List<int> FormatsSelectedIdList { get; set; }

        public ProductViewModel(product product, int formatsCount)
        {
            Id = product.prodID;
            Name = product.name;
            Quantity = formatsCount;
            FormatsSelectedIdList = new List<int>();
            if (product.price_New==0 || product.price_New==null)
            {
                Price = product.price;
            }
            else
            {
                Price = product.price_New;
            }
        }
    }
}