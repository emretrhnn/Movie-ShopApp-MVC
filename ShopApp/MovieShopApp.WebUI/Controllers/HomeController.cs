using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieShopApp.WebUI.Models;

namespace MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        // If you want to perform logging operations in controllers, this field can be defined and injected through
        // the constructor and logging operations can be performed by calling methods through this field.
        private readonly ILogger<HomeController> _logger; 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() 
        {
            _logger.LogInformation($"Home Controller -> Index Action executed on {DateTime.Now}.");

            return View("Welcome"); 
        }

        public IActionResult Privacy() 
        {
            _logger.LogDebug($"Home Controller -> Privacy Action executed on {DateTime.Now}.");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] 
        public IActionResult Error() 
        {
            _logger.LogError($"Home Controller -> Error Action executed on {DateTime.Now}.");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }); 
        }
    }
}