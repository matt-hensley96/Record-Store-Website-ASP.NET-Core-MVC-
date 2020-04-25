using Goldies.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<StoreUser> _userManager;

        public GoldiesSeeder(GoldiesContext ctx, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedASync()
        {
            _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("matthensley42@gmail.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Matt",
                    LastName = "Hensley",
                    Email = "matthensley42@gmail.com",
                    UserName = "matthensley42@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "Password1!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

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
                    order.User = user;
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
