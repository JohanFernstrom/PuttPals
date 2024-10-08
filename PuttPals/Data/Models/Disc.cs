using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PuttPals.Data.Models
{
    public class Disc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // e.g., Driver, Midrange, Putter
        public double Weight { get; set; } // in grams
        public string Plastic { get; set; }
        public string ImageUrl { get; set; }
        public List<PlayerDisc> PlayerDiscs { get; set; } = new List<PlayerDisc>();


        [Range(1, 14, ErrorMessage = "Speed must be between 1 and 14.")]
        public int Speed { get; set; }

        [Range(1, 7, ErrorMessage = "Glide must be between 1 and 7.")]
        public int Glide { get; set; }

        [Range(-5, 1, ErrorMessage = "Turn must be between -5 and 1.")]
        public int Turn { get; set; }

        [Range(0, 6, ErrorMessage = "Fade must be between 0 and 6.")]
        public int Fade { get; set; }
    }
}
