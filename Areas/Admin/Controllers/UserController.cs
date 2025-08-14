using Microsoft.AspNetCore.Mvc;
using Equinox.Models.DataLayer;
using Equinox.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly EquinoxContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(EquinoxContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("🔍 Reached Admin/User/Index action");
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Message"] = "User created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _context.Update(user);
            _context.SaveChanges();

            TempData["Message"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                TempData["Message"] = "User not found.";
                return RedirectToAction(nameof(Index));
            }

            // Check if any class assigned to this user has bookings
            bool hasBookings = _context.Bookings
                .Include(b => b.EquinoxClass)
                .Any(b => b.EquinoxClass.UserId == id);

            if (hasBookings)
            {
                TempData["Message"] = $"Cannot delete user '{user.Name}' because they have booked classes.";
                return RedirectToAction(nameof(Index));
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            TempData["Message"] = $"User '{user.Name}' deleted successfully.";

            return RedirectToAction(nameof(Index));
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateDOB(DateTime dob)
        {
            var minAge = 8;
            var maxAge = 80;
            var today = DateTime.Today;

            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;

            if (dob >= today || age < minAge || age > maxAge)
            {
                return Json($"Age must be between {minAge} and {maxAge}, and DOB must be in the past.");
            }

            return Json(true);
        }
    }
}
