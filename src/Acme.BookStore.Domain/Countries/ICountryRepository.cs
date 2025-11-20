using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Countries;

/// <summary>
/// Custom repository interface for Country entity
/// </summary>
public interface ICountryRepository : IRepository<Country, Guid>
{
    /// <summary>
    /// Checks if a country name already exists (case-insensitive)
    /// </summary>
    Task<bool> IsNameExistsAsync(string nameAr, string nameEn, Guid? excludeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a country by name (case-insensitive)
    /// </summary>
    Task<Country> FindByNameAsync(string nameAr, string nameEn, CancellationToken cancellationToken = default);
}

