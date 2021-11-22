using Ardalis.Specification;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Features.Cities.Specifications
{
    public class CityFilterSpecification : Specification<City>
    {
        public CityFilterSpecification(string filterQuery)
        {
            Query.Where(c => c.Name.ToLower().Contains(filterQuery.ToLower()) 
            || c.Name_ASCII.ToLower().Contains(filterQuery.ToLower()));
        }
    }
}
