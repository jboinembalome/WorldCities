using Ardalis.Specification;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Features.Countries.Specifications
{
    public class CountryIsDupeSpecification : Specification<Country>
    {
        public CountryIsDupeSpecification(string fieldName, string fieldValue, int countryId)
            : base()
        {
            switch (fieldName)
            {
                case "name":
                     Query.Where(
                        c => c.Name.ToLower() == fieldValue.ToLower() && c.Id != countryId);
                    break;
                case "iso2":
                    Query.Where(
                        c => c.ISO2.ToLower() == fieldValue.ToLower() && c.Id != countryId);
                    break;
                case "iso3":
                    Query.Where(
                        c => c.ISO3.ToLower() == fieldValue.ToLower() && c.Id != countryId);
                    break;
            }
        }
    }
}
