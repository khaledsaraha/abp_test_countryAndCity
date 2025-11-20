using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Cities;

/// <summary>
/// City entity related to a country
/// </summary>
public class City : FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// Arabic name of the city
    /// </summary>
    public string NameAr { get; private set; }

    /// <summary>
    /// English name of the city
    /// </summary>
    public string NameEn { get; private set; }

    /// <summary>
    /// Foreign key to the country
    /// </summary>
    public Guid CountryId { get; private set; }

    /// <summary>
    /// Navigation property to the country
    /// </summary>
    public virtual Countries.Country Country { get; private set; }

    // EF Core constructor
    protected City()
    {
    }

    /// <summary>
    /// Constructor for creating a new city
    /// </summary>
    public City(Guid id, string nameAr, string nameEn, Guid countryId) : base(id)
    {
        SetNameAr(nameAr);
        SetNameEn(nameEn);
        SetCountryId(countryId);
    }

    /// <summary>
    /// Sets the Arabic name with validation
    /// </summary>
    public void SetNameAr(string nameAr)
    {
        if (string.IsNullOrWhiteSpace(nameAr))
        {
            throw new ArgumentException("City Arabic name cannot be null or empty", nameof(nameAr));
        }

        NameAr = nameAr.Trim();
    }

    /// <summary>
    /// Sets the English name with validation
    /// </summary>
    public void SetNameEn(string nameEn)
    {
        if (string.IsNullOrWhiteSpace(nameEn))
        {
            throw new ArgumentException("City English name cannot be null or empty", nameof(nameEn));
        }

        NameEn = nameEn.Trim();
    }

    /// <summary>
    /// Sets the country ID with validation
    /// </summary>
    public void SetCountryId(Guid countryId)
    {
        if (countryId == Guid.Empty)
        {
            throw new ArgumentException("Country ID cannot be empty", nameof(countryId));
        }

        CountryId = countryId;
    }

    /// <summary>
    /// Updates the city information
    /// </summary>
    public void Update(string nameAr, string nameEn, Guid countryId)
    {
        SetNameAr(nameAr);
        SetNameEn(nameEn);
        SetCountryId(countryId);
    }
}

