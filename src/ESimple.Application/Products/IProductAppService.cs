using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ESimple.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Products
{
    public interface IProductAppService : IAsyncCrudAppService<ProductDto, int, PagedProductResultRequestDto, CreateProductDto, UpdateProductDto>
    {
        Task<PagedResultDto<ProductDto>> GetAllAsync(PagedProductResultRequestDto input);
    }
}
