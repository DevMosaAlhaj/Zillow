using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zillow.Core.Constant;
using Zillow.Data.DbEntity;

namespace Zillow.Data.Constrains
{
    public class ImageConstrains : IEntityTypeConfiguration<ImageDbEntity>
    {
        public void Configure(EntityTypeBuilder<ImageDbEntity> builder)
        {
            builder.Property(x => x.ImageUrl).IsRequired();

            builder.HasOne(x => x.RealEstate)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.RealEstateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDelete);

            builder.ToTable(DbTablesName.ImageTable);

        }
    }
}