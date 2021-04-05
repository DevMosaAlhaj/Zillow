using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Zillow.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Zillow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {

        protected string UserId { set; get; }
        
        protected IActionResult GetResponse(Func<ApiResponseViewModel> func)
            => Ok(func());

        protected async Task<IActionResult> GetResponse(Func<Task<ApiResponseViewModel>> func)
            => Ok( await func());

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (User.Identity != null && User.Identity.IsAuthenticated)
                UserId = User.FindFirst(ClaimTypes.Sid)?.Value;
        }
    }
}
