using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WorldCities.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.IntegrationTests.Persistence.Boards
{
    [Collection("Database collection")]
    public class CityRepositoryCreateTests : IAsyncLifetime
    {
        private readonly DatabaseFixture _database;

        public CityRepositoryCreateTests(DatabaseFixture database)
        {
            _database = database;
        }

        public async Task DisposeAsync() =>  await DatabaseFixture.ResetState();

        public Task InitializeAsync() => Task.CompletedTask;

        [Fact]
        public async Task AddAsync_ValidBoard_BoardAdded()
        {           
            // Arrange
            var testCityName = "testCity";
            var city = new City { Name = testCityName, Name_ASCII = testCityName };
            var cityRepository =  _database.GetRepository<City, int>();

            // Act
            var newCity = await cityRepository.AddAsync(city);
            var cityAdded = (await _database.DbContext.Cities.ToListAsync())[0];

            // Assert
            cityAdded.Name.Should().Be(newCity.Name);
            cityAdded.Id.Should().BeGreaterThan(0);
        }
    }
}
