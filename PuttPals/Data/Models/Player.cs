using System;

namespace PuttPals.Data.Models
{
    public class Player : Person
    {
        public string Username { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateJoined { get; set; }
        public List<Disc> Discs { get; set; } = new List<Disc>();
        public List<Bag> Bags { get; set; } = new List<Bag>();
        public PlayerStats Stats { get; set; } = new PlayerStats();

        public void AddDisc(Disc disc)
        {
            Discs.Add(disc);
        }

        public void RemoveDisc(Disc disc)
        {
            Discs.Remove(disc);
        }

        public void AddBag(Bag bag)
        {
            Bags.Add(bag);
        }

        public void RemoveBag(Bag bag)
        {
            Bags.Remove(bag);
        }

        public void RecordAce()
        {
            Stats.Aces++;
        }

        public void RecordEagle()
        {
            Stats.Eagles++;
        }

        public void RecordBirdie()
        {
            Stats.Birdies++;
        }

        public void RecordPar()
        {
            Stats.Pars++;
        }

        public void RecordBogey()
        {
            Stats.Bogeys++;
        }
    }
}
