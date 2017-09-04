
using System.Collections.Generic;

namespace SportsStore.Models
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Order {get;}

        void SaveOrder(Order order);
    }
}