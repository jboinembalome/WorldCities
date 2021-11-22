using AutoMapper;
using MediatR;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace WorldCities.Application.Features.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand>
    {
        private readonly IAsyncRepository<City, int> _cityRepository;
        private readonly IAsyncRepository<Country, int> _countryRepository;

        private readonly IMapper _mapper;

        public UpdateCityCommandHandler(IMapper mapper, IAsyncRepository<City, int> cityRepository, IAsyncRepository<Country, int> countryRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public async Task<Unit> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var cityToUpdate = await _cityRepository.GetByIdAsync(request.Id, cancellationToken);
            var country = await _countryRepository.GetByIdAsync(request.CountryId, cancellationToken);

            if (cityToUpdate == null)
                throw new NotFoundException(nameof(City), request.Id);

            _mapper.Map(request, cityToUpdate, typeof(UpdateCityCommand), typeof(City));
            cityToUpdate.Country = country;

            await _cityRepository.UpdateAsync(cityToUpdate, cancellationToken);

            return Unit.Value;
        }
    }
}
