using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tattoo.Data;

namespace tattoo.Controllers
{
    public class TattoosController : Controller
    {
        private readonly TattooDbContext _context;

        public TattoosController(TattooDbContext context)
        {
            _context = context;
        }

        // GET: Tattoos
        public async Task<IActionResult> Index()
        {
            var tattooDbContext = _context.Tattoos.Include(t => t.Categories);
            return View(await tattooDbContext.ToListAsync());
        }

        // GET: Tattoos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tattoos == null)
            {
                return NotFound();
            }

            var tattoo = await _context.Tattoos
                .Include(t => t.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tattoo == null)
            {
                return NotFound();
            }

            return View(tattoo);
        }

        // GET: Tattoos/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Tattoos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategorieId,Description,ImageURL,Price,RegisterON")] Tattoo tattoo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tattoo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", tattoo.CategorieId);
            return View(tattoo);
        }

        // GET: Tattoos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tattoos == null)
            {
                return NotFound();
            }

            var tattoo = await _context.Tattoos.FindAsync(id);
            if (tattoo == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", tattoo.CategorieId);
            return View(tattoo);
        }

        // POST: Tattoos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategorieId,Description,ImageURL,Price,RegisterON")] Tattoo tattoo)
        {
            if (id != tattoo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tattoo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TattooExists(tattoo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", tattoo.CategorieId);
            return View(tattoo);
        }

        // GET: Tattoos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tattoos == null)
            {
                return NotFound();
            }

            var tattoo = await _context.Tattoos
                .Include(t => t.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tattoo == null)
            {
                return NotFound();
            }

            return View(tattoo);
        }

        // POST: Tattoos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tattoos == null)
            {
                return Problem("Entity set 'TattooDbContext.Tattoos'  is null.");
            }
            var tattoo = await _context.Tattoos.FindAsync(id);
            if (tattoo != null)
            {
                _context.Tattoos.Remove(tattoo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TattooExists(int id)
        {
          return (_context.Tattoos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
