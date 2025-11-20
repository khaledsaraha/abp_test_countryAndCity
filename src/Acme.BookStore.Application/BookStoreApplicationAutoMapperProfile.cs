using Acme.BookStore.Cities;
using Acme.BookStore.Countries;
using AutoMapper;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Country mappings
        CreateMap<Country, CountryDto>();
        CreateMap<CreateCountryDto, Country>();
        CreateMap<UpdateCountryDto, Country>();

        // City mappings
        CreateMap<City, CityDto>();
        CreateMap<CreateCityDto, City>();
        CreateMap<UpdateCityDto, City>();
    }
}
