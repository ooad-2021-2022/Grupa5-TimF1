using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using F1.Data;
using F1.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace F1.Controllers
{
    public class EkipaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EkipaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ekipa
        public async Task<IActionResult> Index()
        {
            return View(await _context.ekipe.ToListAsync());
        }

        // GET: Ekipa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekipa = await _context.ekipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ekipa == null)
            {
                return NotFound();
            }

            return View(ekipa);
        }

        // GET: Ekipa/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ekipa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Bodovi")] Ekipa ekipa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ekipa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           if(_context.ekipe.ToList().Exists(e => e.Naziv.Equals(ekipa.Naziv)))
                new ValidationResult("Ovaj naziv je već dodijeljen.");

            return View(ekipa);
        }

        // GET: Ekipa/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekipa = await _context.ekipe.FindAsync(id);
            if (ekipa == null)
            {
                return NotFound();
            }
            return View(ekipa);
        }

        // POST: Ekipa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Bodovi")] Ekipa ekipa)
        {
            if (id != ekipa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ekipa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EkipaExists(ekipa.Id))
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
            return View(ekipa);
        }

        // GET: Ekipa/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekipa = await _context.ekipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ekipa == null)
            {
                return NotFound();
            }

            return View(ekipa);
        }

        // POST: Ekipa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ekipa = await _context.ekipe.FindAsync(id);
            _context.ekipe.Remove(ekipa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EkipaExists(int id)
        {
            return _context.ekipe.Any(e => e.Id == id);
        }
    }
}
