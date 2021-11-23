using AutoMapper;
using FluentAssertions;
using Moq;
using WorldCities.Application.Dto;
using WorldCities.Application.Features.Cities.Queries.GetCities;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Application.Profiles;
using WorldCities.Application.UnitTests.Mocks;
using WorldCities.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using WorldCities.Application.Interfaces.Common;
using System.Linq;

namespace WorldCities.Application.UnitTests.Features.Boards.Queries
{
    public class GetCitiesQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<City, int>> _mockCityRepository;

        public GetCitiesQueryHandlerTests()
        {
            _mockCityRepository = RepositoryMocks.GetCityRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCitiesTest_ReturnsCities()
        {
            // Arrange
            var handler = new GetCitiesQueryHandler(_mapper, _mockCityRepository.Object);
            var getCitiesQuery = new GetCitiesQuery 
            { 
                Page = 0, 
                PageSize = 10, 
                FilterQuery = string.Empty,
                SortColumn = "name",
                SortOrder = "asc"
            };

            // Act
            var result = await handler.Handle(getCitiesQuery, CancellationToken.None);

            // Assert
            result.Should().BeAssignableTo<IPagedList<CityDto>>();
            result.Should().NotBeNull();
            result.Source.Should().HaveCount(3);
            result.Source.First().Name.Should().Be("Leipzig");
            result.Source.Last().Name.Should().Be("Paris");
        }

        [Fact]
        public async Task GetCitiesTest_NoExistingName_ReturnsZeroCity()
        {
            // Arrange
            var handler = new GetCitiesQueryHandler(_mapper, _mockCityRepository.Object);
            var getCitiesQuery = new GetCitiesQuery
            {
                Page = 0,
                PageSize = 10,
                FilterQuery = "LIIIIIL",
                SortColumn = "name",
                SortOrder = "asc"
            };

            // Act
            var result = await handler.Handle(getCitiesQuery, CancellationToken.None);

            // Assert
            result.Should().BeAssignableTo<IPagedList<CityDto>>();
            result.Source.Should().BeEmpty();
        }
    }
}
