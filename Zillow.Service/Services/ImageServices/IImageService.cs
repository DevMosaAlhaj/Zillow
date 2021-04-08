using System.Threading.Tasks;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;

namespace Zillow.Service.Services.ImageServices
{
    public interface IImageService
    {
        Task<PagingViewModel> GetAll(int page, int pageSize);

        Task<ImageViewModel> Get(int id);

        Task<int> Create(CreateImageDto dto, string userId);

        Task<int> Update(int id, UpdateImageDto dto, string userId);

        Task<int> Delete(int id, string userId);
    }
}