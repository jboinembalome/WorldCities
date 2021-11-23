using Moq;
using WorldCities.Application.Behaviours;
using WorldCities.Application.Features.Cities.Commands.CreateCity;
using WorldCities.Application.Interfaces.Common;
using WorldCities.Application.Interfaces.Identity;
using WorldCities.Application.Interfaces.Logging;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WorldCities.Application.UnitTests.Behaviours
{
    public class LoggingBehaviourTests
    {
        private readonly Mock<IAppLogger<CreateCityCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;

        public LoggingBehaviourTests()
        {
            _logger = new Mock<IAppLogger<CreateCityCommand>>();
            _currentUserService = new Mock<ICurrentUserService>();
            _identityService = new Mock<IIdentityService>();
        }

        [Fact]
        public async Task Process_CallGetUserNameAsyncOnceIfAuthenticated()
        {
            _currentUserService.Setup(x => x.UserId).Returns("43a19f1f-df2e-418f-bc4e-e0e98c792beb");

            var loggingBehaviour = new LoggingBehaviour<CreateCityCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await loggingBehaviour.Process(new CreateCityCommand { Name = "New city", Lat = 4, Lon = 4, CountryId = 1 }, new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Process_NotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var requestLogger = new LoggingBehaviour<CreateCityCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await requestLogger.Process(new CreateCityCommand(), new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        }

    }
}
