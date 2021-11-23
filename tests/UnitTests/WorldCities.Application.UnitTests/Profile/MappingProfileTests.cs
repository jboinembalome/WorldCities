using AutoMapper;
using FluentAssertions;
using WorldCities.Application.Dto;
using WorldCities.Application.Profiles;
using WorldCities.Domain.Entities;
using System;
using System.Runtime.Serialization;
using Xunit;
using WorldCities.Application.Features.Cities.Commands.CreateCity;

namespace WorldCities.Application.UnitTests.Profile
{
    public class MappingProfileTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void AssertConfigurationIsValid_HaveValidConfiguration()
        {
            // Act
            Action action = () => _configuration.AssertConfigurationIsValid();

            // Assert
            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(typeof(City), typeof(CityDto))]
        [InlineData(typeof(City), typeof(CreateCityCommand))]
        public void Map_SupportMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = GetInstanceOf(source);

            // Act
            Action action = () => _mapper.Map(instance, source, destination);

            // Assert
            action.Should().NotThrow();
        }

        private static object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }

}
