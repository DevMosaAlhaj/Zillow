using Microsoft.AspNetCore.Http;

namespace Zillow.Core.Dto.CreateDto
{
    public class CreateImageDto
    {
        public IFormFile Image { get; set; }
        
        public int RealEstateId { get; set; }
    }
}