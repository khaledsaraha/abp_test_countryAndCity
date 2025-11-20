using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Countries;

/// <summary>
/// Application service interface for Country operations
/// </summary>
public interface ICountryAppService : ICrudAppService<
    CountryDto,
    Guid,
    GetCountryListDto,
    CreateCountryDto,
    UpdateCountryDto>
{
}

