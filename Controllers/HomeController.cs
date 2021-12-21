using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POC.Tracking.Models;
using POC.Tracking.Services;

namespace POC.Tracking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITrackMeService _trackMeService;

        public HomeController(
            ILogger<HomeController> logger, 
            ITrackMeService trackMeService)
        {
            _logger = logger;
            _trackMeService = trackMeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TrackMe([FromBody] dynamic location)
        {
            return Json(_trackMeService.TrackMe(location));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
