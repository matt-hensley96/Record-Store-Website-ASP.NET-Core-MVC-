using Goldies.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Goldies.Data
{
    public class GoldiesSeeder
    {
        private readonly GoldiesContext _ctx;
        private readonly IHostingEnvironment _hosting;
        public GoldiesSeeder(GoldiesContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any())
            {
                // next bit extracts the products from a json file and stores them in a variable
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/products.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                // next bit stores an order (already added in the migration) from GoldiesContext
                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order != null)
                {
                    // Next bit adds some sample items to the order:
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                _ctx.SaveChanges();
            }
        }
    }
}
