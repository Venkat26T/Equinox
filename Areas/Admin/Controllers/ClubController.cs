using Microsoft.AspNetCore.Mvc;
using Equinox.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClubController : Controller
    {
        private readonly EquinoxContext _context;
        private readonly ILogger<ClubController> _logger;

        public ClubController(ILogger<ClubController> logger, EquinoxContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("🔍 Reached Admin/Club/Index action");

            var clubs = _context.Clubs.ToList();
            return View(clubs);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Club club)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the error");
                return View(club);
            }

            _context.Clubs.Add(club);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var club = _context.Clubs.Find(id);
            return club == null ? NotFound() : View(club);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Club club)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the error");
                return View(club);
            }

            _context.Clubs.Update(club);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var club = _context.Clubs.Find(id);
            return club == null ? NotFound() : View(club);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var club = _context.Clubs.Find(id);
            if (club != null)
            {
                _context.Clubs.Remove(club);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
