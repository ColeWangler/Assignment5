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
    public class AdministratorController : Controller
    {
        private readonly Assignment5Context _context;

        public AdministratorController(Assignment5Context context)
        {
            _context = context;
        }

        // GET: Administrator
        public async Task<IActionResult> Index()
        {
              return _context.MusicInventory != null ? 
                          View(await _context.MusicInventory.ToListAsync()) :
                          Problem("Entity set 'Assignment5Context.MusicInventory'  is null.");
        }

        // GET: Administrator/Details/5
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

        // GET: Administrator/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Create
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

        // GET: Administrator/Edit/5
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

        // POST: Administrator/Edit/5
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

        // GET: Administrator/Delete/5
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

        // POST: Administrator/Delete/5
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
