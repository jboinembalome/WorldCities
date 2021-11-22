using AutoMapper;
using MediatR;
using WorldCities.Application.Dto;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace WorldCities.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CreateCountryCommandResponse>
    {
        private readonly IAsyncRepository<Country, int> _countryRepository;
        private readonly IMapper _mapper;

        public CreateCountryCommandHandler(IMapper mapper, IAsyncRepository<Country, int> countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<CreateCountryCommandResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var createCountryCommandResponse = new CreateCountryCommandResponse();
            var country = _mapper.Map<Country>(request);

            country = await _countryRepository.AddAsync(country, cancellationToken);

            createCountryCommandResponse.Country = _mapper.Map<CountryDto>(country);

            return createCountryCommandResponse;
        }
    }
}
