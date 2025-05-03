using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoManager_X.Models.RestosModel;

namespace RestoManager_X.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly RestosDbContext _context;

        public RestaurantsController(RestosDbContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index(double? seuilNoteMoyenneBas, double? seuilNoteMoyenneHaut)
        {
            if (!seuilNoteMoyenneBas.HasValue || !seuilNoteMoyenneHaut.HasValue || seuilNoteMoyenneBas > seuilNoteMoyenneHaut)
            {
                // No filter applied - return all restaurants
                var allRestaurants = _context.Restaurants.Include(r => r.Leroprietaire);
                return View(await allRestaurants.ToListAsync());
            }

            // Filter by average rating
            var restaurantsFiltres = await (
                from r in _context.Restaurants.Include(r => r.Leroprietaire)
                join a in _context.Avis on r.CodeResto equals a.NumResto into avisGroup
                let moyenneNote = avisGroup.Average(a => (double?)a.Note) ?? 0
                where moyenneNote >= seuilNoteMoyenneBas && moyenneNote <= seuilNoteMoyenneHaut
                select r
            ).ToListAsync();

            return View(restaurantsFiltres);
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var restaurant = await _context.Restaurants
                .Include(r => r.Leroprietaire)
                .FirstOrDefaultAsync(m => m.CodeResto == id);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["Numprop"] = new SelectList(_context.Proprietaires, "Numéro", "Email");
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeResto,NomResto,Specialite,Ville,Tel,Numprop")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Numprop"] = new SelectList(_context.Proprietaires, "Numéro", "Email", restaurant.Numprop);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return NotFound();

            ViewData["Numprop"] = new SelectList(_context.Proprietaires, "Numéro", "Email", restaurant.Numprop);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodeResto,NomResto,Specialite,Ville,Tel,Numprop")] Restaurant restaurant)
        {
            if (id != restaurant.CodeResto)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.CodeResto))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Numprop"] = new SelectList(_context.Proprietaires, "Numéro", "Email", restaurant.Numprop);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var restaurant = await _context.Restaurants
                .Include(r => r.Leroprietaire)
                .FirstOrDefaultAsync(m => m.CodeResto == id);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Restaurants/Avis/5
        public async Task<IActionResult> Avis(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.LesAvis)
                .FirstOrDefaultAsync(r => r.CodeResto == id);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.CodeResto == id);
        }
    }
}
