using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetLabs.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public string Date()
        {
            DateTime thisDay = DateTime.Today;
            return thisDay.ToString("d");
        }
        public IActionResult RedirectToWsei()
        {
            return Redirect("https://wsei.edu.pl/");
        }
        public IActionResult GetJson()
        {
            return Json(new { Name = "Maciej", Surname = "Duda" });
        }
    }
}
