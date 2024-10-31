using System.Threading.Tasks;
using Abp.Application.Services;
using ESimple.Authorization.Accounts.Dto;

namespace ESimple.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
