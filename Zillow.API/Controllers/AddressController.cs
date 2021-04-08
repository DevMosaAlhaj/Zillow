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
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
            => await GetResponse(async ()
                => new ApiResponseViewModel(true, "Get All Address Successfully",
                    await _addressService.GetAll(page, pageSize)));


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Get Address Successfully",
                    await _addressService.Get(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddressDto dto)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Address Created Successfully",
                    await _addressService.Create(dto, UserId)));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressDto dto)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Address Updated Successfully",
                    await _addressService.Update(id, dto, UserId)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Address Deleted Successfully",
                    await _addressService.Delete(id, UserId)));
    }
}