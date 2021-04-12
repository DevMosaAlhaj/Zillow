using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zillow.Data.DbEntity;
using Zillow.Data.Extenstion;

namespace Zillow.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserDbEntity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyEntitiesConstrains();
            
        }

        public DbSet<AddressDbEntity> Address { get; set; }
        public DbSet<CategoryDbEntity> Category { get; set; }
        public DbSet<ContractDbEntity> Contract { get; set; }
        public DbSet<CustomerDbEntity> Customer { get; set; }
        public DbSet<ImageDbEntity> Images { get; set; }
        public DbSet<RealEstateDbEntity> RealEstate { get; set; }
    }
}