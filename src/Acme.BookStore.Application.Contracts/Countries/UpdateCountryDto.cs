using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Countries;

/// <summary>
/// DTO for updating an existing country
/// </summary>
public class UpdateCountryDto
{
    /// <summary>
    /// Arabic name of the country
    /// </summary>
    [Required]
    [StringLength(128)]
    public string NameAr { get; set; } = null!;

    /// <summary>
    /// English name of the country
    /// </summary>
    [Required]
    [StringLength(128)]
    public string NameEn { get; set; } = null!;
}

