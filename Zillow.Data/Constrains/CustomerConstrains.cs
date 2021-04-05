using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zillow.Core.Constant;
using Zillow.Data.DbEntity;

namespace Zillow.Data.Constrains
{
    public class CustomerConstrains : IEntityTypeConfiguration<CustomerDbEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerDbEntity> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired().HasMaxLength(25);
            
            builder.Property(x => x.Phone)
                .IsRequired().HasMaxLength(15);
            
            builder.Property(x => x.Address)
                .IsRequired().HasMaxLength(50);

            builder.HasMany(x => x.Contracts)
                .WithOne(x => x.Customer)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDelete);

            builder.ToTable(DbTablesName.CustomerTable);
        }
    }
}