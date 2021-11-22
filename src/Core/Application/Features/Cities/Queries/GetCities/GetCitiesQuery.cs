using MediatR;
using WorldCities.Application.Dto;
using WorldCities.Application.Interfaces.Common;

namespace WorldCities.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesQuery : IRequest<IPagedList<CityDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string FilterQuery { get; set; }
    }
}
