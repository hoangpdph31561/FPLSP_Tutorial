using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using FPLSP_Tutorial.Application.Interfaces.Services;
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
            services.AddDbContextPool<AppReadOnlyDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("AppDbConnection"));
            });

            services.AddDbContextPool<AppReadWriteDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("AppDbConnection"));
            });

            services.AddTransient<ILocalizationService, LocalizationService>();

            return services;
        }
    }
}