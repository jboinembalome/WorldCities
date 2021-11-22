using Ardalis.Specification;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Features.Countries.Specifications
{
    public class CountryFilterOrderingPaginatedSpecification : Specification<Country>
    {
        public CountryFilterOrderingPaginatedSpecification(string filterQuery, string sortColumn, string sortOrder, int skip, int take)
            : base()
        {
            Query.Where(c => c.Name.ToLower().Contains(filterQuery.ToLower())
            || c.ISO2.ToLower().Contains(filterQuery.ToLower())
            || c.ISO3.ToLower().Contains(filterQuery.ToLower()));

            Query.Include(c => c.Cities);

            OrderBy(sortColumn, sortOrder);

            Query.Skip(skip).Take(take);
        }

        private IOrderedSpecificationBuilder<Country> OrderBy(string sortColumn, string sortOrder)
        {
            return sortOrder == "asc" ? OrderByAsc(sortColumn) : OrderByDesc(sortColumn);
        }

        private IOrderedSpecificationBuilder<Country> OrderByAsc(string sortColumn)
        {
            return sortColumn.ToLower() switch
            {
                "name" => Query.OrderBy(c => c.Name),
                "iso2" => Query.OrderBy(c => c.ISO2),
                "iso3" => Query.OrderBy(c => c.ISO3),
                _ => Query.OrderBy(c => c.Name),
            };
        }

        private IOrderedSpecificationBuilder<Country> OrderByDesc(string sortColumn)
        {
            return sortColumn.ToLower() switch
            {
                "name" => Query.OrderByDescending(c => c.Name),
                "iso2" => Query.OrderByDescending(c => c.ISO2),
                "iso3" => Query.OrderByDescending(c => c.ISO3),
                _ => Query.OrderByDescending(c => c.Name),
            };
        }
    }
}
