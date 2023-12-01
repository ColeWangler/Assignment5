using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment5.Data;
using Assignment5.Models;

namespace Assignment5.Controllers
{
    public class MusicInventoriesController : Controller
    {
        private readonly Assignment5Context _context;

        public MusicInventoriesController(Assignment5Context context)
        {
            _context = context;
        }

        // GET: MusicInventories
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.MusicInventory == null)
            {
                return Problem("Entity set  is null.");
            }

            var music = from m in _context.MusicInventory
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                music = music.Where(s => s.musicTitle!.Contains(searchString));
            }

            return View(await music.ToListAsync());
        }

            // GET: MusicInventories/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MusicInventory == null)
            {
                return NotFound();
            }

            var musicInventory = await _context.MusicInventory
                .FirstOrDefaultAsync(m => m.id == id);
            if (musicInventory == null)
            {
                return NotFound();
            }

            return View(musicInventory);
        }

        // GET: MusicInventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MusicInventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,musicTitle,genre,performer,price,typeOfDownload,yearReleased")] MusicInventory musicInventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musicInventory);
        }

        // GET: MusicInventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MusicInventory == null)
            {
                return NotFound();
            }

            var musicInventory = await _context.MusicInventory.FindAsync(id);
            if (musicInventory == null)
            {
                return NotFound();
            }
            return View(musicInventory);
        }

        // POST: MusicInventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,musicTitle,genre,performer,price,typeOfDownload,yearReleased")] MusicInventory musicInventory)
        {
            if (id != musicInventory.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicInventoryExists(musicInventory.id))
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
            return View(musicInventory);
        }

        // GET: MusicInventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MusicInventory == null)
            {
                return NotFound();
            }

            var musicInventory = await _context.MusicInventory
                .FirstOrDefaultAsync(m => m.id == id);
            if (musicInventory == null)
            {
                return NotFound();
            }

            return View(musicInventory);
        }

        // POST: MusicInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MusicInventory == null)
            {
                return Problem("Entity set 'Assignment5Context.MusicInventory'  is null.");
            }
            var musicInventory = await _context.MusicInventory.FindAsync(id);
            if (musicInventory != null)
            {
                _context.MusicInventory.Remove(musicInventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicInventoryExists(int id)
        {
          return (_context.MusicInventory?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
