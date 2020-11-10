using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MainProject.Models;

namespace MainProject.Controllers
{
    public class ProductController : Controller 
    {
        public readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult ListAll() => View(repository.Products);

        public ViewResult List(string category) => View(repository.Products.Where(product => product.Category == category));
    }
}

