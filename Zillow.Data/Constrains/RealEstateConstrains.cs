using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zillow.Core.Constant;
using Zillow.Data.DbEntity;

namespace Zillow.Data.Constrains
{
    public class RealEstateConstrains : IEntityTypeConfiguration<RealEstateDbEntity>
    {
        public void Configure(EntityTypeBuilder<RealEstateDbEntity> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired().HasMaxLength(80);
            
            builder.Property(x => x.Description)
                .IsRequired().HasMaxLength(500);

            builder.HasOne(x => x.Address)
                .WithMany(x => x.RealEstates)
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.Category)
                .WithMany(x => x.RealEstates)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Images)
                .WithOne(x => x.RealEstate)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Contracts)
                .WithOne(x => x.RealEstate)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDelete);

            builder.ToTable(DbTablesName.RealEstateTable);

        }
    }
}