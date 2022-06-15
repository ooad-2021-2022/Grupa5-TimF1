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
    [Authorize(Roles = "Zaposlenik")]

    public class RezultatZaVozacaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezultatZaVozacaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RezultatZaVozaca
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.rezultatUtrke.Include(r => r.Utrka).Include(r => r.Vozac);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RezultatZaVozaca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezultatZaVozaca = await _context.rezultatUtrke
                .Include(r => r.Utrka)
                .Include(r => r.Vozac)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rezultatZaVozaca == null)
            {
                return NotFound();
            }

            return View(rezultatZaVozaca);
        }

        // GET: RezultatZaVozaca/Create
        public IActionResult Create()
        {
            ViewData["IdUtrke"] = new SelectList(_context.utrke, "Id", "Id");
            ViewData["IdVozaca"] = new SelectList(_context.vozaci, "Id", "Id");
            return View();
        }

        // POST: RezultatZaVozaca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdVozaca,Bodovi,IdUtrke")] RezultatZaVozaca rezultatZaVozaca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezultatZaVozaca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUtrke"] = new SelectList(_context.utrke, "Id", "Id", rezultatZaVozaca.IdUtrke);
            ViewData["IdVozaca"] = new SelectList(_context.vozaci, "Id", "Id", rezultatZaVozaca.IdVozaca);
            return View(rezultatZaVozaca);
        }

        // GET: RezultatZaVozaca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezultatZaVozaca = await _context.rezultatUtrke.FindAsync(id);
            if (rezultatZaVozaca == null)
            {
                return NotFound();
            }
            ViewData["IdUtrke"] = new SelectList(_context.utrke, "Id", "Id", rezultatZaVozaca.IdUtrke);
            ViewData["IdVozaca"] = new SelectList(_context.vozaci, "Id", "Id", rezultatZaVozaca.IdVozaca);
            return View(rezultatZaVozaca);
        }

        // POST: RezultatZaVozaca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdVozaca,Bodovi,IdUtrke")] RezultatZaVozaca rezultatZaVozaca)
        {
            if (id != rezultatZaVozaca.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezultatZaVozaca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezultatZaVozacaExists(rezultatZaVozaca.ID))
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
            ViewData["IdUtrke"] = new SelectList(_context.utrke, "Id", "Id", rezultatZaVozaca.IdUtrke);
            ViewData["IdVozaca"] = new SelectList(_context.vozaci, "Id", "Id", rezultatZaVozaca.IdVozaca);
            return View(rezultatZaVozaca);
        }

        // GET: RezultatZaVozaca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezultatZaVozaca = await _context.rezultatUtrke
                .Include(r => r.Utrka)
                .Include(r => r.Vozac)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rezultatZaVozaca == null)
            {
                return NotFound();
            }

            return View(rezultatZaVozaca);
        }

        // POST: RezultatZaVozaca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezultatZaVozaca = await _context.rezultatUtrke.FindAsync(id);
            _context.rezultatUtrke.Remove(rezultatZaVozaca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezultatZaVozacaExists(int id)
        {
            return _context.rezultatUtrke.Any(e => e.ID == id);
        }
    }
}
