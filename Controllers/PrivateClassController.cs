using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;
namespace Equinox.Controllers
{
    public class PrivateClassController : Controller
    {
        public IActionResult List(string id = "All")
        {
            return Content($"Main Area, PrivateClass Controller, List Action, id: {id}");
        }
    }
}
