using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zillow.API.Controllers;
using Zillow.Core.Dto.AuthDto;
using Zillow.Core.ViewModel;
using Zillow.Service.Services.AuthServices;

namespace Zillow.Api.Controllers
{
    public class AuthController : BaseController
    {

        private readonly IAuthService _service;

        public AuthController (IAuthService service)
        {
            _service = service;
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
            => await GetResponse(async () =>
            new ApiResponseViewModel(true,"Login Successfully",await _service.Login(dto)));

        [HttpPost]
        public async Task<IActionResult> Logout ([FromBody] string accessToken)
            => await GetResponse(async () => 
            new ApiResponseViewModel(true, "Logout Successfully", await _service.Logout(accessToken)));

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
            =>  await GetResponse( async () =>
            new ApiResponseViewModel(true, "Token Refreshed Successfully", await _service.RefreshToken(refreshToken)));

        [HttpPost]
        public async Task<IActionResult> RegisterFcm( [FromBody] string fcmToken)
            => await GetResponse(async () =>
            new ApiResponseViewModel(true, "FCM Token Registerd Successfully", await _service.RegisterFcmToken(UserId,fcmToken)));


    }
}
