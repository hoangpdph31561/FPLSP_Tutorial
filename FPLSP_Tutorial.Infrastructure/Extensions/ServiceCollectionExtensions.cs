using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite;
using FPLSP_Tutorial.Infrastructure.Implements.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FPLSP_Tutorial.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            #region App DbContext
            services.AddDbContextPool<AppReadOnlyDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddDbContextPool<AppReadWriteDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });
            #endregion

            #region Example DbContext
            services.AddDbContextPool<ExampleReadOnlyDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddDbContextPool<ExampleReadWriteDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });
            #endregion

            #region Transients
            services.AddTransient<ILocalizationService, LocalizationService>();
            services.AddTransient<IExampleReadOnlyRepository, ExampleReadOnlyRepository>();
            services.AddTransient<IExampleReadWriteRepository, ExampleReadWriteRepository>();
            services.AddTransient<ITagReadOnlyRepository, TagReadOnlyRepository>();
            services.AddTransient<ITagReadWriteRepository, TagReadWriteRepository>();
            services.AddTransient<IMajorReadOnlyRepository, MajorReadOnlyRepository>();
            services.AddTransient<IMajorReadWriteRepository, MajorReadWriteRepository>();
            services.AddTransient<IPostTagReadWriteRespository, PostTagReadWriteRepository>();
            services.AddTransient<IUserReadOnlyRepository, UserReadOnlyRepository>();
            services.AddTransient<IUserReadWriteRepository, UserReadWriteRepository>();
            services.AddTransient<IClientPostReadOnlyRespository, ClientPostReadOnlyRespository>();
            services.AddTransient<IClientPostReadWriteRespository, ClientPostReadWriteRespository>();
            #endregion
            return services;
        }
    }
}