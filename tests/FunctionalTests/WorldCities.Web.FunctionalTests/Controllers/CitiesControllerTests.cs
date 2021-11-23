using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WorldCities.Application.Dto;
using WorldCities.Application.Features.Cities.Commands.CreateCity;
using WorldCities.Application.Features.Cities.Commands.UpdateCity;
using WorldCities.Web.FunctionalTests.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WorldCities.Application.Interfaces.Common;

namespace WorldCities.Web.FunctionalTests.Controllers
{
    public class CitiesControllerTests : IClassFixture<CustomWebApplicationFactoryFixture<Startup>>
    {
        private readonly CustomWebApplicationFactoryFixture<Startup> _factory;

        public CitiesControllerTests(CustomWebApplicationFactoryFixture<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/cities")]
        [InlineData("/api/cities/1")]
        public async Task Get_EndpointsReturnUnauthorizedToAnonymousUserForRestrictedUrls(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetCities_ReturnsSuccessResult()
        {
            // Arrange
            var provider = TestClaimsProvider.WithAdherentClaims();
            var client = _factory.CreateClientWithTestAuth(provider);

            // Act
            var response = await client.GetAsync("/api/cities");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IPagedList<CityDto>>(responseString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().BeAssignableTo<IPagedList<CityDto>>();
            result.Should().NotBeNull();
            result.Source.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetCity_ReturnsSuccessResult()
        {
            // Arrange
            var provider = TestClaimsProvider.WithAdherentClaims();
            var client = _factory.CreateClientWithTestAuth(provider);
            var id = 1;

            // Act
            var response = await client.GetAsync($"/api/cities/{id}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CityDto>(responseString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().BeOfType<CityDto>();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_ReturnsNotFoundGivenInvalidId()
        {
            // Arrange
            var provider = TestClaimsProvider.WithAdherentClaims();
            var client = _factory.CreateClientWithTestAuth(provider);
            var id = 0;

            // Act
            var response = await client.GetAsync($"/api/cities/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateCity_ReturnsSuccessResult()
        {
            // Arrange
            var provider = TestClaimsProvider.WithAdherentClaims();
            var client = _factory.CreateClientWithTestAuth(provider);
            var request = new CreateCityCommand()
            {
                Name = "Lille",
                Lat = 11.443m,
                Lon = 10.043m,
                CountryId = 1
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/cities", jsonContent);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var city = JsonConvert.DeserializeObject<CreateCityCommandResponse>(responseString);

            // Assert
            city.City.Should().NotBeNull();
            city.City.Id.Should().BePositive();
            city.City.Name.Should().Be("Lille");
        }

        [Fact]
        public async Task UpdateCity_ReturnsNoContent()
        {
            // Arrange
            var provider = TestClaimsProvider.WithAdherentClaims();
            var client = _factory.CreateClientWithTestAuth(provider);
            var id = 2;
            var request = new UpdateCityCommand()
            {
                Id = id,
                Name = "Updated board name",
                Lon = 1,
                Lat = 2,
                CountryId = 1
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync($"api/cities/{id}", jsonContent);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task UpdateCity_ReturnsBadRequest()
        {
            // Arrange
            var provider = TestClaimsProvider.WithAdherentClaims();
            var client = _factory.CreateClientWithTestAuth(provider);
            var id = 2;
            var badId = 1;
            var request = new UpdateCityCommand()
            {
                Id = badId,
                Name = "Updated board name",
                Lon = 1,
                Lat = 2,
                CountryId = 1
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync($"api/cities/{id}", jsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteCity_ReturnsNoContent()
        {
            // Arrange
            var provider = TestClaimsProvider.WithAdherentClaims();
            var client = _factory.CreateClientWithTestAuth(provider);
            var id = 3;

            // Act
            var response = await client.DeleteAsync($"api/cities/{id}");
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteCity_ReturnsNotFoundGivenInvalidId()
        {
            // Arrange
            var provider = TestClaimsProvider.WithAdherentClaims();
            var client = _factory.CreateClientWithTestAuth(provider);
            var id = 0;

            // Act
            var response = await client.DeleteAsync($"api/cities/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
