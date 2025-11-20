using Acme.BookStore;
using Acme.BookStore.Cities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Acme.BookStore.EntityFrameworkCore.Cities;

/// <summary>
/// Entity Framework Core configuration for City entity
/// </summary>
public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable(BookStoreConsts.DbTablePrefix + "Cities", BookStoreConsts.DbSchema);
        builder.ConfigureByConvention();

        // Configure properties
        builder.Property(x => x.NameAr)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.CountryId)
            .IsRequired();

        // Configure indexes
        builder.HasIndex(x => x.NameAr)
            .HasDatabaseName("IX_Cities_NameAr");

        builder.HasIndex(x => x.NameEn)
            .HasDatabaseName("IX_Cities_NameEn");

        builder.HasIndex(x => x.CountryId)
            .HasDatabaseName("IX_Cities_CountryId");

        // Configure unique constraint: City name should be unique within a country
        builder.HasIndex(x => new { x.NameAr, x.CountryId })
            .IsUnique()
            .HasDatabaseName("UK_Cities_NameAr_CountryId");

        builder.HasIndex(x => new { x.NameEn, x.CountryId })
            .IsUnique()
            .HasDatabaseName("UK_Cities_NameEn_CountryId");

        // Configure relationships
        builder.HasOne(x => x.Country)
            .WithMany(x => x.Cities)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

