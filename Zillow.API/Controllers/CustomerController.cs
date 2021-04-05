using Zillow.Core.Dto;

using Microsoft.AspNetCore.Mvc;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;
using Zillow.Service.Services.CustomerServices;
using System.Threading.Tasks;

namespace Zillow.API.Controllers
{
    
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll(int pageSize,int pageNo)
        => GetResponse((() =>
        new ApiResponseViewModel(true, "Get All Customers Successfully",
            _customerService.GetAll(pageSize, pageNo))));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCustomerDto dto)
        {
            var res= await _customerService.Create(dto, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Customer Created Successfully", res)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateCustomerDto entity)
        {
            var res = await _customerService.Update(entity, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Customer Updated Successfully", res)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _customerService.Delete(id, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Customer Deleted Successfully", res)));
        }

    }
}
