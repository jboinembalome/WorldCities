using MediatR;
using System.Text.Json.Serialization;

namespace WorldCities.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommand : IRequest<CreateCountryCommandResponse>
    {
        public string Name { get; set; }
        [JsonPropertyName("iso2")]
        public string ISO2 { get; set; }
        [JsonPropertyName("iso3")]
        public string ISO3 { get; set; }
    }
}
