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

namespace Acme.BookStore.Countries;

/// <summary>
/// Application service implementation for Country operations
/// </summary>
[Authorize]
public class CountryAppService : BookStoreAppService, ICountryAppService
{
    private readonly ICountryRepository _countryRepository;
    private readonly ICityRepository _cityRepository;

    public CountryAppService(
        ICountryRepository countryRepository,
        ICityRepository cityRepository)
    {
        _countryRepository = countryRepository;
        _cityRepository = cityRepository;
    }

    public virtual async Task<CountryDto> CreateAsync(CreateCountryDto input)
    {
        // Check if country name already exists
        var nameExists = await _countryRepository.IsNameExistsAsync(input.NameAr, input.NameEn);
        if (nameExists)
        {
            throw new UserFriendlyException(
                L["Country:NameAlreadyExists", input.NameAr, input.NameEn],
                code: BookStoreDomainErrorCodes.CountryNameAlreadyExists);
        }

        var country = new Country(
            GuidGenerator.Create(),
            input.NameAr,
            input.NameEn
        );

        await _countryRepository.InsertAsync(country);

        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    public virtual async Task<CountryDto> GetAsync(Guid id)
    {
        var country = await _countryRepository.GetAsync(id);
        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    public virtual async Task<PagedResultDto<CountryDto>> GetListAsync(GetCountryListDto input)
    {
        var queryable = await _countryRepository.GetQueryableAsync();

        // Apply filter
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
        var countries = await AsyncExecuter.ToListAsync(queryable);

        return new PagedResultDto<CountryDto>(
            totalCount,
            ObjectMapper.Map<System.Collections.Generic.List<Country>, System.Collections.Generic.List<CountryDto>>(countries)
        );
    }

    public virtual async Task<CountryDto> UpdateAsync(Guid id, UpdateCountryDto input)
    {
        var country = await _countryRepository.GetAsync(id);

        // Check if country name already exists (excluding current country)
        var nameExists = await _countryRepository.IsNameExistsAsync(input.NameAr, input.NameEn, id);
        if (nameExists)
        {
            throw new UserFriendlyException(
                L["Country:NameAlreadyExists", input.NameAr, input.NameEn],
                code: BookStoreDomainErrorCodes.CountryNameAlreadyExists);
        }

        country.Update(input.NameAr, input.NameEn);
        await _countryRepository.UpdateAsync(country);

        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var country = await _countryRepository.GetAsync(id);
        
        // Check if country has cities
        var cities = await _cityRepository.GetByCountryIdAsync(id);
        
        if (cities != null && cities.Any())
        {
            throw new UserFriendlyException(
                L["Country:CannotDeleteHasCities"],
                code: BookStoreDomainErrorCodes.CountryCannotDeleteHasCities);
        }

        await _countryRepository.DeleteAsync(id);
    }
}

