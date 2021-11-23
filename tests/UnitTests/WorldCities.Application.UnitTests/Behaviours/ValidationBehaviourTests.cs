using AutoMapper;
using FluentAssertions;
using Moq;
using WorldCities.Application.Behaviours;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Features.Cities.Commands.UpdateCity;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Application.Profiles;
using WorldCities.Application.UnitTests.Mocks;
using WorldCities.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.UnitTests.Behaviours
{
    public class ValidationBehaviourTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<City, int>> _mockCityRepository;
        private readonly Mock<IAsyncRepository<Country, int>> _mockCountryRepository;

        public ValidationBehaviourTests()
        {
            _mockCityRepository = RepositoryMocks.GetCityRepository();
            _mockCountryRepository = RepositoryMocks.GetCountryRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }


        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public async Task Handle_EmptyName_ThrowsAnValidationException(string boardName)
        {
            var handler = new UpdateCityCommandHandler(_mapper, _mockCityRepository.Object, _mockCountryRepository.Object);
            var updateBoardCommand = new UpdateCityCommand { Name = boardName, Id = 1, Lat = 1, Lon= 1 };
            var validationBehavior = new ValidationBehaviour<UpdateCityCommand, MediatR.Unit>(new List<UpdateCityCommandValidator>()
            {
                new UpdateCityCommandValidator()
            });

            // Act
            Func<Task> action = async () =>
            {
                await validationBehavior.Handle(updateBoardCommand, new CancellationToken(), () =>
                {
                    return handler.Handle(updateBoardCommand, new CancellationToken());
                });
            };

            // Assert
            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
