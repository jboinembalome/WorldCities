using MediatR;
using WorldCities.Application.Dto;

namespace WorldCities.Application.Features.Cities.Queries.GetCity
{
    public class GetCityQuery : IRequest<CityDto>
    {
        public int CityId { get; set; }
    }
}
