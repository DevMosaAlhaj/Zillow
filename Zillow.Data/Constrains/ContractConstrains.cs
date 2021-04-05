using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zillow.Core.Constant;
using Zillow.Data.DbEntity;

namespace Zillow.Data.Constrains
{
    public class ContractConstrains : IEntityTypeConfiguration<ContractDbEntity>
    {
        public void Configure(EntityTypeBuilder<ContractDbEntity> builder)
        {
            builder.Property(x => x.ContractType).IsRequired();
            
            builder.Property(x => x.Price)
                .IsRequired().HasMaxLength(12);

            builder.HasOne(x => x.RealEstate)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.RealEstatesId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDelete);

            builder.ToTable(DbTablesName.ContractTable);
        }
    }
}