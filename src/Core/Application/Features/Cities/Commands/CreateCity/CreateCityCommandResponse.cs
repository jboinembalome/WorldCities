using WorldCities.Application.Dto;
using WorldCities.Application.Responses;

namespace WorldCities.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandResponse : BaseResponse
    {
        public CreateCityCommandResponse() : base() { }

        public CityDto City { get; set; }
    }
}
