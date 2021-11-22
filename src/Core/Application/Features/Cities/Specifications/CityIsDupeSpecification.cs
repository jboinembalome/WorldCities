using Ardalis.Specification;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Features.Cities.Specifications
{
    public class CityIsDupeSpecification : Specification<City>
    {
        public CityIsDupeSpecification(int cityId, string name, decimal lat, decimal lon, int countryId)
            : base()
        {
            Query.Include(c => c.Country)
                .Where(c => c.Id != cityId && c.Name == name && c.Lat == lat
                && c.Lon == lon && c.Country.Id == countryId);
        }
    }
}
