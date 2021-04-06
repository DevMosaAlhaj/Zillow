using Zillow.Core.Dto;
using Zillow.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;

namespace Zillow.Service.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<PagingViewModel> GetAll(int page, int pageSize);

        Task<CategoryViewModel> Get(int id);
        Task<int> Create(CreateCategoryDto dto, string userId);
        Task<int> Update( int id , UpdateCategoryDto dto, string userId);
        Task<int> Delete(int id, string userId);
    }
}
