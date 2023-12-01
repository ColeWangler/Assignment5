using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models
{
    public class MusicInventory
    {
        public int id { get; set; }
        [Display(Name = "Music Title:")]
        public string musicTitle { get; set; }
        [Display(Name = "Genre:")]
        public string genre { get; set; }
        [Display(Name = "Performer:")]
        public string performer { get; set; }
        [Display(Name = "Price")]
        public decimal price { get; set; }
        [Display(Name = "Download Type:")]
        public string typeOfDownload { get; set; }
        [Display(Name = "Year Released:")]
        public int yearReleased { get; set; }
    }
}
