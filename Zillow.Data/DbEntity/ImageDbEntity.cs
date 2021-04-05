namespace Zillow.Data.DbEntity
{
    public class ImageDbEntity : BaseEntity
    {

        public string ImageUrl { get; set; }
        public int RealEstateId { get; set; }
        public RealEstateDbEntity RealEstate { get; set; }

        
    }
}
