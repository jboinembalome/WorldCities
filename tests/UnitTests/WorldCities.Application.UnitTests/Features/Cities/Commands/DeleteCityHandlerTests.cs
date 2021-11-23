using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Features.Cities.Commands.DeleteCity;
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
    public class DeleteCityHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<City, int>> _mockCityRepository;

        public DeleteCityHandlerTests()
        {
            _mockCityRepository = RepositoryMocks.GetCityRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task DeleteCityTest_ExistingCityId_CityDeleted()
        {
            // Arrange
            var handler = new DeleteCityHandler(_mapper, _mockCityRepository.Object);
            var deleteCityCommand = new DeleteCityCommand { Id = 1 };

            // Act
            var result = await handler.Handle(deleteCityCommand, CancellationToken.None);
            var allCities = await _mockCityRepository.Object.ListAllAsync(CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            allCities.Count.Should().Be(2);
        }

        [Fact]
        public async Task DeleteCityTest_NoExistingCityId_ThrowsAnExceptionNotFound()
        {
            // Arrange
            var handler = new DeleteCityHandler(_mapper, _mockCityRepository.Object);
            var deleteCityCommand = new DeleteCityCommand { Id = 0 };

            // Act
            Func<Task> action = async () => { await handler.Handle(deleteCityCommand, CancellationToken.None); };

            // Assert
            await action.Should().ThrowAsync<NotFoundException>();
        }
    }
}
