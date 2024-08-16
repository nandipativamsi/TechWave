using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechWave.Models;
using TechWave.Models.DomainModel;

namespace TechWave.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult StatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("404");
                case 500:
                    return View("500");
                default:
                    return View("Error");
            }
        }
    }
}
