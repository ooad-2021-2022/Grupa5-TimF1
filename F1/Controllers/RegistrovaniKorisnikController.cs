using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using F1.Data;
using F1.Models;

namespace F1.Controllers
{
    public class RegistrovaniKorisnikController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrovaniKorisnikController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RegistrovaniKorisnik
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.korisnici.Include(r => r.BankovniRacun);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RegistrovaniKorisnik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrovaniKorisnik = await _context.korisnici
                .Include(r => r.BankovniRacun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registrovaniKorisnik == null)
            {
                return NotFound();
            }

            return View(registrovaniKorisnik);
        }

        // GET: RegistrovaniKorisnik/Create
        public IActionResult Create()
        {
            ViewData["IdBankovniRacun"] = new SelectList(_context.racuni, "BrojRacuna", "BrojRacuna");
            return View();
        }

        // POST: RegistrovaniKorisnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Prezime,Username,Password,PremiumKod,IdBankovniRacun,IdAsp")] RegistrovaniKorisnik registrovaniKorisnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registrovaniKorisnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBankovniRacun"] = new SelectList(_context.racuni, "BrojRacuna", "BrojRacuna", registrovaniKorisnik.IdBankovniRacun);
            return View(registrovaniKorisnik);
        }

        // GET: RegistrovaniKorisnik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrovaniKorisnik = await _context.korisnici.FindAsync(id);
            if (registrovaniKorisnik == null)
            {
                return NotFound();
            }
            ViewData["IdBankovniRacun"] = new SelectList(_context.racuni, "BrojRacuna", "BrojRacuna", registrovaniKorisnik.IdBankovniRacun);
            return View(registrovaniKorisnik);
        }

        // POST: RegistrovaniKorisnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime,Username,Password,PremiumKod,IdBankovniRacun,IdAsp")] RegistrovaniKorisnik registrovaniKorisnik)
        {
            if (id != registrovaniKorisnik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registrovaniKorisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrovaniKorisnikExists(registrovaniKorisnik.Id))
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
            ViewData["IdBankovniRacun"] = new SelectList(_context.racuni, "BrojRacuna", "BrojRacuna", registrovaniKorisnik.IdBankovniRacun);
            return View(registrovaniKorisnik);
        }

        // GET: RegistrovaniKorisnik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrovaniKorisnik = await _context.korisnici
                .Include(r => r.BankovniRacun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registrovaniKorisnik == null)
            {
                return NotFound();
            }

            return View(registrovaniKorisnik);
        }

        // POST: RegistrovaniKorisnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registrovaniKorisnik = await _context.korisnici.FindAsync(id);
            _context.korisnici.Remove(registrovaniKorisnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrovaniKorisnikExists(int id)
        {
            return _context.korisnici.Any(e => e.Id == id);
        }
    }
}
