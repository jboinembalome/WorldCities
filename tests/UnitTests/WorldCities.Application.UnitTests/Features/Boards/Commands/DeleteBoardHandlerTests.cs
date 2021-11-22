﻿using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Features.Boards.Commands.DeleteBoard;
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
    public class DeleteBoardHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Board, int>> _mockBoardRepository;

        public DeleteBoardHandlerTests()
        {
            _mockBoardRepository = RepositoryMocks.GetBoardRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task DeleteBoardTest_ExistingBoardId_BoardDeleted()
        {
            // Arrange
            var handler = new DeleteBoardHandler(_mapper, _mockBoardRepository.Object);
            var deleteBoardCommand = new DeleteCityCommand { BoardId = 1 };

            // Act
            var result = await handler.Handle(deleteBoardCommand, CancellationToken.None);
            var allBoards = await _mockBoardRepository.Object.ListAllAsync(CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            allBoards.Count.Should().Be(1);
        }

        [Fact]
        public async Task DeleteBoardTest_NoExistingBoardId_ThrowsAnExceptionNotFound()
        {
            // Arrange
            var handler = new DeleteBoardHandler(_mapper, _mockBoardRepository.Object);
            var deleteBoardCommand = new DeleteCityCommand { BoardId = 0 };

            // Act
            Func<Task> action = async () => { await handler.Handle(deleteBoardCommand, CancellationToken.None); };

            // Assert
            await action.Should().ThrowAsync<NotFoundException>();
        }
    }
}
