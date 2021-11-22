using MediatR;

namespace WorldCities.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommand : IRequest
    {
        public int Id { get; set; }
    }
}
