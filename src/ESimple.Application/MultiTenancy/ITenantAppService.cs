using Abp.Application.Services;
using ESimple.MultiTenancy.Dto;

namespace ESimple.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

