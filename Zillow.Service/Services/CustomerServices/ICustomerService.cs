using Zillow.Core.ViewModel;
using System.Collections.Generic;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using System.Threading.Tasks;

namespace Zillow.Service.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<PagingViewModel> GetAll(int page, int pageSize);
        Task<CustomerViewModel> Get(int id);
        Task<int> Create(CreateCustomerDto dto, string userId);
        Task<int> Update(UpdateCustomerDto dto, string userId);
        Task<int> Delete(int id, string userId);
    }
}
