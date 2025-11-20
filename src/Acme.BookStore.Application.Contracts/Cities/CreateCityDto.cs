using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Cities;

/// <summary>
/// DTO for creating a new city
/// </summary>
public class CreateCityDto
{
    /// <summary>
    /// Arabic name of the city
    /// </summary>
    [Required]
    [StringLength(128)]
    public string NameAr { get; set; } = null!;

    /// <summary>
    /// English name of the city
    /// </summary>
    [Required]
    [StringLength(128)]
    public string NameEn { get; set; } = null!;

    /// <summary>
    /// Foreign key to the country
    /// </summary>
    [Required]
    public Guid CountryId { get; set; }
}

