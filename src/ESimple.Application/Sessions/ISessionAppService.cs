using System.Threading.Tasks;
using Abp.Application.Services;
using ESimple.Sessions.Dto;

namespace ESimple.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
