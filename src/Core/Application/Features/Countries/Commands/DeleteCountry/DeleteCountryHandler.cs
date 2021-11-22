using AutoMapper;
using MediatR;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace WorldCities.Application.Features.Countries.Commands.DeleteCountry
{
    public class DeleteCountryHandler : IRequestHandler<DeleteCountryCommand>
    {
        private readonly IAsyncRepository<Country, int> _countryRepository;
        private readonly IMapper _mapper;

        public DeleteCountryHandler(IMapper mapper, IAsyncRepository<Country, int> countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var countryToDelete = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (countryToDelete == null)
                throw new NotFoundException(nameof(Country), request.Id);

            await _countryRepository.DeleteAsync(countryToDelete, cancellationToken);

            return Unit.Value;
        }
    }
}
