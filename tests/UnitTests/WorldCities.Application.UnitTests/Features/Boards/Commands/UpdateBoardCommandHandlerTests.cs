using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Features.Boards.Commands.UpdateBoard;
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
    public class UpdateBoardCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Board, int>> _mockBoardRepository;

        public UpdateBoardCommandHandlerTests()
        {
            _mockBoardRepository = RepositoryMocks.GetBoardRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task UpdateBoardTest_ExistingBoardId_BoardUpdated()
        {
            // Arrange
            var handler = new UpdateBoardCommandHandler(_mapper, _mockBoardRepository.Object);
            var updateBoardCommand = new UpdateCityCommand { Name = "My new name", BoardId = 1 };

            // Act
            var result = await handler.Handle(updateBoardCommand, CancellationToken.None);
            var updatedBoard = await _mockBoardRepository.Object.GetByIdAsync(1, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            updatedBoard.Name.Should().Be("My new name");
        }

        [Fact]
        public async Task UpdateBoardTest_NoExistingBoardId_ThrowsAnExceptionNotFound()
        {
            // Arrange
            var handler = new UpdateBoardCommandHandler(_mapper, _mockBoardRepository.Object);
            var updateBoardCommand = new UpdateCityCommand { Name = "My new name", BoardId = 0 };

            // Act
            Func<Task> action = async () => { await handler.Handle(updateBoardCommand, CancellationToken.None); };

            // Assert
            await action.Should().ThrowAsync<NotFoundException>();
        }
    }
}
