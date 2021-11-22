using MediatR;

namespace WorldCities.Application.Features.Cities.Commands.IsDupeCity
{
    public class IsDupeCityCommand : IRequest<IsDupeCityCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public int CountryId { get; set; }
    }
}
