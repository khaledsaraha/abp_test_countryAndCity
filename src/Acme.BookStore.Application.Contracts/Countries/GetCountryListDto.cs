using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Countries;

/// <summary>
/// DTO for filtering and sorting countries
/// </summary>
public class GetCountryListDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// Filter by name (searches in both Arabic and English names)
    /// </summary>
    public string? Filter { get; set; }
}

