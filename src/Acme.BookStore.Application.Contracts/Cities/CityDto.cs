using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Cities;

/// <summary>
/// City data transfer object
/// </summary>
public class CityDto : FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// Arabic name of the city
    /// </summary>
    public string NameAr { get; set; } = null!;

    /// <summary>
    /// English name of the city
    /// </summary>
    public string NameEn { get; set; } = null!;

    /// <summary>
    /// Foreign key to the country
    /// </summary>
    public Guid CountryId { get; set; }

    /// <summary>
    /// Country name (for display purposes)
    /// </summary>
    public string CountryName { get; set; } = null!;
}

