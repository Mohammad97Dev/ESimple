using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ESimple.Controllers
{
    public abstract class ESimpleControllerBase: AbpController
    {
        protected ESimpleControllerBase()
        {
            LocalizationSourceName = ESimpleConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
