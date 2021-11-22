using MediatR;
using WorldCities.Application.Dto;

namespace WorldCities.Application.Features.Countries.Queries.GetCountry
{
    public class GetCountryQuery : IRequest<CountryDto>
    {
        public int Id { get; set; }
    }
}
