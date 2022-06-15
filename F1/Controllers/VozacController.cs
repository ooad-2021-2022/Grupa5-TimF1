using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using F1.Data;
using F1.Models;
using Microsoft.AspNetCore.Authorization;

namespace F1.Controllers
{
    public class VozacController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VozacController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vozac
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.vozaci.Include(v => v.Ekipa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vozac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozac = await _context.vozaci
                .Include(v => v.Ekipa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vozac == null)
            {
                return NotFound();
            }

            return View(vozac);
        }

        // GET: Vozac/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["IdEkipe"] = new SelectList(_context.ekipe, "Id", "Id");
            return View();
        }

        // POST: Vozac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        

        public async Task<IActionResult> Create([Bind("Id,Ime,Prezime,Drzava,Bodovi,IdEkipe")] Vozac vozac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vozac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEkipe"] = new SelectList(_context.ekipe, "Id", "Id", vozac.IdEkipe);
            return View(vozac);
        }

        // GET: Vozac/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozac = await _context.vozaci.FindAsync(id);
            if (vozac == null)
            {
                return NotFound();
            }
            ViewData["IdEkipe"] = new SelectList(_context.ekipe, "Id", "Id", vozac.IdEkipe);
            return View(vozac);
        }

        // POST: Vozac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime,Drzava,Bodovi,IdEkipe")] Vozac vozac)
        {
            if (id != vozac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vozac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VozacExists(vozac.Id))
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
            ViewData["IdEkipe"] = new SelectList(_context.ekipe, "Id", "Id", vozac.IdEkipe);
            return View(vozac);
        }

        // GET: Vozac/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozac = await _context.vozaci
                .Include(v => v.Ekipa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vozac == null)
            {
                return NotFound();
            }

            return View(vozac);
        }

        // POST: Vozac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vozac = await _context.vozaci.FindAsync(id);
            _context.vozaci.Remove(vozac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VozacExists(int id)
        {
            return _context.vozaci.Any(e => e.Id == id);
        }
    }
}
