using AutoMapper;
using System.Collections.Generic;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Profiles.Converters
{
    public class CityTypeConverter : ITypeConverter<IEnumerable<int>, IEnumerable<City>>
    {
        public IEnumerable<City> Convert(IEnumerable<int> cityIds, IEnumerable<City> cities, ResolutionContext context)
        {
            var citiesConvert = new List<City>();

            foreach (var id in cityIds)
                citiesConvert.Add(new City { Id = id });

            return citiesConvert;
        }
    }
}
