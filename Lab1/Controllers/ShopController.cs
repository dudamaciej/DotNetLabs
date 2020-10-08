using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static DotNetLabs.Models.ShopProductModel;

namespace DotNetLabs.Controllers
{
    public class ShopController : Controller
    {

        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product() { Name = "Air Joran 1 Mid Shadow", Description = "size 42 color: black/white", Price = 699 });
            products.Add(new Product() { Name = "Yeezy Boost 350v2", Description = "size 43 color: red", Price = 899 });
            products.Add(new Product() { Name = "Sacai", Description = "size 45 color: nylon-white", Price = 1000 });
            return View(products);
        }
    }
}
