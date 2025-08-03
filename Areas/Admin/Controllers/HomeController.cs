using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;

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
            _logger.LogInformation("🔍 Reached Admin/Home/Index action");
            return View();
        }
    }
}
