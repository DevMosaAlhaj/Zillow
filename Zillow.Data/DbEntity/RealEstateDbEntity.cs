using System.Collections.Generic;

namespace Zillow.Data.DbEntity
{
    public class RealEstateDbEntity : BaseEntity
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public CategoryDbEntity Category { get; set; }
        public int AddressId { get; set; }
        public AddressDbEntity Address { get; set; }

        public List<ImageDbEntity> Images { get; set; }
        public List<ContractDbEntity> Contracts { get; set; }

    }
}
