using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WorldCities.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.IntegrationTests.Persistence.Boards
{
    [Collection("Database collection")]
    public class CityRepositoryDeleteTests : IAsyncLifetime
    {
        private readonly DatabaseFixture _database;

        public CityRepositoryDeleteTests(DatabaseFixture database)
        {
            _database = database;
        }

        public async Task DisposeAsync() =>  await DatabaseFixture.ResetState();

        public Task InitializeAsync() => Task.CompletedTask;

        [Fact]
        public async Task DeleteAsync_ExistingCity_CityDeleted()
        {           
            // Arrange
            var testCityName = "testCity";
            var city = new City { Name = testCityName, Name_ASCII = testCityName };
            var cityRepository =  _database.GetRepository<City, int>();

            _database.DbContext.Cities.Add(city);
            await _database.DbContext.SaveChangesAsync();

            // Act
            await cityRepository.DeleteAsync(city);
            var cityDeleted = await _database.DbContext.Cities.FirstOrDefaultAsync(b => b.Name == testCityName);

            // Assert
            cityDeleted.Should().BeNull();
        }
    }
}
