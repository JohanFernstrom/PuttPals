namespace PuttPals.Data.Models
{
    public class Bag
    {
        public string Name { get; set; }
        public List<Disc> Discs { get; set; } = new List<Disc>();

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
