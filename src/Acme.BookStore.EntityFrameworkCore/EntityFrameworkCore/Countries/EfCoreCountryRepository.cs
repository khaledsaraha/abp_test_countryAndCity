using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acme.BookStore.Countries;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookStore.EntityFrameworkCore.Countries;

/// <summary>
/// Entity Framework Core implementation of Country repository
/// </summary>
public class EfCoreCountryRepository : EfCoreRepository<BookStoreDbContext, Country, Guid>, ICountryRepository
{
    public EfCoreCountryRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<bool> IsNameExistsAsync(string nameAr, string nameEn, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = query.Where(x => 
            x.NameAr.ToLower() == nameAr.ToLower() || 
            x.NameEn.ToLower() == nameEn.ToLower());

        if (excludeId.HasValue)
        {
            query = query.Where(x => x.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<Country> FindByNameAsync(string nameAr, string nameEn, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        return await query.FirstOrDefaultAsync(x => 
            x.NameAr.ToLower() == nameAr.ToLower() || 
            x.NameEn.ToLower() == nameEn.ToLower(), 
            cancellationToken);
    }
}

