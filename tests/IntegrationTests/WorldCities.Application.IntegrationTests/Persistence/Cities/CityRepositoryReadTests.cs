using FluentAssertions;
using WorldCities.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.IntegrationTests.Persistence.Boards
{
    [Collection("Database collection")]
    public class CityRepositoryReadTests : IAsyncLifetime
    {
        private readonly DatabaseFixture _database;

        public CityRepositoryReadTests(DatabaseFixture database)
        {
            _database = database;
        }

        public async Task DisposeAsync() =>  await DatabaseFixture.ResetState();

        public Task InitializeAsync() => Task.CompletedTask;

        [Fact]
        public async Task GetByIdAsync_ExistingId_CityRetrieved()
        {           
            // Arrange
            var testCityName = "testCity";
            var city = new City { Name = testCityName, Name_ASCII = testCityName };
            var cityRepository =  _database.GetRepository<City, int>();

            _database.DbContext.Cities.Add(city);
            await _database.DbContext.SaveChangesAsync();

            int cityId = city.Id;

            // Act
            var cityRetrived = await cityRepository.GetByIdAsync(cityId);

            // Assert
            cityRetrived.Name.Should().Be(city.Name);
            cityRetrived.Id.Should().BeGreaterThan(0);
        }
    }
}
