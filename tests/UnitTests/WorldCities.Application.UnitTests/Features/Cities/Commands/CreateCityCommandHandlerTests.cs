using AutoMapper;
using FluentAssertions;
using Moq;
using WorldCities.Application.Features.Cities.Commands.CreateCity;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Application.Profiles;
using WorldCities.Application.UnitTests.Mocks;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.UnitTests.Features.Boards.Commands
{
    public class CreateCityCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<City, int>> _mockCityRepository;
        private readonly Mock<IAsyncRepository<Country, int>> _mockCountryRepository;

        public CreateCityCommandHandlerTests()
        {
            _mockCityRepository = RepositoryMocks.GetCityRepository();
            _mockCountryRepository = RepositoryMocks.GetCountryRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task CreateCityTest_ValidCIty_CityAdded()
        {
            // Arrange
            var handler = new CreateCityCommandHandler(_mapper, _mockCityRepository.Object, _mockCountryRepository.Object);
            var createCityCommand = new CreateCityCommand { Name = "New city", Lat = 4, Lon = 4, CountryId = 1 };

            // Act
            var result = await handler.Handle(createCityCommand, CancellationToken.None);
            var allCities = await _mockCityRepository.Object.ListAllAsync(CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            allCities.Count.Should().Be(4);
            allCities[allCities.Count - 1].Name.Should().Be(result.City.Name);
        }
    }
}
