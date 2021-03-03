using K207Shopping.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Web.ViewModels
{
    public class ProductDetailVM
    {
        public Product  Product { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        public List<Product> SameCategoryPro { get; set; }
    }
}
