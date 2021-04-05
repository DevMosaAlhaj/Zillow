using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
