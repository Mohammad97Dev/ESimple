using ESimple.Orders.Dto;
using ESimple.Orders;
using ESimple.Products.Dto;
using ESimple.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Abp.Application.Services.Dto;

namespace ESimple.Tests.Orders
{
    public class OrderAppService_Tests : ESimpleTestBase
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IProductAppService _productAppService;

        public OrderAppService_Tests()
        {
            _orderAppService = Resolve<IOrderAppService>();
            _productAppService = Resolve<IProductAppService>(); // Needed to seed products for order items
        }

        [Fact]
        public async Task GetOrders_Test()
        {
            // Act
            var output = await _orderAppService.GetAllAsync(); 

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreateOrder_Test()
        {
            var product = await _productAppService.GetAsync(new EntityDto(1));
            await _orderAppService.CreateAsync(new CreateOrderDto
            {
                Items = new List<CreateOrderItemDto>
            {
                new CreateOrderItemDto
                {
                    ProductId = product.Id,
                    Quantity = 2
                }
            }
            });

            // Assert: Verify the order and order items were created in the database
            await UsingDbContextAsync(async context =>
            {
                var testOrder = await context.Orders.FirstOrDefaultAsync(o => o.UserId == 2);
                testOrder.ShouldNotBeNull();
                testOrder.TotalAmount.ShouldBe(50.00m);

                var orderItem = await context.OrderItems.FirstOrDefaultAsync(i => i.OrderId == testOrder.Id);
                orderItem.ShouldNotBeNull();
                orderItem.ProductId.ShouldBe(product.Id);
                orderItem.Quantity.ShouldBe(2);
                orderItem.Price.ShouldBe(product.Price);
            });
        }
    }
}
