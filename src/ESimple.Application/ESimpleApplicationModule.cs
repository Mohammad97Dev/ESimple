using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ESimple.Authorization;

namespace ESimple
{
    [DependsOn(
        typeof(ESimpleCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ESimpleApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ESimpleAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ESimpleApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
