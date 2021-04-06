using System.Collections.Generic;
using System.Threading.Tasks;
using Zillow.Core.Dto;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;

namespace Zillow.Service.Services.ContractServices
{
  public  interface IContractService
  {
      Task<PagingViewModel> GetAll(int page, int pageSize);

      Task<ContractViewModel> Get(int id);
      Task<int> Create(CreateContractDto dto, string userId);
      Task<int> Update(int id ,UpdateContractDto dto, string userId);
      Task<int> Delete(int id, string userId);
  }
}
