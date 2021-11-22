using AutoMapper;
using MediatR;
using WorldCities.Application.Dto;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Features.Cities.Specifications;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace WorldCities.Application.Features.Cities.Queries.GetCity
{
    public class GetCityQueryHandler : IRequestHandler<GetCityQuery, CityDto>
    {
        private readonly IAsyncRepository<City, int> _cityRepository;
        private readonly IMapper _mapper;

        public GetCityQueryHandler(IMapper mapper, IAsyncRepository<City, int> cityRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        public async Task<CityDto> Handle(GetCityQuery request, CancellationToken cancellationToken)
        {
            var specification = new CityWithCountrySpec(request.CityId);
            var city = await _cityRepository.FirstOrDefaultAsync(specification, cancellationToken);

            if (city == null)
                throw new NotFoundException(nameof(City), request.CityId);

            return _mapper.Map<CityDto>(city);
        }
    }
}
