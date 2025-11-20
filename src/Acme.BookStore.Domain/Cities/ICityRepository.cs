using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Cities;

/// <summary>
/// Custom repository interface for City entity
/// </summary>
public interface ICityRepository : IRepository<City, Guid>
{
    /// <summary>
    /// Checks if a city name already exists in a country (case-insensitive)
    /// </summary>
    Task<bool> IsNameExistsAsync(string nameAr, string nameEn, Guid countryId, Guid? excludeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all cities by country ID
    /// </summary>
    Task<List<City>> GetByCountryIdAsync(Guid countryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a city by name and country (case-insensitive)
    /// </summary>
    Task<City> FindByNameAsync(string nameAr, string nameEn, Guid countryId, CancellationToken cancellationToken = default);
}

