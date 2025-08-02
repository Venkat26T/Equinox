using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
