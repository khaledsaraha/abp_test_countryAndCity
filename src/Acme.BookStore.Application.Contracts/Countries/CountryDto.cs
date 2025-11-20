using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Countries;

/// <summary>
/// Country data transfer object
/// </summary>
public class CountryDto : FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// Arabic name of the country
    /// </summary>
    public string NameAr { get; set; } = null!;

    /// <summary>
    /// English name of the country
    /// </summary>
    public string NameEn { get; set; } = null!;
}

