using Zillow.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;
using Zillow.Service.Services.RealEstateServices;

namespace Zillow.API.Controllers
{
    
    public class RealEstatesController : BaseController
    {
        private readonly IRealEstatesService _realEstatesService;

        public RealEstatesController(IRealEstatesService realEstatesService)
        {
            _realEstatesService = realEstatesService;
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Get All Real Estates Successfully",
                     await _realEstatesService.GetAll(page, pageSize)));
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Get(int id)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Get Real Estate Successfully",
                    await _realEstatesService.Get(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRealEstatesDto dto)
        => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Real Estate Created Successfully",
                await _realEstatesService.Create(dto,UserId)));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,[FromBody]UpdateRealEstatesDto dto)
        => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Real Estate Updated Successfully",
                await _realEstatesService.Update(id,dto, UserId)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Real Estate Deleted Successfully",
                await _realEstatesService.Delete(id, UserId)));

    }
}
