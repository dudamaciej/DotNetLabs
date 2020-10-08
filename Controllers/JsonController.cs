using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetLabs.Controllers
{
    public class JsonController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Json = GetJson();
            return View();
        }
        public IActionResult GetJson()
        {
            return Json(new { Name = "Maciej", Surname = "Duda" });
        }
    }
}
