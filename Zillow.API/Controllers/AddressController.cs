
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;
using Zillow.Service.Services.AddressServices;

namespace Zillow.API.Controllers
{
    
    public class AddressController : BaseController
    {
        private IAddressService _AddressService;
       
        public AddressController(IAddressService AddressService)
        {
            _AddressService = AddressService;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageSize, int pageNo)
        => await GetResponse(async ()
            => new ApiResponseViewModel(true, "", await _AddressService.GetAll(pageNo, pageSize)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateAddressDto dto)
        {

           var res=await _AddressService.Create(dto, UserId);

            return GetResponse((() =>
                new ApiResponseViewModel(true, "Address Created Successfully", res)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateAddressDto dto)
        {
            var res = await _AddressService.Update(dto, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Address Updated Successfully", res)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _AddressService.Delete(id, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Address Deleted Successfully", res)));
        }

    }
}
