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
using WorldCities.Application.Features.Cities.Specifications;

namespace WorldCities.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IPagedList<CityDto>>
    {
        private readonly IAsyncRepository<City, int> _cityRepository;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IMapper mapper, IAsyncRepository<City, int> cityRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        public async Task<IPagedList<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var filterSpecification = new CityFilterSpecification(request.FilterQuery);
            var filterOrderingPaginatedSpecification = new CityFilterOrderingPaginatedSpecification(request.FilterQuery, request.SortColumn, request.SortOrder, request.PageSize * request.Page, request.PageSize);

            var itemsOnPage = await _cityRepository.ListAsync(filterOrderingPaginatedSpecification, cancellationToken);
            var totalItems = await _cityRepository.CountAsync(filterSpecification, cancellationToken);

            var citiesDto = _mapper.Map<IEnumerable<CityDto>>(itemsOnPage);

            return citiesDto.ToPagedList(request.Page, request.PageSize, totalItems);
        }
    }
}
