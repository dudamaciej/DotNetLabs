using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetLabs.Controllers
{
    public class RedirectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RedirectToWsei()
        {
            return Redirect("https://wsei.edu.pl/");
        }
    }
}
