using Microsoft.AspNetCore.Mvc;
using Equinox.Models;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClassCategoryController : Controller
    {
        private readonly EquinoxContext _context;

        public ClassCategoryController(EquinoxContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.ClassCategories.ToList();
            return View(categories);
        }

        public IActionResult Create() => View();

      

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(ClassCategory category)
{
    
    if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the error");
                return View(category);
            }
    
                      _context.ClassCategories.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
}







        public IActionResult Edit(int id)
        {
            var category = _context.ClassCategories.Find(id);
            return category == null ? NotFound() : View(category);
        }

        [HttpPost]
        
[ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ClassCategory category)
        {
            if (id != category.ClassCategoryId) return BadRequest();
            if (ModelState.IsValid)
            {
                _context.Update(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Please fix the error");     
                   return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.ClassCategories.Find(id);
            return category == null ? NotFound() : View(category);
        }

        [HttpPost, ActionName("Delete")]
        
[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _context.ClassCategories.Find(id);
            if (category != null)
            {
                _context.ClassCategories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
