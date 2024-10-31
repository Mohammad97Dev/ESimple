using ESimple.Domains.OrderItems;
using ESimple.Domains.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.EntityFrameworkCore.Seed.Tenants
{
    public class OrderBuilder
    {
        private readonly ESimpleDbContext _context;
        private readonly int _tenantId;

        public OrderBuilder(ESimpleDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateOrder();
        }

        private void CreateOrder()
        {
            if (!_context.Orders.Any())
            {
                // Ensure there are products in the database to create order items
                var product = _context.Products.FirstOrDefault();
                if (product == null)
                {
                    throw new InvalidOperationException("No products found in the database to create order items.");
                }

                var order = new Order
                {
                    UserId = 2, // Assuming a test user with ID 2
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = product.Price * 2, // Example quantity and calculation
                    OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = 2,
                        Price = product.Price
                    }
                }
                };

                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }
    }
}
