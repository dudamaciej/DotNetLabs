using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext _ctx;
        public EFProductRepository(AppDbContext ctx)
        {
            this._ctx = ctx;
        }
        public IQueryable<Product> Products =>_ctx.Products;

        public Product DeleteProduct(int ProductId)
        {
            Product dbEntry = _ctx.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if(dbEntry != null)
            {
                _ctx.Products.Remove(dbEntry);
                _ctx.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if(product.ProductId == 0)
            {
                _ctx.Products.Add(product);
            }
            else
            {
                Product dbEntry = _ctx.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

                if(dbEntry != null)
                {
                    dbEntry.ProductName = product.ProductName;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            _ctx.SaveChanges();
        }
    }
}
