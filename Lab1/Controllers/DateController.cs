using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetLabs.Controllers
{
    public class DateController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Date = Date();
            return View();
        }
        public string Date()
        {
            DateTime thisDay = DateTime.Today;
            return $"Today is {thisDay.ToString("d")}";
        }
    }
}
