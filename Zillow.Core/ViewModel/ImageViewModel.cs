namespace Zillow.Core.ViewModel
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        
        public string ImageUrl { get; set; }

        public RealEstateViewModel RealEstate { get; set; }
    }
}