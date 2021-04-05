using Zillow.Core.Enum;

namespace Zillow.Data.DbEntity
{
    public class ContractDbEntity : BaseEntity
    {
        
        public ContractType ContractType { get; set; }
        public double Price { get; set; }
        public int RealEstatesId { get; set; }
        public RealEstateDbEntity RealEstate { get; set; }
        public int CustomerId { get; set; }
        public CustomerDbEntity Customer { get; set; }
    }
}
