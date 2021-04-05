using System.Collections.Generic;
using System.Threading.Tasks;
using Zillow.Core.Dto;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;

namespace Zillow.Service.Services.RealEstateServices
{
    public interface IRealEstatesService
    {
        Task<PagingViewModel> GetAll(int page, int pageSize);
        Task<RealEstateViewModel> Get(int id);
        Task<int> Create(CreateRealEstatesDto dto, string userId);
        Task<int> Update(UpdateRealEstatesDto dto, string userId);
        Task<int> Delete(int id, string userId);
    }
}
