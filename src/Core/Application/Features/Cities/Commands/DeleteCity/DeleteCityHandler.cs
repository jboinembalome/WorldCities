using AutoMapper;
using MediatR;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace WorldCities.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly IAsyncRepository<City, int> _cityRepository;
        private readonly IMapper _mapper;

        public DeleteCityHandler(IMapper mapper, IAsyncRepository<City, int> cityRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var cityToDelete = await _cityRepository.GetByIdAsync(request.Id, cancellationToken);

            if (cityToDelete == null)
                throw new NotFoundException(nameof(City), request.Id);

            await _cityRepository.DeleteAsync(cityToDelete, cancellationToken);

            return Unit.Value;
        }
    }
}
