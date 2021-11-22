using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorldCities.Application.Dto;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Features.Cities.Queries.GetCity;
using WorldCities.Application.Features.Cities.Specifications;
using WorldCities.Application.Features.Countries.Specifications;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Features.Countries.Queries.GetCountry
{
    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, CountryDto>
    {
        private readonly IAsyncRepository<Country, int> _countryRepository;
        private readonly IMapper _mapper;

        public GetCountryQueryHandler(IMapper mapper, IAsyncRepository<Country, int> countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<CountryDto> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var specification = new CountryWithCitiesSpec(request.Id);
            var country = await _countryRepository.FirstOrDefaultAsync(specification, cancellationToken);

            if (country == null)
                throw new NotFoundException(nameof(Country), request.Id);

            return _mapper.Map<CountryDto>(country);
        }
    }
}
