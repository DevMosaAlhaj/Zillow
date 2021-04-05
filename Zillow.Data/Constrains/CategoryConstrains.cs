using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zillow.Core.Constant;
using Zillow.Data.DbEntity;

namespace Zillow.Data.Constrains
{
    public class CategoryConstrains : IEntityTypeConfiguration<CategoryDbEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryDbEntity> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(100);
            
            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.HasMany(x => x.RealEstates)
                .WithOne(x => x.Category)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDelete);

            builder.ToTable(DbTablesName.CategoryTable);
        }
    }
}