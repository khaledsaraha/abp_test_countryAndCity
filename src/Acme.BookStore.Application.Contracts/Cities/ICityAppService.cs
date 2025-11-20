using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Cities;

/// <summary>
/// Application service interface for City operations
/// </summary>
public interface ICityAppService : ICrudAppService<
    CityDto,
    Guid,
    GetCityListDto,
    CreateCityDto,
    UpdateCityDto>
{
}

