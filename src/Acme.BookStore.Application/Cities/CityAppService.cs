using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Cities;
using Acme.BookStore.Countries;
using Acme.BookStore.Localization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Cities;

/// <summary>
/// Application service implementation for City operations
/// </summary>
[Authorize]
public class CityAppService : BookStoreAppService, ICityAppService
{
    private readonly ICityRepository _cityRepository;
    private readonly IRepository<Country, Guid> _countryRepository;

    public CityAppService(
        ICityRepository cityRepository,
        IRepository<Country, Guid> countryRepository)
    {
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
    }

    public virtual async Task<CityDto> CreateAsync(CreateCityDto input)
    {
        // Validate country exists
        var countryExists = await _countryRepository.AnyAsync(x => x.Id == input.CountryId);
        if (!countryExists)
        {
            throw new UserFriendlyException(
                L["City:CountryNotFound"],
                code: BookStoreDomainErrorCodes.CityCountryNotFound);
        }

        // Check if city name already exists in the country
        var nameExists = await _cityRepository.IsNameExistsAsync(input.NameAr, input.NameEn, input.CountryId);
        if (nameExists)
        {
            throw new UserFriendlyException(
                L["City:NameAlreadyExists", input.NameAr, input.NameEn],
                code: BookStoreDomainErrorCodes.CityNameAlreadyExists);
        }

        var city = new City(
            GuidGenerator.Create(),
            input.NameAr,
            input.NameEn,
            input.CountryId
        );

        await _cityRepository.InsertAsync(city);

        var cityDto = ObjectMapper.Map<City, CityDto>(city);
        
        // Load country name for display
        var country = await _countryRepository.GetAsync(input.CountryId);
        cityDto.CountryName = country.NameEn;

        return cityDto;
    }

    public virtual async Task<CityDto> GetAsync(Guid id)
    {
        var city = await _cityRepository.GetAsync(id);
        var cityDto = ObjectMapper.Map<City, CityDto>(city);
        
        // Load country name for display
        var country = await _countryRepository.GetAsync(city.CountryId);
        cityDto.CountryName = country.NameEn;

        return cityDto;
    }

    public virtual async Task<PagedResultDto<CityDto>> GetListAsync(GetCityListDto input)
    {
        var queryable = await _cityRepository.GetQueryableAsync();

        // Apply country filter
        if (input.CountryId.HasValue)
        {
            queryable = queryable.Where(x => x.CountryId == input.CountryId.Value);
        }

        // Apply name filter
        if (!string.IsNullOrWhiteSpace(input.Filter))
        {
            queryable = queryable.Where(x =>
                x.NameAr.Contains(input.Filter) ||
                x.NameEn.Contains(input.Filter));
        }

        // Apply sorting - default to NameEn if no sorting specified
        queryable = queryable.OrderBy(x => x.NameEn);

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        queryable = queryable.PageBy(input.SkipCount, input.MaxResultCount);
        var cities = await AsyncExecuter.ToListAsync(queryable);

        var cityDtos = ObjectMapper.Map<System.Collections.Generic.List<City>, System.Collections.Generic.List<CityDto>>(cities);

        // Load country names
        var countryIds = cities.Select(x => x.CountryId).Distinct().ToList();
        var countries = await _countryRepository.GetListAsync(x => countryIds.Contains(x.Id));
        var countryDict = countries.ToDictionary(x => x.Id, x => x.NameEn);

        foreach (var cityDto in cityDtos)
        {
            if (countryDict.TryGetValue(cityDto.CountryId, out var countryName))
            {
                cityDto.CountryName = countryName;
            }
        }

        return new PagedResultDto<CityDto>(totalCount, cityDtos);
    }

    public virtual async Task<CityDto> UpdateAsync(Guid id, UpdateCityDto input)
    {
        var city = await _cityRepository.GetAsync(id);

        // Validate country exists
        var countryExists = await _countryRepository.AnyAsync(x => x.Id == input.CountryId);
        if (!countryExists)
        {
            throw new UserFriendlyException(
                L["City:CountryNotFound"],
                code: BookStoreDomainErrorCodes.CityCountryNotFound);
        }

        // Check if city name already exists in the country (excluding current city)
        var nameExists = await _cityRepository.IsNameExistsAsync(input.NameAr, input.NameEn, input.CountryId, id);
        if (nameExists)
        {
            throw new UserFriendlyException(
                L["City:NameAlreadyExists", input.NameAr, input.NameEn],
                code: BookStoreDomainErrorCodes.CityNameAlreadyExists);
        }

        city.Update(input.NameAr, input.NameEn, input.CountryId);
        await _cityRepository.UpdateAsync(city);

        var cityDto = ObjectMapper.Map<City, CityDto>(city);
        
        // Load country name for display
        var country = await _countryRepository.GetAsync(input.CountryId);
        cityDto.CountryName = country.NameEn;

        return cityDto;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _cityRepository.DeleteAsync(id);
    }
}

