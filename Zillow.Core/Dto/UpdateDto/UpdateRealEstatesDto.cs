namespace Zillow.Core.Dto.UpdateDto
{
    public class UpdateRealEstatesDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int CategoryId { get; set; }
        
        public int AddressId { get; set; }
    }
}
