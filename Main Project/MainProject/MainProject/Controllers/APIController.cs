using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainProject.Controllers
{
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
       private readonly IProductRepository repo;

        public APIController(IProductRepository productRepository)
        {
            this.repo = productRepository;
        }

        /// <summary>
        /// Gets a list of all products
        /// </summary>
        /// <returns>List of all products</returns>
        [HttpGet("AllProduct")]
        public ActionResult<Product> GetAllProduct()
        {
            var products = repo.Products;

            return Ok(products);
        }

        /// <summary>
        /// Get product by product ID
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [HttpGet("ProductById")]
        public ActionResult<Product> GetProductByID(int ProductId)
        {
            var product = repo.Products.SingleOrDefault(p => p.ProductId == ProductId);
            if (product == null) { return NotFound(); }

            return Ok(product);
        }

        /// <summary>
        /// Create new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Introduced Product </returns>
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            repo.SaveProduct(product);
            return Ok(product);
        }

        /// <summary>
        /// Updates product
        /// </summary>
        /// <param name="product">Updated product</param>
        /// <returns>Updated product</returns>
        [HttpPut]
        public ActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!repo.Products.Any(p => p.ProductId == product.ProductId))
                return NotFound();

            repo.SaveProduct(product);

            return NoContent();

        }

        /// <summary>
        /// Deletes product by ID.
        /// </summary>
        /// <param name="ProductId">Product Id</param>
        /// <returns>Nothing</returns>
        [HttpDelete]
        public ActionResult<Product> DeleteProduct(int ProductId)
        {
            repo.DeleteProduct(ProductId);
            return NoContent();
        }
    }
}
