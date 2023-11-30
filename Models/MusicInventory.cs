namespace Assignment5.Models
{
    public class MusicInventory
    {
        public int id { get; set; }
        public string musicTitle { get; set; }
        public string genre { get; set; }
        public string performer { get; set; }
        public decimal price { get; set; }
        public string typeOfDownload { get; set; }
        public int yearReleased { get; set; }
    }
}
