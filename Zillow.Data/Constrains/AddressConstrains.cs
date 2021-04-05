using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zillow.Core.Constant;
using Zillow.Data.DbEntity;

namespace Zillow.Data.Constrains
{
    public class AddressConstrains : IEntityTypeConfiguration<AddressDbEntity>
    {
        public void Configure(EntityTypeBuilder<AddressDbEntity> builder)
        {
            builder.Property(x => x.CityName)
                .IsRequired().HasMaxLength(30);
            
            builder.Property(x => x.CountryName)
                .IsRequired().HasMaxLength(30);

            builder.HasMany(x => x.RealEstates)
                .WithOne(x => x.Address).OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDelete);

            builder.ToTable(DbTablesName.AddressTable);
        }
    }
}