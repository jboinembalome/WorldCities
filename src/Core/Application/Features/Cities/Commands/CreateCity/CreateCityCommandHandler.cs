using AutoMapper;
using MediatR;
using WorldCities.Application.Dto;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace WorldCities.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CreateCityCommandResponse>
    {
        private readonly IAsyncRepository<City, int> _cityRepository;
        private readonly IAsyncRepository<Country, int> _countryRepository;

        private readonly IMapper _mapper;

        public CreateCityCommandHandler(IMapper mapper, IAsyncRepository<City, int> cityRepository, IAsyncRepository<Country, int> countryRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public async Task<CreateCityCommandResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var createCityCommandResponse = new CreateCityCommandResponse();
            var country = await _countryRepository.GetByIdAsync(request.CountryId, cancellationToken);

            var city = _mapper.Map<City>(request);
            city.Country = country;
            city = await _cityRepository.AddAsync(city, cancellationToken);

            createCityCommandResponse.City = _mapper.Map<CityDto>(city);

            return createCityCommandResponse;
        }
    }
}
