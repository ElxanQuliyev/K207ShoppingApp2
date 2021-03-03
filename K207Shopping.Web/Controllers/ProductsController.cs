using K207Shopping.Web.Data;
using K207Shopping.Web.Models;
using K207Shopping.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Web.Controllers
{
    public class ProductsController : Controller
    {
        private ShoppingContext _context;

        public ProductsController(ShoppingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindProductByID(int? productId)
        {
            var productList = _context.Products.Include("ProductPictures.Picture")
                .FirstOrDefault(x => x.ID == productId);
            HomeVM vm = new HomeVM()
            {
                Product = productList
            };
            return PartialView("ProductQuickView", vm);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Product singlePro = _context.Products.Include("ProductPictures.Picture")
                .FirstOrDefault(p => p.ID == id);


            if (singlePro == null) return NotFound();
            ProductDetailVM vm = new ProductDetailVM();
            vm.Product = singlePro;

            vm.FeaturedProducts = _context.Products.Include("ProductPictures.Picture")
                .Where(x => x.IsFeatured).ToList();

            vm.SameCategoryPro = await _context.Products.Include("ProductPictures.Picture")
                .Where(x => x.CategoryID == singlePro.CategoryID && x.ID!=singlePro.ID)
                .Take(10)
                .ToListAsync();

            return View(vm);
        }
    }
}
