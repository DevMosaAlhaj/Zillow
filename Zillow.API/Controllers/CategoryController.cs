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

        [HttpGet]
        public IActionResult GetAll(int pageSize, int pageNo)
            => GetResponse((() =>
                new ApiResponseViewModel(true, "Get All Categories Successfully",
                    _categoryService.GetAll(pageSize, pageNo))));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCategoryDto dto)
        {
            var res = await _categoryService.Create(dto, UserId);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Category Created Successfully",res)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateCategoryDto entity)
        {
            var res = await _categoryService.Update(entity, UserId);
            
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Category Updated Successfully")));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _categoryService.Delete(id, UserId);
            
            return GetResponse((() =>
                new ApiResponseViewModel(true, "Category Deleted Successfully")));
        }

    }
}
