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

        [HttpGet]
        public IActionResult GetAll(int pageSize, int pageNo)
            => GetResponse((() =>
                new ApiResponseViewModel(true, "Get All Real Estates Successfully",
                    _realEstatesService.GetAll(pageSize, pageNo))));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRealEstatesDto dto)
        {
            var res = await _realEstatesService.Create(dto,UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Real Estate Created Successfully", res)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateRealEstatesDto entity)
        {
            var res = await _realEstatesService.Update(entity, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Real Estate Updated Successfully", res)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res=await _realEstatesService.Delete(id, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Real Estate Deleted Successfully", res)));
        }

    }
}
