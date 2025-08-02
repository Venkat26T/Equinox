using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;
namespace Equinox.Controllers
{
    public class MembershipController : Controller
    {
        public IActionResult List(string id = "All")
        {
            return Content($"Main Area, Membership Controller, List Action, id: {id}");
        }
    }
}
