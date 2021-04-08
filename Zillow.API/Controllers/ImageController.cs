using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;
using Zillow.Service.Services.ImageServices;

namespace Zillow.API.Controllers
{
    public class ImageController : BaseController
    {

        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Get All Images Successfully",
                    await _imageService.GetAll(page, pageSize)));
        

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Get Image Successfully",
                    await _imageService.Get(id)));

        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateImageDto dto)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Create Image Successfully",
                    await _imageService.Create(dto,UserId)));
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] UpdateImageDto dto)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Update Image Successfully",
                    await _imageService.Update(id,dto,UserId)));
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Delete Image Successfully",
                    await _imageService.Delete(id,UserId)));
        
    }
}