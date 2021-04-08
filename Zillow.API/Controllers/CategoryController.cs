using Zillow.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;
using Zillow.Service.Services.CategoryServices;
using System.Threading.Tasks;

namespace Zillow.API.Controllers
{
    
    public class CategoryController : BaseController
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Get All Categories Successfully",
                     await _categoryService.GetAll(page, pageSize)));
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Get Category Successfully",
                    await _categoryService.Get(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCategoryDto dto)
        => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Category Created Successfully",
                await _categoryService.Create(dto, UserId)));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,[FromBody]UpdateCategoryDto dto)
        => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Category Updated Successfully",
                await _categoryService.Update(id , dto, UserId)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        => await GetResponse(async () =>
            new ApiResponseViewModel(true, "Category Deleted Successfully",
                await _categoryService.Delete(id, UserId)));

    }
}
