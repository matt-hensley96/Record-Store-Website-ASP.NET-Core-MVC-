using Goldies.Data.Entities;
using System.Collections.Generic;

namespace Goldies.Data
{
    public interface IGoldiesRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        
        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}