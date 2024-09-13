using helpmepickmymain.Models;
using helpmepickmymain.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace helpmepickmymain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("HomeController Index method called.");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorMessage)
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return View(new ErrorViewModel 
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, 
                ExceptionMessage = exception?.Error.Message, 
                ErrorMessage = errorMessage 
            });
        }
    }
}
