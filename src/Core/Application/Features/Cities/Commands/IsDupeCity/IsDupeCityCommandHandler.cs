using AutoMapper;
using MediatR;
using WorldCities.Application.Dto;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using WorldCities.Application.Features.Cities.Specifications;

namespace WorldCities.Application.Features.Cities.Commands.IsDupeCity
{
    public class IsDupeCityCommandHandler : IRequestHandler<IsDupeCityCommand, IsDupeCityCommandResponse>
    {
        private readonly IAsyncRepository<City, int> _cityRepository;

        public IsDupeCityCommandHandler(IAsyncRepository<City, int> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IsDupeCityCommandResponse> Handle(IsDupeCityCommand request, CancellationToken cancellationToken)
        {
            var isDupeCityCommandResponse = new IsDupeCityCommandResponse();

            var isDupeSpecification = new CityIsDupeSpecification(request.Id, request.Name, request.Lat, request.Lon, request.CountryId);

            var isDupe = await _cityRepository.AnyAsync(isDupeSpecification, cancellationToken);
            isDupeCityCommandResponse.IsDupe = isDupe;

            return isDupeCityCommandResponse;
        }
    }
}
