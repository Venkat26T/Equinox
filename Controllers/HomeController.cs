using Microsoft.AspNetCore.Mvc;
using Equinox.Models.Infrastructure;

namespace Equinox.Controllers
{
    public class HomeController : Controller
    {
        private void SetBookingCount()
        {
            var sessionHelper = new SessionHelper(HttpContext.Session);
            ViewBag.BookingCount = sessionHelper.GetBookings().Count;
        }

        public IActionResult Index()
        {
            SetBookingCount();
            return View();
        }

        public IActionResult Privacy()
        {
            SetBookingCount();
            return View();
        }

        public IActionResult Contact()
        {
            SetBookingCount();
            return View();
        }

        public IActionResult Terms()
        {
            SetBookingCount();
            return View();
        }

        public IActionResult CookiePolicy()
        {
            SetBookingCount();
            return View();
        }
    }
}
