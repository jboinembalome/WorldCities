using FluentAssertions;
using WorldCities.Application.Features.Cities.Specifications;
using WorldCities.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WorldCities.Application.UnitTests.Features.Boards.Specifications
{
    public class CityWithCountrySpecTests
    {
        private readonly int cityId1 = 1;

        [Fact]
        public void CityWithCountrySpecTest_ExistingUserId_ReturnsCity()
        {
            // Arrange
            var cities = GetTestCitiesCollection();

            var specification = new CityWithCountrySpec(cityId1);

            // Act
            var firstCity = specification.Evaluate(cities).FirstOrDefault();

            // Assert
            firstCity.Id.Should().Be(cityId1);

            firstCity.Country.Should().NotBeNull();
        }

        public List<City> GetTestCitiesCollection()
        {
            var cityId2 = 2;

            var city1 = new City
            {
                Id = cityId1,
                Name = "Orléans",
                Lat = 1,
                Lon = 2,
                Country = new Country 
                { 
                    Id = 1,
                    Name = "France",
                    ISO2 = "FR",
                    ISO3 = "FRA",
                }
            };
            var city2 = new City { Id = cityId2 };

            var cities = new List<City>() { city1, city2 };

            return cities;
        }
    }
}
