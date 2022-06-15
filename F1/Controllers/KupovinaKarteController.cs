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
{   [Authorize]
    public class KupovinaKarteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KupovinaKarteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KupovinaKarte
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.karte.Include(k => k.Utrka);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: KupovinaKarte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karta = await _context.karte
                .Include(k => k.Utrka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karta == null)
            {
                return NotFound();
            }

            return View(karta);
        }

        // GET: KupovinaKarte/Create
        public IActionResult Create()
        {
            ViewData["IdUtrke"] = new SelectList(_context.utrke, "Id", "Id");
            return View();
        }

        // POST: KupovinaKarte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RedniBroj,Kategorija,Cijena,IdUtrke")] Karta karta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(karta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUtrke"] = new SelectList(_context.utrke, "Id", "Id", karta.IdUtrke);
            return View(karta);
        }

        // GET: KupovinaKarte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karta = await _context.karte.FindAsync(id);
            if (karta == null)
            {
                return NotFound();
            }
            ViewData["IdUtrke"] = new SelectList(_context.utrke, "Id", "Id", karta.IdUtrke);
            return View(karta);
        }

        // POST: KupovinaKarte/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RedniBroj,Kategorija,Cijena,IdUtrke")] Karta karta)
        {
            if (id != karta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KartaExists(karta.Id))
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
            ViewData["IdUtrke"] = new SelectList(_context.utrke, "Id", "Id", karta.IdUtrke);
            return View(karta);
        }

        // GET: KupovinaKarte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karta = await _context.karte
                .Include(k => k.Utrka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karta == null)
            {
                return NotFound();
            }

            return View(karta);
        }

        // POST: KupovinaKarte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var karta = await _context.karte.FindAsync(id);
            _context.karte.Remove(karta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KartaExists(int id)
        {
            return _context.karte.Any(e => e.Id == id);
        }
    }
}
