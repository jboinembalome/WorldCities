using WorldCities.Application.Responses;

namespace WorldCities.Application.Features.Countries.Commands.IsDupeCountry
{
    public class IsDupeCountryCommandResponse : BaseResponse
    {
        public IsDupeCountryCommandResponse() : base() { }

        public bool IsDupe { get; set; }
    }
}
