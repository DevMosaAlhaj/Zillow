using System.Collections.Generic;

namespace Zillow.Data.DbEntity
{
    public class CustomerDbEntity : BaseEntity
    {
        
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<ContractDbEntity> Contracts { get; set; }
    }
}
