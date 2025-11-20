using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acme.BookStore.Cities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookStore.EntityFrameworkCore.Cities;

/// <summary>
/// Entity Framework Core implementation of City repository
/// </summary>
public class EfCoreCityRepository : EfCoreRepository<BookStoreDbContext, City, Guid>, ICityRepository
{
    public EfCoreCityRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<bool> IsNameExistsAsync(string nameAr, string nameEn, Guid countryId, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = query.Where(x => 
            x.CountryId == countryId &&
            (x.NameAr.ToLower() == nameAr.ToLower() || 
             x.NameEn.ToLower() == nameEn.ToLower()));

        if (excludeId.HasValue)
        {
            query = query.Where(x => x.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<List<City>> GetByCountryIdAsync(Guid countryId, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        return await query
            .Where(x => x.CountryId == countryId)
            .OrderBy(x => x.NameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<City> FindByNameAsync(string nameAr, string nameEn, Guid countryId, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        return await query.FirstOrDefaultAsync(x => 
            x.CountryId == countryId &&
            (x.NameAr.ToLower() == nameAr.ToLower() || 
             x.NameEn.ToLower() == nameEn.ToLower()), 
            cancellationToken);
    }
}

