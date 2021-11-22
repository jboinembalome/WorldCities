using WorldCities.Application.Responses;

namespace WorldCities.Application.Features.Cities.Commands.IsDupeCity
{
    public class IsDupeCityCommandResponse : BaseResponse
    {
        public IsDupeCityCommandResponse() : base() { }

        public bool IsDupe { get; set; }
    }
}
