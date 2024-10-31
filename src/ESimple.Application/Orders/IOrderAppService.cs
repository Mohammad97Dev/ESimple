using Abp.Application.Services.Dto;
using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESimple.Orders.Dto;

namespace ESimple.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto input);
        Task<OrderDto> GetAsync(EntityDto<int> input);
        Task<ListResultDto<OrderDto>> GetAllAsync();
        Task DeleteAsync(EntityDto<int> input);
    }
}
