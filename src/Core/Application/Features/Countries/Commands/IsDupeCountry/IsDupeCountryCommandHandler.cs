using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorldCities.Application.Dto;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;

using WorldCities.Application.Features.Countries.Specifications;

namespace WorldCities.Application.Features.Countries.Commands.IsDupeCountry
{
    public class IsDupeCountryCommandHandler : IRequestHandler<IsDupeCountryCommand, IsDupeCountryCommandResponse>
    {
        private readonly IAsyncRepository<Country, int> _countryRepository;

        public IsDupeCountryCommandHandler(IAsyncRepository<Country, int> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IsDupeCountryCommandResponse> Handle(IsDupeCountryCommand request, CancellationToken cancellationToken)
        {
            var isDupeCountryCommandResponse = new IsDupeCountryCommandResponse();
            var isDupe = false;

            var fieldName = request.FieldName.ToLower();
            if (fieldName == "name" || fieldName == "iso2" || fieldName == "iso3")
            {
                var isDupeSpecification = new CountryIsDupeSpecification(request.FieldName, request.FieldValue, request.CountryId);
                isDupe = await _countryRepository.AnyAsync(isDupeSpecification, cancellationToken);
            }

            isDupeCountryCommandResponse.IsDupe = isDupe;

            return isDupeCountryCommandResponse;
        }
    }
}
