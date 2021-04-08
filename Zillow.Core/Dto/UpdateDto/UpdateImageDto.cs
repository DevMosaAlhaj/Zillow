using Microsoft.AspNetCore.Http;

namespace Zillow.Core.Dto.UpdateDto
{
    public class UpdateImageDto
    {
        public int Id { get; set; }
        
        public IFormFile Image { get; set; }

    }
}