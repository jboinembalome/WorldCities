using Ardalis.Specification;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Features.Countries.Specifications
{
    public class CountryFilterSpecification : Specification<Country>
    {
        public CountryFilterSpecification(string filterQuery)
        {
            Query.Where(c => c.Name.ToLower().Contains(filterQuery.ToLower())
            || c.ISO2.ToLower().Contains(filterQuery.ToLower())
            || c.ISO3.ToLower().Contains(filterQuery.ToLower()));
        }
    }
}
