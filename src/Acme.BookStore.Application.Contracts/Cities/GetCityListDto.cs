using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Cities;

/// <summary>
/// DTO for filtering and sorting cities
/// </summary>
public class GetCityListDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// Filter by name (searches in both Arabic and English names)
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// Filter by country ID
    /// </summary>
    public Guid? CountryId { get; set; }
}

