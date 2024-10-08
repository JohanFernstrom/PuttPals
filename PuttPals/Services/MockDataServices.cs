using PuttPals.Data.Models;

namespace PuttPals.Services
{
    public class MockPuttPalsDataService
    {
        private static List<Player>? _players = default!;
        private static List<Disc>? _discs = default!;

        public static List<Player>? Players
        {
            get
            {
                _players ??= InitializeMockPlayers();

                return _players;
            }
        }

        public static List<Disc>? Discs
        {
            get
            {
                _discs ??= InitializeMockDiscs();

                return _discs;
            }
        }

        private static List<Disc> InitializeMockDiscs()
        {
            return new List<Disc>
            {
                new Disc { Name = "Destroyer", Type = "Driver", Weight = 175, Plastic = "Star", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 12, Glide = 5, Turn = -1, Fade = 3 },
                new Disc { Name = "Buzzz", Type = "Midrange", Weight = 177, Plastic = "Z", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 5, Glide = 4, Turn = -1, Fade = 1  },
                new Disc { Name = "Leopard", Type = "Fairway Driver", Weight = 170, Plastic = "Champion", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 6, Glide = 5, Turn = -2, Fade = 1 },
                new Disc { Name = "Aviar", Type = "Putter", Weight = 175, Plastic = "DX", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 2, Glide = 3, Turn = 0, Fade = 1 },
                new Disc { Name = "Teebird", Type = "Fairway Driver", Weight = 172, Plastic = "Star", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 7, Glide = 5, Turn = 0, Fade = 2 },
                new Disc { Name = "Roc", Type = "Midrange", Weight = 180, Plastic = "KC Pro", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 4, Glide = 4, Turn = 0, Fade = 3 },
                new Disc { Name = "Firebird", Type = "Driver", Weight = 175, Plastic = "Champion", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 9, Glide = 3, Turn = 0, Fade = 4 },
                new Disc { Name = "Mako3", Type = "Midrange", Weight = 180, Plastic = "Star", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 5, Glide = 5, Turn = 0, Fade = 0 },
                new Disc { Name = "Wraith", Type = "Driver", Weight = 175, Plastic = "Star", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 11, Glide = 5, Turn = -1, Fade = 3 },
                new Disc { Name = "Valkyrie", Type = "Driver", Weight = 170, Plastic = "Champion", ImageUrl = "https://discsport.com/img/disc/discmania/s-line-horizon-fd3-solar-flare_sm.webp", Speed = 9, Glide = 4, Turn = -2, Fade = 2 }
            };
        }

        private static List<Player> InitializeMockPlayers()
        {
            var p1 = new Player
            {
                IsActive = true,
                DateJoined = new DateTime(2019, 1, 1),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                Username = "DiscMaster",
                IdentityUserId = "DiscMaster",
                IdentityUser = new Data.ApplicationUser(),
                ProfilePictureUrl = $"https://randomuser.me/api/portraits/men/{new Random().Next(75)}.jpg",
                //Discs = new List<Disc>
                //{
                //    new Disc { Name = "Destroyer", Type = "Driver", Weight = 175, Speed = 12, Glide = 5, Turn = -1, Fade = 3 },
                //    new Disc { Name = "Buzzz", Type = "Midrange", Weight = 177, Speed = 5, Glide = 4, Turn = -1, Fade = 1 }
                //},
                Bags = new List<Bag>
                {
                    new Bag { Name = "Tournament Bag", Discs = new List<Disc>
                        {
                            new Disc { Name = "Destroyer", Type = "Driver", Weight = 175, Speed = 12, Glide = 5, Turn = -1, Fade = 3 },
                            new Disc { Name = "Buzzz", Type = "Midrange", Weight = 177, Speed = 5, Glide = 4, Turn = -1, Fade = 1 }
                        }
                    }
                },
                Stats = new PlayerStats { Aces = 1, Eagles = 2, Birdies = 10, Pars = 20, Bogeys = 5 }
            };

            var p2 = new Player
            {
                IsActive = false,
                DateJoined = new DateTime(2020, 1, 1),
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "987-654-3210",
                Username = "AceQueen",
                IdentityUserId = "AceQueen",
                IdentityUser = new Data.ApplicationUser(),
                ProfilePictureUrl = $"https://randomuser.me/api/portraits/women/{new Random().Next(75)}.jpg",
                //Discs = new List<Disc>
                //{
                //    new Disc { Name = "Leopard", Type = "Fairway Driver", Weight = 170, Speed = 6, Glide = 5, Turn = -2, Fade = 1 },
                //    new Disc { Name = "Aviar", Type = "Putter", Weight = 175, Speed = 2, Glide = 3, Turn = 0, Fade = 1 }
                //},
                Bags = new List<Bag>
                {
                    new Bag { Name = "Casual Bag", Discs = new List<Disc>
                        {
                            new Disc { Name = "Leopard", Type = "Fairway Driver", Weight = 170, Speed = 6, Glide = 5, Turn = -2, Fade = 1 },
                            new Disc { Name = "Aviar", Type = "Putter", Weight = 175, Speed = 2, Glide = 3, Turn = 0, Fade = 1 }
                        }
                    }
                },
                Stats = new PlayerStats { Aces = 3, Eagles = 1, Birdies = 15, Pars = 25, Bogeys = 3 }
            };

            return new List<Player> { p1, p2 };
        }
    }
}
