using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.Products
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IRepository<Product> _productRepository;

        public ProductManager(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _productRepository.InsertAsync(product);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetAsync(productId);
        }

        public async Task<List<Product>> CheckAndGetListOfProduct(List<int> productIds)
        {
            await CheckIfAllPropertyExist(productIds);
            return await _productRepository.GetAllListAsync(x => productIds.Contains(x.Id));
        }

        private async Task CheckIfAllPropertyExist(List<int> productIds)
        {
            var existingProductCount = await _productRepository.CountAsync(c => productIds.Contains(c.Id));
            // If the count of existing products is not equal to the number of requested IDs, throw an exception
            if (existingProductCount != productIds.Count)
                throw new UserFriendlyException("Not All Products Are Existed !");
        }
    }

}
