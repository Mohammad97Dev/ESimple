using ESimple.Domains.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.EntityFrameworkCore.Seed.Tenants
{
    public class ProductBuilder
    {
        private readonly ESimpleDbContext _context;
        private readonly int _tenantId;

        public ProductBuilder(ESimpleDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }
        public void Create()
        {
            CreateProduct();
        }
        private void CreateProduct()
        {
            if (!_context.Products.Any())
            {
                _context.Products.AddAsync(
                     new Product { Name = "Sample Product 1", Description = "Description", Price = 25.00m, Stock = 50, ImageUrl = "Uploads\\Attachments\\defaultUser.png", LowImageUrl = "Uploads\\LowResolutionPhotos\\defaultUser.png" }
                 );
                _context.SaveChanges();
            }
        }
    }
}
