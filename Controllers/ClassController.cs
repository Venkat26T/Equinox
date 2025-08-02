using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;
namespace Equinox.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult List(string id = "All")
        {
            return Content($"Main Area, Class Controller, List Action, id: {id}");
        }
    }
}
