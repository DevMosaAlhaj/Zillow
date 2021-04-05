using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zillow.Data.DbEntity;

namespace Zillow.Data.Constrains
{
    public class UserConstrains : IEntityTypeConfiguration<UserDbEntity>
    {
        public void Configure(EntityTypeBuilder<UserDbEntity> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired().HasMaxLength(10);
            
            builder.Property(x => x.LastName)
                .IsRequired().HasMaxLength(20);

            builder.Property(x => x.UserType)
                .IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}