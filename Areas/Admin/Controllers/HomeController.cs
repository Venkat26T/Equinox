using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("✅ Admin/HomeController.Index loaded successfully.");
            return View();
        }
    }
}
