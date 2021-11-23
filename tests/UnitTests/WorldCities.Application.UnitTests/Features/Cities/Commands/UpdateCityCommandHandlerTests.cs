using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Features.Cities.Commands.UpdateCity;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Application.Profiles;
using WorldCities.Application.UnitTests.Mocks;
using WorldCities.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.UnitTests.Features.Boards.Commands
{
    public class UpdateCityCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<City, int>> _mockCityRepository;
        private readonly Mock<IAsyncRepository<Country, int>> _mockCountryRepository;

        public UpdateCityCommandHandlerTests()
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
        public async Task UpdateCityTest_ExistingCityId_CityUpdated()
        {
            // Arrange
            var handler = new UpdateCityCommandHandler(_mapper, _mockCityRepository.Object, _mockCountryRepository.Object);
            var updateCityCommand = new UpdateCityCommand { Name = "My new name", Id = 1 };

            // Act
            var result = await handler.Handle(updateCityCommand, CancellationToken.None);
            var updatedCity = await _mockCityRepository.Object.GetByIdAsync(1, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            updatedCity.Name.Should().Be("My new name");
        }

        [Fact]
        public async Task UpdateCityTest_NoExistingCityId_ThrowsAnExceptionNotFound()
        {
            // Arrange
            var handler = new UpdateCityCommandHandler(_mapper, _mockCityRepository.Object, _mockCountryRepository.Object);
            var updateCityCommand = new UpdateCityCommand { Name = "My new name", Id = 0 };

            // Act
            Func<Task> action = async () => { await handler.Handle(updateCityCommand, CancellationToken.None); };

            // Assert
            await action.Should().ThrowAsync<NotFoundException>();
        }
    }
}
