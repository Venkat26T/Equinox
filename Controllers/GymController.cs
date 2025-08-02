using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Equinox.Models;
using Equinox.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Equinox.Controllers
{
    public class GymController : Controller
    {
        private readonly EquinoxContext _context;

        private const string BookingSessionKey = "Bookings";
        private const string ClubFilterKey = "SelectedClub";
        private const string CategoryFilterKey = "SelectedCategory";

        public GymController(EquinoxContext context)
        {
            _context = context;
        }

        private void SetBookingCount()
        {
            var bookings = HttpContext.Session.GetObjectFromJson<List<int>>(BookingSessionKey) ?? new List<int>();
            ViewBag.BookingCount = bookings.Count;
        }

        public IActionResult ShowClasses(string? club, string? category)
        {
            SetBookingCount();

            club ??= HttpContext.Session.GetString(ClubFilterKey) ?? "All";
            category ??= HttpContext.Session.GetString(CategoryFilterKey) ?? "All";

            HttpContext.Session.SetString(ClubFilterKey, club);
            HttpContext.Session.SetString(CategoryFilterKey, category);

            var model = new FilterViewModel
            {
                AllClubs = _context.Clubs.ToList(),
                AllCategories = _context.ClassCategories.ToList(),
                SelectedClubName = club,
                SelectedCategoryName = category
            };

            var query = _context.Classes
                .Include(c => c.Club)
                .Include(c => c.ClassCategory)
                .Include(c => c.User)
                .AsQueryable();

            if (club != "All")
                query = query.Where(c => c.Club.Name == club);

            if (category != "All")
                query = query.Where(c => c.ClassCategory.Name == category);

            model.AvailableClasses = query.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Filter(string club, string category)
        {
            HttpContext.Session.SetString(ClubFilterKey, club);
            HttpContext.Session.SetString(CategoryFilterKey, category);
            return RedirectToAction("ShowClasses");
        }

        public IActionResult Detail(int id)
        {
            SetBookingCount();

            var gymClass = _context.Classes
                .Include(c => c.Club)
                .Include(c => c.ClassCategory)
                .Include(c => c.User)
                .FirstOrDefault(c => c.EquinoxClassId == id);

            if (gymClass == null)
                return NotFound();

            return View(gymClass);
        }

        [HttpPost]
        public IActionResult Book(int id)
        {
            var bookings = HttpContext.Session.GetObjectFromJson<List<int>>(BookingSessionKey) ?? new List<int>();

            if (!bookings.Contains(id))
            {
                bookings.Add(id);
                HttpContext.Session.SetObjectAsJson(BookingSessionKey, bookings);
                TempData["Message"] = "Class booked successfully!";
            }

            return RedirectToAction("ShowClasses");
        }

        public IActionResult ViewBookings()
        {
            SetBookingCount();

            var bookings = HttpContext.Session.GetObjectFromJson<List<int>>(BookingSessionKey) ?? new List<int>();

            var bookedClasses = _context.Classes
                .Include(c => c.Club)
                .Include(c => c.ClassCategory)
                .Include(c => c.User)
                .Where(c => bookings.Contains(c.EquinoxClassId))
                .ToList();

            return View(bookedClasses);
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var bookings = HttpContext.Session.GetObjectFromJson<List<int>>(BookingSessionKey) ?? new List<int>();

            if (bookings.Contains(id))
            {
                bookings.Remove(id);
                HttpContext.Session.SetObjectAsJson(BookingSessionKey, bookings);
                TempData["Message"] = "Booking canceled.";
            }

            return RedirectToAction("ViewBookings");
        }
    }
}
