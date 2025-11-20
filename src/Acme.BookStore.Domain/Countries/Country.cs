using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Countries;

/// <summary>
/// Country aggregate root entity
/// </summary>
public class Country : FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// Arabic name of the country
    /// </summary>
    public string NameAr { get; private set; }

    /// <summary>
    /// English name of the country
    /// </summary>
    public string NameEn { get; private set; }

    /// <summary>
    /// Navigation property for cities
    /// </summary>
    public virtual ICollection<Cities.City> Cities { get; private set; }

    // EF Core constructor
    protected Country()
    {
        Cities = new List<Cities.City>();
    }

    /// <summary>
    /// Constructor for creating a new country
    /// </summary>
    public Country(Guid id, string nameAr, string nameEn) : base(id)
    {
        SetNameAr(nameAr);
        SetNameEn(nameEn);
        Cities = new List<Cities.City>();
    }

    /// <summary>
    /// Sets the Arabic name with validation
    /// </summary>
    public void SetNameAr(string nameAr)
    {
        if (string.IsNullOrWhiteSpace(nameAr))
        {
            throw new ArgumentException("Country Arabic name cannot be null or empty", nameof(nameAr));
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
            throw new ArgumentException("Country English name cannot be null or empty", nameof(nameEn));
        }

        NameEn = nameEn.Trim();
    }

    /// <summary>
    /// Updates the country information
    /// </summary>
    public void Update(string nameAr, string nameEn)
    {
        SetNameAr(nameAr);
        SetNameEn(nameEn);
    }
}

