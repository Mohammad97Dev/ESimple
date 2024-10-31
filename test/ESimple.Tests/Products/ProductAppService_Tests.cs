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

namespace ESimple.Tests.Products
{
    public class ProductAppService_Tests : ESimpleTestBase
    {
        private readonly IProductAppService _productAppService;

        public ProductAppService_Tests()
        {
            _productAppService = Resolve<IProductAppService>();

        }

        [Fact]
        public async Task GetProducts_Test()
        {
            // Act
            var output = await _productAppService.GetAllAsync(new PagedProductResultRequestDto { MaxResultCount = 20, SkipCount = 0 });

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreateProduct_Test()
        {
            // Act
            await _productAppService.CreateAsync(
                new CreateProductDto
                {
                    Name = "Test Product",
                    Description = "This is a test product.",
                    Price = 49.99m,
                    Stock = 100
                });

            await UsingDbContextAsync(async context =>
            {
                var testProduct = await context.Products.FirstOrDefaultAsync(p => p.Name == "Test Product");
                testProduct.ShouldNotBeNull();
                testProduct.Price.ShouldBe(49.99m);
                testProduct.Stock.ShouldBe(100);
            });
        }
    }
}
