using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PuttPals.Data.Models
{
    public class Player : Person
    {
        public int Id { get; set; } //PK
        public required string Username { get; set; }
        public required string ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateJoined { get; set; }
        public List<PlayerDisc> PlayerDiscs { get; set; } = new List<PlayerDisc>();
        public List<Bag> Bags { get; set; } = new List<Bag>();
        public PlayerStats Stats { get; set; } = new PlayerStats();

        //FK for IdentityUser
        public required string IdentityUserId { get; set; }
        public required ApplicationUser IdentityUser { get; set; }

        public void AddDisc(Disc disc)
        {
            PlayerDiscs.Add(new PlayerDisc { PlayerId = this.Id, DiscId = disc.Id, Player = this, Disc = disc });
        }

        public void RemoveDisc(Disc disc)
        {
            var playerDisc = PlayerDiscs.Find(pd => pd.DiscId == disc.Id);
            if (playerDisc != null)
            {
                PlayerDiscs.Remove(playerDisc);
            }
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
