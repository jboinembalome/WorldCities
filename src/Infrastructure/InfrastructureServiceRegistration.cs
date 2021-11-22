using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldCities.Application.Interfaces.Common;
using WorldCities.Application.Interfaces.FileExport;
using WorldCities.Application.Interfaces.Identity;
using WorldCities.Application.Interfaces.Logging;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Infrastructure.Common;
using WorldCities.Infrastructure.FileExport;
using WorldCities.Infrastructure.Identity;
using WorldCities.Infrastructure.Logging;
using WorldCities.Infrastructure.Persistence;
using WorldCities.Infrastructure.Persistence.Repositories;

namespace WorldCities.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<WorldCitiesDbContext>(options =>
                    options.UseInMemoryDatabase("WorldCitiesDb"));
            }
            else
            {
                services.AddDbContext<WorldCitiesDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(WorldCitiesDbContext).Assembly.FullName)));
            }

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WorldCitiesDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, WorldCitiesDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient(typeof(ICsvExporter<>), typeof(CsvExporter<>));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddAuthentication()
                .AddIdentityServerJwt();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            //});

            return services;
        }
    }
}
