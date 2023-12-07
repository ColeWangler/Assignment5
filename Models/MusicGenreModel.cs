using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Assignment5.Models;

    public class MusicGenreModel
    {
        public List<MusicInventory>? Music { get; set; }
        public SelectList? Genres { get; set; }
    public string? MusicGenre { get; set; }
    public SelectList? Producers { get; set; }

    public string? MusicProducers { get; set; }
    public string? SearchString { get; set; }


    }

