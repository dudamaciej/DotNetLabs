using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Air Joran 1 Mid Shadow", Description = "size 44 color: black/white", Price = 1699, Category = "Sneakers"},
            new Product { ProductId = 2, ProductName = "Air Joran 11 Light Smoke", Description = "size 41 color: black/white", Price = 1099, Category = "Sneakers"},
            new Product { ProductId = 3, ProductName = "Air Joran 1 Low Laser Orange", Description = "size 40 color: orange/white", Price = 999, Category = "Sneakers"},
        }.AsQueryable<Product>();
       

        void IProductRepository.SaveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        Product IProductRepository.DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
