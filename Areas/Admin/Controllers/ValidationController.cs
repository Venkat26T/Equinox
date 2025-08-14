using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Equinox.Models.DataLayer;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private readonly EquinoxContext _context;

        public ValidationController(EquinoxContext context)
        {
            _context = context;
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult CheckPhone(string phoneNumber)
        {
            bool exists = _context.Users.Any(u => u.PhoneNumber == phoneNumber);
            return exists
                ? Json($"Phone number {phoneNumber} is already registered.")
                : Json(true);
        }
    }
}
