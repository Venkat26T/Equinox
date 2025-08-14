using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Equinox.Models;
using Equinox.Models.ViewModels;
using Equinox.Models.Infrastructure;
using Equinox.Models.DataLayer;
using Equinox.Models.DomainModels;
using System.Text.Json;

namespace Equinox.Controllers
{
    public class GymController : Controller
    {
        private readonly EquinoxContext _context;

        public GymController(EquinoxContext context)
        {
            _context = context;
        }

        private void SetBookingCount()
        {
            var sessionHelper = new SessionHelper(HttpContext.Session);
            ViewBag.BookingCount = sessionHelper.GetBookings().Count;
        }

        public IActionResult ShowClasses(string? club, string? category)
        {
            SetBookingCount();

            var sessionHelper = new SessionHelper(HttpContext.Session);

            club ??= sessionHelper.GetSelectedClub();
            category ??= sessionHelper.GetSelectedCategory();

            sessionHelper.SetSelectedClub(club);
            sessionHelper.SetSelectedCategory(category);

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
            var sessionHelper = new SessionHelper(HttpContext.Session);
            sessionHelper.SetSelectedClub(club);
            sessionHelper.SetSelectedCategory(category);

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
            // Look up the class; Find can return null, so guard it
            var equinoxClass = _context.Classes.Find(id);
            if (equinoxClass == null)
            {
                TempData["Message"] = "Sorry, that class was not found.";
                return RedirectToAction(nameof(ShowClasses));
            }

            // Save booking to DB without user info
            var booking = new Booking
            {
                EquinoxClassId = id,
                EquinoxClass = equinoxClass
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            // Update session bookings (list of class IDs)
            var sessionHelper = new SessionHelper(HttpContext.Session);
            var bookings = sessionHelper.GetBookings();

            if (!bookings.Contains(id))
            {
                bookings.Add(id);
                sessionHelper.SetBookings(bookings);
            }

            TempData["Message"] = "Class booked successfully!";
            return RedirectToAction(nameof(ShowClasses));
        }

        public IActionResult ViewBookings()
        {
            SetBookingCount();

            var sessionHelper = new SessionHelper(HttpContext.Session);
            var bookings = sessionHelper.GetBookings();

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
            var sessionHelper = new SessionHelper(HttpContext.Session);
            var bookings = sessionHelper.GetBookings();

            if (bookings.Contains(id))
            {
                bookings.Remove(id);
                sessionHelper.SetBookings(bookings);
                TempData["Message"] = "Booking canceled.";
            }

            return RedirectToAction(nameof(ViewBookings));
        }
    }
}
