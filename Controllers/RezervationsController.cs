using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using tattoo.Data;

namespace tattoo.Controllers
{
    [Authorize]
    public class RezervationsController : Controller
    {
        private readonly TattooDbContext _context;
        private readonly UserManager<Customer> _userManager;

        public RezervationsController(TattooDbContext context,UserManager<Customer> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Rezervations
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var TattooDbContext = _context.Rezervations.Include(o => o.Tattoos).Include(o => o.Customers);
                return View(await TattooDbContext.ToListAsync());
            }
            else
            {
                var currentUser = _userManager.GetUserId(User);
                var TattooDbContext = await _context.Rezervations.Include(o => o.Tattoos).Include(o => o.Customers)
                     .Where(x => x.CustomerId == currentUser.ToString()).ToListAsync();
                return View(TattooDbContext);
            }
        }

        // GET: Rezervations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rezervations == null)
            {
                return NotFound();
            }

            var rezervation = await _context.Rezervations
                .Include(r => r.Customers)
                .Include(r => r.Employers)
                .Include(r => r.Tattoos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervation == null)
            {
                return NotFound();
            }

            return View(rezervation);
        }

        // GET: Rezervations/Create
        public IActionResult Create()
        {
            ViewData["EmployerId"] = new SelectList(_context.Employers, "Id", "Name");
            ViewData["TattooId"] = new SelectList(_context.Tattoos, "Id", "Name");
            return View();
        }

        // POST: Rezervations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TattooId,EmployerId,Time")] Rezervation rezervation)
        {
            if (ModelState.IsValid)
            {
                rezervation.CustomerId = _userManager.GetUserId(User);
                _context.Rezervations.Add(rezervation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployerId"] = new SelectList(_context.Employers, "Id", "Name", rezervation.EmployerId);
            ViewData["TattooId"] = new SelectList(_context.Tattoos, "Id", "Name", rezervation.TattooId);
            return View(rezervation);
        }

        // GET: Rezervations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rezervations == null)
            {
                return NotFound();
            }

            var rezervation = await _context.Rezervations.FindAsync(id);
            if (rezervation == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "UserName", rezervation.CustomerId);
            ViewData["EmployerId"] = new SelectList(_context.Employers, "Id", "Id", rezervation.EmployerId);
            ViewData["TattooId"] = new SelectList(_context.Tattoos, "Id", "Id", rezervation.TattooId);
            return View(rezervation);
        }

        // POST: Rezervations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TattooId,EmployerId,Time")] Rezervation rezervation)
        {
            if (id != rezervation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    rezervation.CustomerId = _userManager.GetUserId(User);
                    _context.Update(rezervation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervationExists(rezervation.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "UserName", rezervation.CustomerId);
            ViewData["EmployerId"] = new SelectList(_context.Employers, "Id", "Id", rezervation.EmployerId);
            ViewData["TattooId"] = new SelectList(_context.Tattoos, "Id", "Id", rezervation.TattooId);
            return View(rezervation);
        }

        // GET: Rezervations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rezervations == null)
            {
                return NotFound();
            }

            var rezervation = await _context.Rezervations
                .Include(r => r.Customers)
                .Include(r => r.Employers)
                .Include(r => r.Tattoos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervation == null)
            {
                return NotFound();
            }

            return View(rezervation);
        }

        // POST: Rezervations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rezervations == null)
            {
                return Problem("Entity set 'TattooDbContext.Rezervations'  is null.");
            }
            var rezervation = await _context.Rezervations.FindAsync(id);
            if (rezervation != null)
            {
                _context.Rezervations.Remove(rezervation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervationExists(int id)
        {
          return (_context.Rezervations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
