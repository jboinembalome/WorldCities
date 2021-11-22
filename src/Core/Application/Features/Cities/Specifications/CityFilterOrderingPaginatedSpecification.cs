using Ardalis.Specification;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Features.Cities.Specifications
{
    public class CityFilterOrderingPaginatedSpecification : Specification<City>
    {
        public CityFilterOrderingPaginatedSpecification(string filterQuery, string sortColumn, string sortOrder, int skip, int take)
            : base()
        {
            Query.Where(c => c.Name.ToLower().Contains(filterQuery.ToLower())
            || c.Name_ASCII.ToLower().Contains(filterQuery.ToLower()));

            Query.Include(c => c.Country);

            OrderBy(sortColumn, sortOrder);

            Query.Skip(skip).Take(take);
        }

        private IOrderedSpecificationBuilder<City> OrderBy(string sortColumn, string sortOrder)
        {
            return sortOrder == "asc" ? OrderByAsc(sortColumn) : OrderByDesc(sortColumn);
        }

        private IOrderedSpecificationBuilder<City> OrderByAsc(string sortColumn)
        {
            return sortColumn.ToLower() switch
            {
                "name" => Query.OrderBy(c => c.Name),
                "name_ascii" => Query.OrderBy(c => c.Name_ASCII),
                "lat" => Query.OrderBy(c => c.Lat),
                "lon" => Query.OrderBy(c => c.Lon),
                "countryname" => Query.OrderBy(c => c.Country.Name),
                _ => Query.OrderBy(c => c.Name),
            };
        }

        private IOrderedSpecificationBuilder<City> OrderByDesc(string sortColumn)
        {
            return sortColumn.ToLower() switch
            {
                "name" => Query.OrderByDescending(c => c.Name),
                "name_ascii" => Query.OrderByDescending(c => c.Name_ASCII),
                "lat" => Query.OrderByDescending(c => c.Lat),
                "lon" => Query.OrderByDescending(c => c.Lon),
                "countryname" => Query.OrderByDescending(c => c.Country.Name),
                _ => Query.OrderByDescending(c => c.Name),
            };
        }
    }
}
