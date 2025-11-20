using Acme.BookStore;
using Acme.BookStore.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Acme.BookStore.EntityFrameworkCore.Countries;

/// <summary>
/// Entity Framework Core configuration for Country entity
/// </summary>
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable(BookStoreConsts.DbTablePrefix + "Countries", BookStoreConsts.DbSchema);
        builder.ConfigureByConvention();

        // Configure properties
        builder.Property(x => x.NameAr)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(128);

        // Configure indexes
        builder.HasIndex(x => x.NameAr)
            .HasDatabaseName("IX_Countries_NameAr");

        builder.HasIndex(x => x.NameEn)
            .HasDatabaseName("IX_Countries_NameEn");

        // Configure relationships
        builder.HasMany(x => x.Cities)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

