using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbCtx;

        public ProductRepository(AppDbContext ctx)
        {
            _appDbCtx = ctx;
        }

        public IList<Product> Products => _appDbCtx.Products.ToList();

        public Product DeleteProduct(int productId)
        {
            Product p = _appDbCtx.Products.FirstOrDefault(i => i.ProductID == productId);
            if (p != null)
            {
                _appDbCtx.Products.Remove(p);
                _appDbCtx.SaveChanges();
            }
            return p;
        }

        public void SaveProduct(Product product)
        {
            var p = _appDbCtx.Products.FirstOrDefault(i => i.ProductID == product.ProductID);
            if (p == null)
            {
                _appDbCtx.Products.Add(product);
            }
            else
            {
                p.Name = product.Name;
                p.Category = product.Category;
                p.Description = product.Description;
                p.Price = product.Price;
            }

            _appDbCtx.SaveChanges();
        }
    }
}