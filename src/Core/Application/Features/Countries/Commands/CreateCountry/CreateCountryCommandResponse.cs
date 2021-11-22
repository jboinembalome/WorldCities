using WorldCities.Application.Dto;
using WorldCities.Application.Responses;

namespace WorldCities.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandResponse : BaseResponse
    {
        public CreateCountryCommandResponse() : base() { }

        public CountryDto Country { get; set; }
    }
}
