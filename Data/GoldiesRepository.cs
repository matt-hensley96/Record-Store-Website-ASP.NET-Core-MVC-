using Goldies.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldies.Data
{
    public class GoldiesRepository : IGoldiesRepository
    {
        private readonly GoldiesContext _ctx;
        private readonly ILogger<GoldiesRepository> _logger;
        public GoldiesRepository(GoldiesContext ctx, ILogger<GoldiesRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
            }
            else
            {
                return _ctx.Orders
                        .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                        .Where(o => o.User.UserName == username)
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
            }
            else
            {
                return _ctx.Orders
                        .Where(o => o.User.UserName == username)
                        .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");

                return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id && o.User.UserName == username)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try
            {
                _logger.LogInformation("GetProductsByCategory was called");

                return _ctx.Products
                    .Where(p => p.Category == category)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products by category: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
