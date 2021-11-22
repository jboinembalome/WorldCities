using AutoMapper;
using System.Linq;
using WorldCities.Application.Dto;
using WorldCities.Application.Features.Cities.Commands.CreateCity;
using WorldCities.Application.Features.Cities.Commands.UpdateCity;
using WorldCities.Application.Features.Countries.Commands.CreateCountry;
using WorldCities.Application.Features.Countries.Commands.UpdateCountry;
using WorldCities.Domain.Entities;

namespace WorldCities.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region City

            CreateMap<City, CityDto>()
                .ForMember(d => d.CountryId, opt => opt.MapFrom(c => c.Country.Id))
                .ForMember(d => d.CountryName, opt => opt.MapFrom(c => c.Country.Name));
            CreateMap<City, CreateCityCommand>()
                .ReverseMap();
            CreateMap<City, UpdateCityCommand>()
                .ReverseMap();
            #endregion

            #region Country

            CreateMap<Country, CountryDto>()
                .ForMember(d => d.TotCities, opt => opt.MapFrom(c => c.Cities.Count));
            CreateMap<Country, CreateCountryCommand>()
               .ReverseMap();
            CreateMap<Country, UpdateCountryCommand>()
                .ReverseMap();
            #endregion
        }

    }
}
