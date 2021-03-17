using K207Shopping.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Web.ViewModels
{
    public class CheckoutVM
    {
        public List<Product> Products { get; set; }
        public List<int> ProductIds { get; set; }
        public Task<K207User> K207User { get; set; }
    }
}
