using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using ESimple.Authorization;
using ESimple.Domains.Products;
using ESimple.Products.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Products
{
    [AbpAuthorize]
    public class ProductAppService : AsyncCrudAppService<Product, ProductDto, int, PagedProductResultRequestDto, CreateProductDto, UpdateProductDto>, IProductAppService
    {
        private readonly IProductManager _productManager;
        private readonly string _appBaseUrl;

        public ProductAppService(IRepository<Product, int> repository,
            IProductManager productManager,
            IConfiguration configuration)
            : base(repository)
        {
            _productManager = productManager;
            _appBaseUrl = configuration[ESimpleConsts.AppServerRootAddressKey] ?? "/";
        }
        [AbpAuthorize(PermissionNames.Product_Create)]
        public override async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = ObjectMapper.Map<Product>(input);
            var createdProduct = await _productManager.CreateProductAsync(product);
            return ObjectMapper.Map<ProductDto>(createdProduct);
        }
        [AbpAuthorize(PermissionNames.Product_Update)]
        public override async Task<ProductDto> UpdateAsync(UpdateProductDto input)
        {
            var product = await _productManager.GetProductByIdAsync(input.Id);
            ObjectMapper.Map(input, product);
            var updatedProduct = await _productManager.UpdateProductAsync(product);
            return ObjectMapper.Map<ProductDto>(updatedProduct);
        }
        [AbpAuthorize(PermissionNames.Product_Get)]
        public override async Task<ProductDto> GetAsync(EntityDto<int> input)
        {
            var product = await Repository.GetAsync(input.Id);
            var dto = ObjectMapper.Map<ProductDto>(product);
            Uri baseUri = new Uri(_appBaseUrl);
            dto.ImageUrl = (new Uri(baseUri, dto.ImageUrl)).AbsoluteUri;
            dto.LowImageUrl = (new Uri(baseUri, dto.LowImageUrl)).AbsoluteUri;
            return dto;
        }
        [AbpAuthorize(PermissionNames.Product_List)]
        public override async Task<PagedResultDto<ProductDto>> GetAllAsync(PagedProductResultRequestDto input)
        {
            var productList = await base.GetAllAsync(input);
            Uri baseUri = new Uri(_appBaseUrl);
            foreach (var item in productList.Items)
            {
                item.ImageUrl = (new Uri(baseUri, item.ImageUrl)).AbsoluteUri;
                item.LowImageUrl = (new Uri(baseUri, item.LowImageUrl)).AbsoluteUri;
            }
            return productList;
        }
        [AbpAuthorize(PermissionNames.Product_Delete)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            await _productManager.DeleteProductAsync(input.Id);
        }

        protected override IQueryable<Product> CreateFilteredQuery(PagedProductResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input);
            if (!string.IsNullOrEmpty(input.Keyword))
                data = data.Where(x => input.Keyword.Contains(x.Name) || input.Keyword.Contains(x.Description));
            if (input.MinPrice.HasValue)
                data = data.Where(x => x.Price >= input.MinPrice.Value);
            if (input.MaxPrice.HasValue)
                data = data.Where(x => x.Price <= input.MaxPrice.Value);
            return data;
        }

        protected override IQueryable<Product> ApplySorting(IQueryable<Product> query, PagedProductResultRequestDto input)
        {
            if (input.SortByName)
                return query.OrderBy(x => x.Name);
            if (input.SortByDate)
                return query.OrderBy(x => x.CreationTime);
            return query.OrderBy(x => x.CreationTime);
        }
    }
}
