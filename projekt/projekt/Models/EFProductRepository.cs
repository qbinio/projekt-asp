using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt.Models;

namespace projekt.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext ctx;
        public EFProductRepository(AppDbContext ctx)
        {
           this.ctx = ctx;
        }

        public IQueryable<Product> Products => ctx.Products;

        public void SaveProduct(Product product)
        {
            if(product.ProductId == 0)
            {
                ctx.Products.Add(product);
            }
            else
            {
                Product dbEntry = ctx.Products
                    .FirstOrDefault(p => p.ProductId == product.ProductId);
                if(dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            ctx.SaveChanges();
        }
        public Product DeleteProduct(int prodcutId)
        {
            Product dbEntry = ctx.Products
                .FirstOrDefault(p => p.ProductId == prodcutId);
            if(dbEntry != null)
            {
                ctx.Products.Remove(dbEntry);
                ctx.SaveChanges();
            }
            return dbEntry;
        }
    }
}
