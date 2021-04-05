using Microsoft.EntityFrameworkCore;
using Zillow.Data.Constrains;

namespace Zillow.Data.Extenstion
{
    public static class EntityConstrainsExtenstion
    {
        public static ModelBuilder ApplyEntitiesConstrains(this ModelBuilder builder)
        {

            builder.ApplyConfiguration(new AddressConstrains());
            builder.ApplyConfiguration(new CategoryConstrains());
            builder.ApplyConfiguration(new ContractConstrains());
            builder.ApplyConfiguration(new CustomerConstrains());
            builder.ApplyConfiguration(new ImageConstrains());
            builder.ApplyConfiguration(new RealEstateConstrains());
            builder.ApplyConfiguration(new UserConstrains());
            
            return builder;
        }
    }
}