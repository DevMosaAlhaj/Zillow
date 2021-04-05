using System.Collections.Generic;

namespace Zillow.Data.DbEntity
{
    public class CategoryDbEntity : BaseEntity
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RealEstateDbEntity> RealEstates { get; set; }
    }
}
