namespace PuttPals.Data.Models
{
    public class Disc
    {
        public string Name { get; set; }
        public string Type { get; set; } // e.g., Driver, Midrange, Putter
        public double Weight { get; set; } // in grams
        public string Plastic { get; set; }
        public string ImageUrl { get; set; }
        public DiscStats Stats { get; set; } = new DiscStats();
    }
}
