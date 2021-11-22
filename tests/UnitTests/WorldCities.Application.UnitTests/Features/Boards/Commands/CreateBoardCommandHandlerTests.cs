﻿using AutoMapper;
using FluentAssertions;
using Moq;
using WorldCities.Application.Features.Boards.Commands.CreateBoard;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Application.Profiles;
using WorldCities.Application.UnitTests.Mocks;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.UnitTests.Features.Boards.Commands
{
    public class CreateBoardCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Board, int>> _mockBoardRepository;
        private readonly Mock<IAsyncRepository<Adherent, int>> _mockAdherentRepository;

        public CreateBoardCommandHandlerTests()
        {
            _mockBoardRepository = RepositoryMocks.GetBoardRepository();
            _mockAdherentRepository = RepositoryMocks.GetAdherentRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task CreateBoardTest_ValidBoard_BoardAdded()
        {
            // Arrange
            var handler = new CreateBoardCommandHandler(_mapper, _mockBoardRepository.Object, _mockAdherentRepository.Object);
            var createBoardCommand = new CreateBoardCommand { UserId = "2cd08f87-33a6-4cbc-a0de-71d428986b85" };

            // Act
            var result = await handler.Handle(createBoardCommand, CancellationToken.None);
            var allBoards = await _mockBoardRepository.Object.ListAllAsync(CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            allBoards.Count.Should().Be(3);
            allBoards[allBoards.Count - 1].Name.Should().Be(result.Board.Name);
        }
    }
}