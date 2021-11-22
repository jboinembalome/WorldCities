using AutoMapper;
using MediatR;
using WorldCities.Application.Dto;
using WorldCities.Application.Interfaces.Persistence;
using WorldCities.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorldCities.Application.Extensions;
using WorldCities.Application.Interfaces.Common;
using WorldCities.Application.Features.Countries.Specifications;

namespace WorldCities.Application.Features.Countries.Queries.GetCountries
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IPagedList<CountryDto>>
    {
        private readonly IAsyncRepository<Country, int> _countryRepository;
        private readonly IMapper _mapper;

        public GetCountriesQueryHandler(IMapper mapper, IAsyncRepository<Country, int> countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<IPagedList<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var filterSpecification = new CountryFilterSpecification(request.FilterQuery);
            var filterOrderingPaginatedSpecification = new CountryFilterOrderingPaginatedSpecification(request.FilterQuery, request.SortColumn, request.SortOrder, request.PageSize * request.PageIndex, request.PageSize);

            var itemsOnPage = await _countryRepository.ListAsync(filterOrderingPaginatedSpecification, cancellationToken);
            var totalItems = await _countryRepository.CountAsync(filterSpecification, cancellationToken);

            var countriesDto = _mapper.Map<IEnumerable<CountryDto>>(itemsOnPage);

            return countriesDto.ToPagedList(request.PageIndex, request.PageSize, totalItems);
        }
    }
}
