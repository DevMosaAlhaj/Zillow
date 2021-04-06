using Zillow.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;
using Zillow.Service.Services.ContractServices;
using System.Threading.Tasks;

namespace Zillow.API.Controllers
{
    
    public class ContractsController : BaseController
    {
        private readonly IContractService _contractsService;

        public ContractsController(IContractService contractsService)
        {
            _contractsService = contractsService;
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetAll(int page,int pageSize)
            => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Get All Contracts Successfully",
                await _contractsService.GetAll(page, pageSize)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateContractDto dto)
        => await GetResponse( async () =>
            new ApiResponseViewModel(true, "Contract Created Successfully",
                await _contractsService.Create(dto, UserId)));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,[FromBody]UpdateContractDto dto)
        => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Contract Updated Successfully",
                await _contractsService.Update(id,dto, UserId)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        => await GetResponse( async () =>
            new ApiResponseViewModel(true, "Contract Deleted Successfully",
                await _contractsService.Delete(id, UserId)));

    }

   
}
