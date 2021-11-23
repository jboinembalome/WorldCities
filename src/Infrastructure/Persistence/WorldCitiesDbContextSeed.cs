using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorldCities.Domain.Entities;
using WorldCities.Infrastructure.Identity;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Reflection;
using System;

namespace WorldCities.Infrastructure.Persistence
{
    public static class WorldCitiesDbContextSeed
    {
        private const string ADMIN_USER_ID = "31a7ffcf-d099-4637-bd58-2a87641d1aaf";
        private const string ADHERENT_USER_ID = "533f27ad-d3e8-4fe7-9259-ee4ef713dbea";

        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await CreateUser(userManager, roleManager, "Administrator", ADMIN_USER_ID, "administrator@localhost", "administrator@localhost", "Administrator1!");
            await CreateUser(userManager, roleManager, "Adherent", ADHERENT_USER_ID, "adherent@localhost", "adherent@localhost", "Adherent1!");
        }

        public static async Task SeedSampleDataAsync(WorldCitiesDbContext context)
        {
            await LoadDataByExcelFile(context);
            //var franceCities = new Collection<City>
            //{
            //    new City
            //    {
            //        Name = "Orléans",
            //        Name_ASCII = "Orleans",
            //        Lat = 47.9004m,
            //        Lon = 1.9m
            //    },
            //    new City
            //    {
            //        Name = "Paris",
            //        Name_ASCII = "Paris",
            //        Lat = 48.8667m,
            //        Lon = 2.3333m
            //    },
            //};

            //var countries = new Collection<Country>
            //{
            //    new Country
            //     {
            //         Name = "France",
            //         ISO2 = "FR",
            //         ISO3 = "FRA",
            //         Cities = franceCities
            //     },
            //};

            //// Seed, if necessary
            //if (!await context.Countries.AnyAsync())
            //{
            //    await context.Countries.AddRangeAsync(countries);

            //    await context.SaveChangesAsync();
            //}
        }

        private static async Task LoadDataByExcelFile(WorldCitiesDbContext context)
        {
            var dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var path = Path.Combine(dirPath, "Data/Source/worldcities.xlsx");


            using var stream = File.OpenRead(path);
            using var excelPackage = new ExcelPackage(stream);

            // get the first worksheet 
            var worksheet = excelPackage.Workbook.Worksheets[0];

            // define how many rows we want to process
            var nEndRow = worksheet.Dimension.End.Row;

            // initialize the record counters 
            var numberOfCountriesAdded = 0;
            var numberOfCitiesAdded = 0;

            // create a lookup dictionary
            // containing all the countries already existing 
            // into the Database (it will be empty on first run).
            var countriesByName = context.Countries
                .AsNoTracking()
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            // iterates through all rows, skipping the first one 
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];

                var countryName = row[nRow, 5].GetValue<string>();
                var iso2 = row[nRow, 6].GetValue<string>();
                var iso3 = row[nRow, 7].GetValue<string>();

                // skip this country if it already exists in the database
                if (countriesByName.ContainsKey(countryName))
                    continue;

                // create the Country entity and fill it with xlsx data 
                var country = new Country
                {
                    Name = countryName,
                    ISO2 = iso2,
                    ISO3 = iso3
                };

                // add the new country to the DB context 
                await context.Countries.AddAsync(country);

                // store the country in our lookup to retrieve its Id later on
                countriesByName.Add(countryName, country);

                // increment the counter 
                numberOfCountriesAdded++;
            }

            // save all the countries into the Database 
            if (numberOfCountriesAdded > 0) await context.SaveChangesAsync();

            // create a lookup dictionary 
            // containing all the cities already existing 
            // into the Database (it will be empty on first run). 
            var cities = context.Cities
                .AsNoTracking()
                .ToDictionary(x => (
                    Name: x.Name,
                    Lat: x.Lat,
                    Lon: x.Lon,
                    CountryId: x.Country.Id));

            // iterates through all rows, skipping the first one 
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];

                var name = row[nRow, 1].GetValue<string>();
                var nameAscii = row[nRow, 2].GetValue<string>();
                var lat = row[nRow, 3].GetValue<decimal>();
                var lon = row[nRow, 4].GetValue<decimal>();
                var countryName = row[nRow, 5].GetValue<string>();

                // retrieve country Id by countryName
                var countryId = countriesByName[countryName].Id;

                // skip this city if it already exists in the database
                if (cities.ContainsKey((
                    Name: name,
                    Lat: lat,
                    Lon: lon,
                    CountryId: countryId)))
                    continue;

                // create the City entity and fill it with xlsx data 
                var city = new City
                {
                    Name = name,
                    Name_ASCII = nameAscii,
                    Lat = lat,
                    Lon = lon,
                    Country = countriesByName[countryName]
                };

                // add the new city to the DB context 
                context.Cities.Add(city);

                // increment the counter 
                numberOfCitiesAdded++;
            }
            if (numberOfCitiesAdded > 0) await context.SaveChangesAsync();
        }

        private static async Task CreateUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, string role, string userId, string userName, string userMail, string userPassword)
        {
            var identityRole = new IdentityRole(role);

            if (roleManager.Roles.All(r => r.Name != identityRole.Name))
                await roleManager.CreateAsync(identityRole);

            var administrator = new ApplicationUser { Id = userId, UserName = userName, Email = userMail };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                var test = await userManager.CreateAsync(administrator, userPassword);
                await userManager.AddToRolesAsync(administrator, new[] { identityRole.Name });
            }
        }
    }
}
