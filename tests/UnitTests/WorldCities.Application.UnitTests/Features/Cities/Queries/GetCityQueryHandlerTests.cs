using AutoMapper;
using FluentAssertions;
using Moq;
using WorldCities.Application.Dto;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Features.Cities.Queries.GetCity;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Application.Profiles;
using WorldCities.Application.UnitTests.Mocks;
using WorldCities.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.UnitTests.Features.Boards.Queries
{
    public class GetCityQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<City, int>> _mockCityRepository;

        public GetCityQueryHandlerTests()
        {
            _mockCityRepository = RepositoryMocks.GetCityRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCityTest_ExistingCityId_ReturnsCity()
        {
            // Arrange
            var handler = new GetCityQueryHandler(_mapper, _mockCityRepository.Object);
            var getCityQuery = new GetCityQuery { CityId = 1 };

            // Act
            var result = await handler.Handle(getCityQuery, CancellationToken.None);

            // Assert
            result.Should().BeOfType<CityDto>();
            result.Name.Should().Be("Paris");
            result.Lat.Should().Be(1);
            result.Lon.Should().Be(1);
            result.CountryId.Should().Be(1);
            result.CountryName.Should().Be("France");
        }

        [Fact]
        public async Task GetCityTest_NoExistingCityId_ThrowsAnExceptionNotFound()
        {
            // Arrange
            var handler = new GetCityQueryHandler(_mapper, _mockCityRepository.Object);
            var getCityQuery = new GetCityQuery { CityId = 0 };

            // Act
            Func<Task> action = async () => { await handler.Handle(getCityQuery, CancellationToken.None); };

            // Assert
            await action.Should().ThrowAsync<NotFoundException>();
        }

    }
}
