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

        [HttpGet]
        public IActionResult GetAll(int pageSize,int pageNo)
            => GetResponse((() =>
            new ApiResponseViewModel(true, "Get All Contracts Successfully",
                _contractsService.GetAll(pageSize, pageNo))));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateContractDto dto)
        {
            var res = await _contractsService.Create(dto, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Contract Created Successfully", res)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateContractDto entity)
        {
            var res = await _contractsService.Update(entity, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Contract Updated Successfully", res)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _contractsService.Delete(id, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Contract Deleted Successfully", res)));
        }

    }

   
}
