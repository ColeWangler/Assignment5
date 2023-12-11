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
        public async Task<IActionResult> Index(string musicGenre,string musicPerformer, string searchString)
        {
            if (_context.MusicInventory == null)
            {
                return Problem("Entity set  is null.");
            }

            IQueryable<string> genreQuery = from m in _context.MusicInventory
                                            orderby m.genre
                                            select m.genre;


            IQueryable<string> producerQuery = from m in _context.MusicInventory
                                               orderby m.performer
                                               where m.genre == musicGenre
                                            select m.performer;

            var music = from m in _context.MusicInventory
                        select m;


            if (!string.IsNullOrEmpty(searchString))
            {
                music = music.Where(a => a.musicTitle!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(musicGenre) )
            {

                music = music.Where(x => x.genre == musicGenre );
            }

            if (!string.IsNullOrEmpty(musicPerformer))
            {
                music = music.Where(a => a.performer!.Contains(musicPerformer));
            }


            var musicGenreVM = new MusicGenreModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Producers = new SelectList(await producerQuery.Distinct().ToListAsync()),
                Music = await music.ToListAsync()
            };

            return View(musicGenreVM);
        }

            
    }
}
