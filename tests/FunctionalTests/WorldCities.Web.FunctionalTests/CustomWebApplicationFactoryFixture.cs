using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldCities.Infrastructure.Identity;
using WorldCities.Infrastructure.Persistence;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WorldCities.Web.FunctionalTests
{
    public class CustomWebApplicationFactoryFixture<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private static IConfigurationRoot _configuration;

        public ApplicationUser AdministratorUser { get; private set; }

        public CustomWebApplicationFactoryFixture()
        {
            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .AddEnvironmentVariables();

            _configuration = configurationBuilder.Build();
        }

        protected override void Dispose(bool disposing)
        {

            using var scope = Services.CreateScope();

            var context = scope.ServiceProvider.GetService<WorldCitiesDbContext>();

            context.Database.EnsureDeleted();

            base.Dispose(disposing);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.UseConfiguration(_configuration);

            builder.ConfigureServices(services =>
            {
                // Remove the WorldCitiesDbContext registration present in Startup.
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<WorldCitiesDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Add a new WorldCitiesDbContext registration.
                if (_configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    services.AddDbContext<WorldCitiesDbContext>(options =>
                        options.UseInMemoryDatabase("WorldCitiesDb"));
                }
                else
                {
                    services.AddDbContext<WorldCitiesDbContext>(options =>
                        options.UseSqlServer(
                            _configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly(typeof(WorldCitiesDbContext).Assembly.FullName)));
                }

                var serviceProvider = services.BuildServiceProvider();

                EnsureDatabase(serviceProvider).Wait();
            });
        }

        private static async Task EnsureDatabase(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetService<WorldCitiesDbContext>();

            if (!context.Database.IsInMemory())
                context.Database.Migrate();
            else
                context.Database.EnsureCreated();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await WorldCitiesDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
            await WorldCitiesDbContextSeed.SeedSampleDataAsync(context);
        }
    }

}
