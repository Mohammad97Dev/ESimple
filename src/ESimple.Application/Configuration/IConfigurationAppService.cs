using System.Threading.Tasks;
using ESimple.Configuration.Dto;

namespace ESimple.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
