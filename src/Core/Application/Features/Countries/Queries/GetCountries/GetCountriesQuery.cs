using MediatR;
using WorldCities.Application.Dto;
using WorldCities.Application.Interfaces.Common;

namespace WorldCities.Application.Features.Countries.Queries.GetCountries
{
    public class GetCountriesQuery : IRequest<IPagedList<CountryDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string FilterQuery { get; set; }
    }
}
