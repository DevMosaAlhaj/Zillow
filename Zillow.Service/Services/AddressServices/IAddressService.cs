using System.Collections.Generic;
using System.Threading.Tasks;
using Zillow.Core.Dto;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;

namespace Zillow.Service.Services.AddressServices
{
    public interface IAddressService
    {
        Task<PagingViewModel> GetAll(int page, int pageSize);

        Task<AddressViewModel> Get(int id);
        Task<int> Create(CreateAddressDto dto, string userId);
        Task<int> Update(UpdateAddressDto dto, string userId);
        Task<int> Delete(int id, string userId);
    }
}