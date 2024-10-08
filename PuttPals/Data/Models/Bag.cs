namespace PuttPals.Data.Models
{
    public class Bag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Disc> Discs { get; set; } = new List<Disc>();

        // Foreign key for Player
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public void AddDisc(Disc disc)
        {
            Discs.Add(disc);
        }

        public void RemoveDisc(Disc disc)
        {
            Discs.Remove(disc);
        }
    }
}
