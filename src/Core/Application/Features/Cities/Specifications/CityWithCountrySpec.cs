using Ardalis.Specification;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Features.Cities.Specifications
{
    public class CityWithCountrySpec : Specification<City>, ISingleResultSpecification
    {
        public CityWithCountrySpec(int cityId)
        {
            Query.Where(c => c.Id == cityId);
            Query.Include(c => c.Country);
        }
    }
}
