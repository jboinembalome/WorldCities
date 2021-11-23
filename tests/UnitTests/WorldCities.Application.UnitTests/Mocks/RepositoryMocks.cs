using Ardalis.Specification;
using Moq;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace WorldCities.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IAsyncRepository<City, int>> GetCityRepository()
        {
            #region Fake data for countries
            var countries = new Collection<Country>
            {
                new Country
                {
                    Id = 1,
                    Name = "France",
                    ISO2 = "FR",
                    ISO3= "FRA",
                },
                new Country
                {
                    Id = 2,
                    Name = "Italy",
                    ISO2 = "IT",
                    ISO3= "ITA"
                },
                new Country
                {
                    Id = 3,
                    Name = "Germany",
                    ISO2 = "GR",
                    ISO3= "GER"
                },
            };

            #endregion

            #region Fake data for cities
            var cities = new Collection<City>
            {
                new City
                {
                    Id = 1,
                    Name = "Paris",
                    Name_ASCII = "Paris",
                    Lat = 1,
                    Lon = 1,
                    Country = countries[0]
                },
                new City
                {
                    Id = 2,
                    Name = "London",
                    Name_ASCII = "London",
                    Lat = 2,
                    Lon = 2,
                    Country = countries[1]
                },
                new City
                {
                    Id = 3,
                    Name = "Leipzig",
                    Name_ASCII = "Leipzig",
                    Lat = 3,
                    Lon = 3,
                    Country = countries[2]
                },
            };
            #endregion


            var mockCityRepository = new Mock<IAsyncRepository<City, int>>();
            mockCityRepository.Setup(repo => repo.ListAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(cities);

            mockCityRepository.Setup(repo => repo.ListAsync(It.IsAny<ISpecification<City>>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                  (ISpecification<City> specification, CancellationToken cancellationToken) =>
                  {
                      IReadOnlyList<City> cityList = specification.Evaluate(cities).ToList().AsReadOnly();
                      return cityList;
                  });

            mockCityRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(
              (int id, CancellationToken cancellationToken) =>
              {
                  var city = cities.FirstOrDefault(c => c.Id == id);
                  return city;
              });

            mockCityRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<City>>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                (ISpecification<City> specification, CancellationToken cancellationToken) =>
                {
                    City city = specification.Evaluate(cities).FirstOrDefault();
                    return city;
                });

            mockCityRepository.Setup(repo => repo.AddAsync(It.IsAny<City>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                (City city, CancellationToken cancellationToken) =>
                {
                    city.Id = cities.Count + 1;
                    cities.Add(city);
                    return city;
                });

            mockCityRepository.Setup(repo => repo.UpdateAsync(It.IsAny<City>(), It.IsAny<CancellationToken>())).Callback(
                (City city, CancellationToken cancellationToken) =>
                {
                    var cityToBeUpdated = cities.FirstOrDefault(c => c.Id == city.Id);
                    cityToBeUpdated = city;
                });

            mockCityRepository.Setup(repo => repo.DeleteAsync(It.IsAny<City>(), It.IsAny<CancellationToken>())).Callback(
               (City city, CancellationToken cancellationToken) =>
               {
                   cities.Remove(city);
               });

            return mockCityRepository;
        }

        public static Mock<IAsyncRepository<Country, int>> GetCountryRepository()
        {
            #region Fake data for cities
            var cities = new Collection<City>
            {
                new City
                {
                    Id = 1,
                    Name = "Paris",
                    Lat = 1,
                    Lon = 1,
                },
                new City
                {
                    Id = 2,
                    Name = "London",
                    Lat = 2,
                    Lon = 2,
                },
                new City
                {
                    Id = 3,
                    Name = "Leipzig",
                    Lat = 3,
                    Lon = 3,
                },
            };
            #endregion

            #region Fake data for countries
            var countries = new Collection<Country>
            {
                new Country
                {
                    Id = 1,
                    Name = "France",
                    ISO2 = "FR",
                    ISO3= "FRA",
                    Cities = new Collection<City> { cities[0] }
                },
                new Country
                {
                    Id = 2,
                    Name = "Italy",
                    ISO2 = "IT",
                    ISO3= "ITA",
                    Cities = new Collection<City> { cities[1] }
                },
                new Country
                {
                    Id = 3,
                    Name = "Germany",
                    ISO2 = "GR",
                    ISO3= "GER",
                    Cities = new Collection<City> { cities[1] }
                },
            };

            #endregion

            var mockCountryRepository = new Mock<IAsyncRepository<Country, int>>();
            mockCountryRepository.Setup(repo => repo.ListAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(countries);

            mockCountryRepository.Setup(repo => repo.ListAsync(It.IsAny<ISpecification<Country>>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                  (ISpecification<Country> specification, CancellationToken cancellationToken) =>
                  {
                      IReadOnlyList<Country> countryList = specification.Evaluate(countries).ToList().AsReadOnly();
                      return countryList;
                  });

            mockCountryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(
              (int id, CancellationToken cancellationToken) =>
              {
                  var country = countries.FirstOrDefault(c => c.Id == id);
                  return country;
              });

            mockCountryRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Country>>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                (ISpecification<Country> specification, CancellationToken cancellationToken) =>
                {
                    var country = specification.Evaluate(countries).FirstOrDefault();
                    return country;
                });

            mockCountryRepository.Setup(repo => repo.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                (Country country, CancellationToken cancellationToken) =>
                {
                    country.Id = countries.Count + 1;
                    countries.Add(country);
                    return country;
                });

            mockCountryRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>())).Callback(
                (Country country, CancellationToken cancellationToken) =>
                {
                    var countryToBeUpdated = countries.FirstOrDefault(c => c.Id == country.Id);
                    countryToBeUpdated = country;
                });

            mockCountryRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>())).Callback(
               (Country country, CancellationToken cancellationToken) =>
               {
                   countries.Remove(country);
               });

            return mockCountryRepository;
        }
    }
}
