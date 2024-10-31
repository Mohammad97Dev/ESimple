using System;
using Castle.MicroKernel.Registration;
using NSubstitute;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Configuration.Startup;
using Abp.Net.Mail;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Abp.Zero.EntityFrameworkCore;
using ESimple.EntityFrameworkCore;
using ESimple.Tests.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace ESimple.Tests
{
    [DependsOn(
        typeof(ESimpleApplicationModule),
        typeof(ESimpleEntityFrameworkModule),
        typeof(AbpTestBaseModule)
        )]
    public class ESimpleTestModule : AbpModule
    {
        public ESimpleTestModule(ESimpleEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;
        }

        public override void PreInitialize()
        {
            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);
            Configuration.UnitOfWork.IsTransactional = false;

            // Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
            Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            RegisterFakeService<AbpZeroDbMigrator<ESimpleDbContext>>();

            Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ESimpleTestModule).Assembly);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();


            IocManager.IocContainer.Register(
                Component.For<IConfiguration>()
                    .Instance(configuration)
                    .LifestyleSingleton()
            );

            var webHostEnvironmentMock = Substitute.For<IWebHostEnvironment>();
            webHostEnvironmentMock.EnvironmentName.Returns("Development"); // or whatever environment you want to set
            //webHostEnvironmentMock.ContentRootPath.Returns("/path/to/content/root"); // set to your content root path

            IocManager.IocContainer.Register(
                Component.For<IWebHostEnvironment>()
                    .Instance(webHostEnvironmentMock)
                    .LifestyleSingleton()
            );
            ServiceCollectionRegistrar.Register(IocManager);
        }

        private void RegisterFakeService<TService>() where TService : class
        {
            IocManager.IocContainer.Register(
                Component.For<TService>()
                    .UsingFactoryMethod(() => Substitute.For<TService>())
                    .LifestyleSingleton()
            );
        }
    }
}
