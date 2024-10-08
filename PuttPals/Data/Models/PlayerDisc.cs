namespace PuttPals.Data.Models
{
    public class PlayerDisc
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int DiscId { get; set; }
        public Disc Disc { get; set; }

        public int Count { get; set; } = 1; // Default count to 1
    }
}
