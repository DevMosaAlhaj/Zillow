using Microsoft.AspNetCore.Mvc;

namespace Zillow.API.Controllers
{
    public class HomeController : Controller
    {
       

        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("/Swagger");
        }

    }
}
