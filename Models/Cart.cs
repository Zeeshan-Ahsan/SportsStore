using System.Linq;
using System.Collections.Generic;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.FirstOrDefault(l => l.Product.ProductID == product.ProductID);

            if(line == null)
            {
                line = new CartLine
                {
                    Product = product,
                    Quantity = quantity
                };

                lineCollection.Add(line);
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) => lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        
        public virtual decimal ComputeTotalValue() => lineCollection.Sum(l => l.Product.Price * l.Quantity);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines { get => lineCollection; }
    }
}