using System.Collections.Generic;

namespace SportsStore.Models
{
    public interface IProductRepository
    {
        IList<Product> Products { get; }

        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}