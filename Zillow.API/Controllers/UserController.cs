using Zillow.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.ViewModel;
using Zillow.Service.Services.UserServices;
using Microsoft.AspNetCore.Authorization;

namespace Zillow.API.Controllers
{
    
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
            => GetResponse((() =>
                new ApiResponseViewModel(true, "Get All Users Successfully",
                    _userService.GetAll())));

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody]CreateUserDto dto)
        {
            await _userService.Register(dto);
            return GetResponse((() =>
                new ApiResponseViewModel(true, "User Created Successfully")));
        }

 

    }
}
