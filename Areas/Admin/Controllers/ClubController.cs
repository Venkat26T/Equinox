using Microsoft.AspNetCore.Mvc;
using Equinox.Models;
using System.Collections.Generic;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClubController : Controller
    {
        private readonly EquinoxContext _context;

        public ClubController(EquinoxContext context) => _context = context;

        public IActionResult Index()
        {
            List<Club> clubs = new List<Club>();
            foreach (var c in _context.Clubs)
            {
                clubs.Add(c);
            }
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
            return RedirectToAction("Index");
        }



        public IActionResult Edit(int id)
        {
            Club club = _context.Clubs.Find(id);

            //    var club = _context.Clubs.Find(id);
            //if (club == null)
            //{
            //  return NotFound();
            //}

            return View(club);
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
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Club club = _context.Clubs.Find(id);
            return View(club);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Club club = _context.Clubs.Find(id);
            if (club != null)
            {
                _context.Clubs.Remove(club);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
