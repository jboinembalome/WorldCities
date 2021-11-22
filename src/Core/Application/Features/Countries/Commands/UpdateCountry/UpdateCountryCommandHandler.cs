using AutoMapper;
using MediatR;
using WorldCities.Application.Exceptions;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace WorldCities.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand>
    {
        private readonly IAsyncRepository<Country, int> _countryRepository;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IMapper mapper, IAsyncRepository<Country, int> countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var countryToUpdate = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (countryToUpdate == null)
                throw new NotFoundException(nameof(Country), request.Id);

            _mapper.Map(request, countryToUpdate, typeof(UpdateCountryCommand), typeof(Country));

            await _countryRepository.UpdateAsync(countryToUpdate, cancellationToken);

            return Unit.Value;
        }
    }
}
