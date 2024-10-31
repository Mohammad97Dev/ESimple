using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ESimple.Configuration;

namespace ESimple.Web.Host.Startup
{
    [DependsOn(
       typeof(ESimpleWebCoreModule))]
    public class ESimpleWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ESimpleWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ESimpleWebHostModule).GetAssembly());
        }
    }
}
