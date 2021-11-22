using MediatR;

namespace WorldCities.Application.Features.Countries.Commands.IsDupeCountry
{
    public class IsDupeCountryCommand : IRequest<IsDupeCountryCommandResponse>
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public int CountryId { get; set; }
    }
}
