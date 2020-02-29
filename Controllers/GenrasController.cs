using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using labb3.DB;
using labb3.Models;

namespace labb3.Controllers
{
    public class GenrasController : Controller
    {
        private readonly GenraContext _context;

        public GenrasController(GenraContext context)
        {
            _context = context;
        }

        // GET: Genras
        public async Task<IActionResult> Index()
        {
            var genraContext = _context.Genra.Include(g => g.Artist);
            return View(await genraContext.ToListAsync());
        }

        // GET: Genras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genra = await _context.Genra
                .Include(g => g.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genra == null)
            {
                return NotFound();
            }

            return View(genra);
        }

        // GET: Genras/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Name");
            return View();
        }

        // POST: Genras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ArtistId")] Genra genra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", genra.ArtistId);
            return View(genra);
        }

        // GET: Genras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genra = await _context.Genra.FindAsync(id);
            if (genra == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", genra.ArtistId);
            return View(genra);
        }

        // POST: Genras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ArtistId")] Genra genra)
        {
            if (id != genra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenraExists(genra.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", genra.ArtistId);
            return View(genra);
        }

        // GET: Genras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genra = await _context.Genra
                .Include(g => g.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genra == null)
            {
                return NotFound();
            }

            return View(genra);
        }

        // POST: Genras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genra = await _context.Genra.FindAsync(id);
            _context.Genra.Remove(genra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenraExists(int id)
        {
            return _context.Genra.Any(e => e.Id == id);
        }
    }
}
