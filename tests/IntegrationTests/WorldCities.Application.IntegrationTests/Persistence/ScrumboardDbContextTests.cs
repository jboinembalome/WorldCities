
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using WorldCities.Application.Interfaces.Common;
using WorldCities.Domain.Entities;
using WorldCities.Infrastructure.Persistence;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.IntegrationTests.Persistence
{
    [Collection("Database collection")]
    public class WorldCitiesDbContextTests : IAsyncLifetime
    {
        private readonly DatabaseFixture _database;

        public WorldCitiesDbContextTests(DatabaseFixture database)
        {
            _database = database;
        }

        public async Task DisposeAsync() => await DatabaseFixture.ResetState();

        public Task InitializeAsync() => Task.CompletedTask;

        [Fact]
        public async void SaveChangesAsync_SetCreatedByProperty()
        {
            // Arrange
            var _currentUserService = "00000000-0000-0000-0000-000000000000"; // Value of the current user in DatabaseFixture
            var testBoardName = "testBoard";
            var board = new Board { Name = testBoardName };

            _database.SetDbContext();

            // Act
            _database.DbContext.Boards.Add(board);
            await _database.DbContext.SaveChangesAsync();

            // Assert
            board.CreatedBy.Should().Be(_currentUserService);
        }
    }

}
