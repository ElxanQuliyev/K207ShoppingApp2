using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Web.ViewComponents
{
    public class BasketViewComponent:ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            var productCookie = Request.Cookies["k207Cart"];
            if (productCookie != null && productCookie.Length > 0)
            {
                ViewData["basketId"] = productCookie.Split('-').Select(p => int.Parse(p)).ToList();
            }
            return View();
        }
    }
}
