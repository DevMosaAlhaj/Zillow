using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.ViewModel;
using Zillow.Service.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Zillow.Core.Dto.UpdateDto;

namespace Zillow.API.Controllers
{
    
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "Get All Users",
                    await _userService.GetAll(page, pageSize)));
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody]CreateUserDto dto)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "User Created Successfully",
                    await _userService.Create(dto)));
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id,[FromBody]UpdateUserDto dto)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "User Updated Successfully",
                    await _userService.Update(id,dto,UserId)));
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
            => await GetResponse(async () =>
                new ApiResponseViewModel(true, "User Deleted Successfully",
                    await _userService.Delete(id,UserId)));

    }
}
