using Ardalis.Specification;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Features.Countries.Specifications
{
    public class CountryWithCitiesSpec : Specification<Country>, ISingleResultSpecification
    {
        public CountryWithCitiesSpec(int cityId)
        {
            Query.Where(c => c.Id == cityId);
            Query.Include(c => c.Cities);
        }
    }
}
