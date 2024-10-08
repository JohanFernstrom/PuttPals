namespace PuttPals.Data.Models
{
    public class PlayerStats
    {
        public int Id { get; set; }
        public int Aces { get; set; }
        public int Eagles { get; set; }
        public int Birdies { get; set; }
        public int Pars { get; set; }
        public int Bogeys { get; set; }

        // Foreign key for Player
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
