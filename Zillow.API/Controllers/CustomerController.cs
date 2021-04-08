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

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetAll(int page,int pageSize)
        => await GetResponse( async () =>
        new ApiResponseViewModel(true, "Get All Customers Successfully",
             await _customerService.GetAll(page, pageSize)));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Get Customer Successfully",
                    await _customerService.Get(id)));
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCustomerDto dto)
        => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Customer Created Successfully",
                await _customerService.Create(dto, UserId)));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,[FromBody]UpdateCustomerDto dto)
       => await GetResponse(async () =>
           new ApiResponseViewModel(true, "Customer Updated Successfully",
               await _customerService.Update(id,dto, UserId)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Customer Deleted Successfully",
                await _customerService.Delete(id, UserId)));

    }
}
