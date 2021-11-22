using MediatR;

namespace WorldCities.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<CreateCityCommandResponse>
    {
        public string Name { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public int CountryId { get; set; }
    }
}
