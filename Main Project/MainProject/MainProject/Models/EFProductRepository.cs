using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext ctx;
        public EFProductRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
        }
        public IQueryable<Product> Products => ctx.Products;

        public Product DeleteProduct(int ProductId)
        {
            Product dbEntry = ctx.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if(dbEntry != null)
            {
                ctx.Products.Remove(dbEntry);
                ctx.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if(product.ProductId == 0)
            {
                ctx.Products.Add(product);
            }
            else
            {
                Product dbEntry = ctx.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

                if(dbEntry != null)
                {
                    dbEntry.ProductName = product.ProductName;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            ctx.SaveChanges();
        }
    }
}
