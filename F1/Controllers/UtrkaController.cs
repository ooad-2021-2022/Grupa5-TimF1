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
    public class UtrkaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtrkaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utrka
        public async Task<IActionResult> Index()
        {
            return View(await _context.utrke.ToListAsync());
        }

        // GET: Utrka/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utrka = await _context.utrke
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utrka == null)
            {
                return NotFound();
            }

            return View(utrka);
        }

        // GET: Utrka/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utrka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mjesto,Datum,BrojDostupnihKarata,IdVozaca")] Utrka utrka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utrka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utrka);
        }

        // GET: Utrka/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utrka = await _context.utrke.FindAsync(id);
            if (utrka == null)
            {
                return NotFound();
            }
            return View(utrka);
        }

        // POST: Utrka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mjesto,Datum,BrojDostupnihKarata,IdVozaca")] Utrka utrka)
        {
            if (id != utrka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utrka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtrkaExists(utrka.Id))
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
            return View(utrka);
        }

        // GET: Utrka/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utrka = await _context.utrke
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utrka == null)
            {
                return NotFound();
            }

            return View(utrka);
        }

        // POST: Utrka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utrka = await _context.utrke.FindAsync(id);
            _context.utrke.Remove(utrka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtrkaExists(int id)
        {
            return _context.utrke.Any(e => e.Id == id);
        }
    }
}
