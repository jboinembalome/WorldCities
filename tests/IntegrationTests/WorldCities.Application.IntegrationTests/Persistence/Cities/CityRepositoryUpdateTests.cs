using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WorldCities.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.IntegrationTests.Persistence.Boards
{
    [Collection("Database collection")]
    public class CityRepositoryUpdateTests : IAsyncLifetime
    {
        private readonly DatabaseFixture _database;

        public CityRepositoryUpdateTests(DatabaseFixture database)
        {
            _database = database;
        }

        public async Task DisposeAsync() =>  await DatabaseFixture.ResetState();

        public Task InitializeAsync() => Task.CompletedTask;

        [Fact]
        public async Task UpdateAsync_ExistingCity_CityUpdated()
        {           
            // Arrange
            var testCityName = "testCty";
            var city = new City { Name = testCityName, Name_ASCII = testCityName };
            var cityRepository =  _database.GetRepository<City, int>();

            _database.DbContext.Cities.Add(city);
            await _database.DbContext.SaveChangesAsync();

            city.Name = "Updated Name";

            // Act
            await cityRepository.UpdateAsync(city);
            var cityUpdated = await _database.DbContext.Cities.FirstOrDefaultAsync(b => b.Name == city.Name);

            // Assert
            cityUpdated.Should().NotBeNull();
            cityUpdated.Id.Should().Be(city.Id);
            cityUpdated.Name.Should().Be(city.Name);        
        }
    }
}
