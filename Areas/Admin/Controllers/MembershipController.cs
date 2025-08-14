using Equinox.Models.DataLayer;
using Equinox.Models; // <-- changed from .DomainModels
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MembershipController : Controller
    {
        private readonly EquinoxContext _context;
        public MembershipController(EquinoxContext context) => _context = context;

        // GET: Admin/Membership
        public IActionResult Index()
        {
            var list = _context.Memberships
                .OrderBy(m => m.Name)
                .ToList();
            return View(list);
        }

        // GET: Admin/Membership/Create
        public IActionResult Create() => View();

        // POST: Admin/Membership/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Membership model)
        {
            if (!ModelState.IsValid) return View(model);
            _context.Memberships.Add(model);
            _context.SaveChanges();
            TempData["Message"] = "Membership created.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Membership/Edit/5
        public IActionResult Edit(int id)
        {
            var m = _context.Memberships.Find(id);
            return m == null ? NotFound() : View(m);
        }

        // POST: Admin/Membership/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Membership model)
        {
            if (id != model.MembershipID) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            _context.Memberships.Update(model);
            _context.SaveChanges();
            TempData["Message"] = "Membership updated.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Membership/Delete/5
        public IActionResult Delete(int id)
        {
            var m = _context.Memberships.Find(id);
            return m == null ? NotFound() : View(m);
        }

        // POST: Admin/Membership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var m = _context.Memberships.Find(id);
            if (m == null) return NotFound();
            _context.Memberships.Remove(m);
            _context.SaveChanges();
            TempData["Message"] = "Membership deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
